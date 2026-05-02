using Microsoft.Data.SqlClient;
using ProductApp.Models;
using System.Data;

namespace ProductApp.Repository
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        }

        // To Add Employee details
        public int AddEmployee(EmployeeModel obj)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand com = new SqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);

            con.Open();
            object? result = com.ExecuteScalar();
            return result is null ? 0 : Convert.ToInt32(result);
        }

        // To view employee details
        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            using SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                empList.Add(MapEmployee(dr));
            }

            return empList;
        }

        public EmployeeModel? GetEmployeeById(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand com = new SqlCommand("GetEmployeeById", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", id);
            using SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return MapEmployee(dt.Rows[0]);
        }

        // To Update Employee details
        public bool UpdateEmployee(EmployeeModel obj)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand com = new SqlCommand("UpdateEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", obj.Empid);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);

            con.Open();
            int i = com.ExecuteNonQuery();
            return i >= 1;
        }

        // To delete Employee details
        public bool DeleteEmployee(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand com = new SqlCommand("DeleteEmp", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", id);

            con.Open();
            int i = com.ExecuteNonQuery();
            return i >= 1;
        }

        private static EmployeeModel MapEmployee(DataRow dr)
        {
            return new EmployeeModel
            {
                Empid = Convert.ToInt32(dr["EmpId"]),
                Name = Convert.ToString(dr["Name"]) ?? string.Empty,
                City = Convert.ToString(dr["City"]) ?? string.Empty,
                Address = Convert.ToString(dr["Address"]) ?? string.Empty
            };
        }
    }
}
