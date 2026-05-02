USE master;
GO

IF DB_ID('EmployeeDB') IS NULL
BEGIN
    CREATE DATABASE EmployeeDB;
END;
GO

USE EmployeeDB;
GO

IF OBJECT_ID('dbo.Employee', 'U') IS NULL
BEGIN
    CREATE TABLE Employee (
        EmpId INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        City NVARCHAR(100) NOT NULL,
        Address NVARCHAR(MAX) NOT NULL
    );
END;
GO

CREATE OR ALTER PROCEDURE AddNewEmpDetails
    @Name NVARCHAR(100),
    @City NVARCHAR(100),
    @Address NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employee (Name, City, Address)
    VALUES (@Name, @City, @Address);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS EmpId;
END;
GO

CREATE OR ALTER PROCEDURE GetEmployees
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EmpId, Name, City, Address
    FROM Employee
    ORDER BY EmpId;
END;
GO

CREATE OR ALTER PROCEDURE GetEmployeeById
    @EmpId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EmpId, Name, City, Address
    FROM Employee
    WHERE EmpId = @EmpId;
END;
GO

CREATE OR ALTER PROCEDURE UpdateEmpDetails
    @EmpId INT,
    @Name NVARCHAR(100),
    @City NVARCHAR(100),
    @Address NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employee
    SET Name = @Name,
        City = @City,
        Address = @Address
    WHERE EmpId = @EmpId;
END;
GO

CREATE OR ALTER PROCEDURE DeleteEmp
    @EmpId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Employee WHERE EmpId = @EmpId;
END;
GO
