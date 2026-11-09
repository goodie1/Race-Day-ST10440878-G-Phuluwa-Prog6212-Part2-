using System.Data;
using System.Data.SqlClient;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RaceDay_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationService;

        public Authentication(IAuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost("Sign up")]
        public string Sign_up(string name, string surname, string email,string Cell_Number,string role,string password)
        {
            return _authenticationService.Sign_up(name, surname, email, Cell_Number, role, password);
        }

        [HttpPost("Login")]
        public string Log_in(string name,string email,string password)
        {
            return _authenticationService.Log_in(name, email, password);
        }

        [HttpGet("{id}")]
        public string retrive_Organizer(int id)
        {

            return _authenticationService.retrive_Organizer(id);

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
            return _authenticationService.update_Profile(id, name, surname, email, Cell_Number, password);
        }


        [HttpDelete("{id}")]
        public string remove_Organizer(int id, string role)
        {
            return _authenticationService.remove_Organizer(id, role);
        }

    }
}
