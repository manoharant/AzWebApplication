using AzWebApplication.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzWebApplication.Services
{
    public class CourseService
    {
        // Ensure to change the below variables to reflect the connection details for your database
        private static string db_source = "manomssqlserver.database.windows.net";
        private static string db_user = "azureuser";
        private static string db_password = "Lufthansa@21";
        private static string db_database = "manomssqldb";

        private SqlConnection GetConnection()
        {
            // Here we are creating the SQL connection
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        /*public IEnumerable<Course> GetCourses()
        {
            List<Course> _lst = new List<Course>();
            string _statement = "SELECT CourseID,CourseName,rating from Course";
            SqlConnection _connection = GetConnection();
            // Let's open the connection
            _connection.Open();
            // We then construct the statement of getting the data from the Course table
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            // Using the SqlDataReader class , we will read all the data from the Course table
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Course _course = new Course()
                    {
                        CourseID = _reader.GetInt32(0),
                        CourseName = _reader.GetString(1),
                        Rating = _reader.GetDecimal(2)
                    };

                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return _lst;
        }*/
        public async Task<IEnumerable<Course>> GetCourses()
        {
            // Here we now make a call to the Azure Function
            // Make sure to replace the URL with your Azure Function
            string functionurl = "https://manofuncapp.azurewebsites.net/api/GetCourses?code=pYl8ygQQVsL0bae7ELADrzHKlfYotk3LTEWaaxEthTjSNFOTug8Daw==";
            // We are using the HttpClient class to send a request to the Azure Function and to get a response
            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage _response = await _client.GetAsync(functionurl);
                string _content = await _response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Course>>(_content);
            }
        }

    }
}
