using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        [HttpPost]
        public string category(string category_type, string distance, int age,string role)
        {
            
            if (string.IsNullOrWhiteSpace(role) || !role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
            {
                return "You are not an organizer, you don't have access.";
            }
            string message = "";

            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"insert into category values('"+category_type+"','"+distance+"','"+age+"','"+role+"');";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Information has been captured";
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }

            return message;
        }

        [HttpGet("{id}")]
        public string return_info(int id)
        {
            string message = "ID not found...";
        
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";
        
                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();
        
                string query = @"Select * from category where category_id=" + id;
                SqlCommand run_query = new SqlCommand(query, Connect);
        
                SqlDataReader data_found = run_query.ExecuteReader();
        
                while (data_found.Read())
                {
                    message = "Welcome to Rcae Day..." +
                             " Race Type:" + data_found["category_type"].ToString() +
                             " Distance:" + data_found["distance"].ToString() +
                             " Age:" + data_found["age"].ToString();
                }
        
                data_found.Close();
                Connect.Close();
            }
            catch (Exception)
            {
                message = "ID not found...";
            }
        
            return message;
        }
    }
}
