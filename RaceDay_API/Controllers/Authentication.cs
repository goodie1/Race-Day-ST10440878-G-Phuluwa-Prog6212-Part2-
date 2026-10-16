using System.Data;
using System.Data.SqlClient;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType; 


namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly PasswordHasher<object> passwordHasher = new PasswordHasher<object>();
        [HttpPost("Sign up")]
        public string Sign_up(string name, string surname, string email,string Cell_Number,string role,string password)
        {
            string message = " ";
            try
            {
                string hashedPasword = passwordHasher.HashPassword(null, password);
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"insert into authentication values('"+ name +"','"+ surname +"','"+ email +"', '"+ Cell_Number +"','"+role+"', @password);";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.Parameters.AddWithValue("@password", hashedPasword);
                run_query.ExecuteNonQuery();

                if (role.Trim().Equals("Participant", StringComparison.OrdinalIgnoreCase))
                {
                    message = "You have successfully signed up as a participant.";
                }
                else if (role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
                {
                    message = "You have successfully signed up as a Organizer.";
                }
                else
                {
                    message = "Invalid role. Please enter Participant or Organizer.";
                }
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }

            return message;
        }

        [HttpPost("Login")]
        public string Log_in(string name,string email)
        {
            string message = "";
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Select name,email,password from authentication 
                                where name='"+name+"' and email= '"+ email +"';";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();
       
                SqlDataReader data_found = run_query.ExecuteReader();
                if (data_found.HasRows)
                {
                    message = "You are now logged in";
                }
                else
                {
                    message = "This account is not registered...";
                }
                data_found.Close();
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }

            return message;
        }

        [HttpGet("{id}")]
        public string retrive_Organizer(int id)
        {
            string message = "ID not found...";
        
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";
        
                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();
        
                string query = @"Select * from authentication where authentication_id='" + id + "';";
                SqlCommand run_query = new SqlCommand(query, Connect);
        
                SqlDataReader data_found = run_query.ExecuteReader();
        
                while (data_found.Read())
                {
                    string role = data_found["role"].ToString();
        
                    if (role.Trim().Equals("Participant", StringComparison.OrdinalIgnoreCase))
                    {
                        message = "Welcome " + data_found["name"].ToString() + " " + data_found["surname"].ToString();
                    }
                    else if (role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
                    {
                        message = "Welcome " + data_found["name"].ToString() + " " + data_found["surname"].ToString();
                    }
                    else
                    {
                        message = "Invalid role.";
                    }
                }
        
                Connect.Close();
            }
            catch (Exception)
            {
                message = "ID not found...";
            }
        
            return message;
        }

        [HttpPut("{id}")]
        public string update_Profile(int id,string name, string surname, string email, string Cell_Number,string password)
        {
            string message = "";
            try
            {
                string hashedPasword = passwordHasher.HashPassword(null, password);
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Update authentication set name='"+ name +"',surname='"+ surname +"',email='"+email+"',Cell_Number='"+Cell_Number+"',password='"+password+"' where authentication_id='" + id + "' ;";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Event information has been updated successfully";
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }
            return message;

        }


        [HttpDelete("{id}")]
        public string remove_Organizer(int id, string role)
        {
            string message = "ID not found";
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Delete from authentication where authentication_id='"+ id +"';";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Account deleted successfully...";
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }
            return message;
        }

    }
}
