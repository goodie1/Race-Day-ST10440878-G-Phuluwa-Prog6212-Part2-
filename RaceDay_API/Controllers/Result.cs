using System.Data;
using System.Data.SqlClient;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Result : ControllerBase
    {
        [HttpPost]
        public string enter_results(string finish_time,string race_Positions,string Avg_pace, string role)
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(role) || !role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
            {
                return "You are not an organizer, you don't have access.";
            }

            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"insert into result values('" + finish_time + "','"+race_Positions+"','"+Avg_pace+"','"+role+"');";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Results have been saved successfully...";
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }
            return message;
        }

        [HttpGet("{id}")]
        public string retrieve(int id)
        {
            string message = "";
        
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";
        
                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();
        
                string query = @"Select * from result where result_id='" + id + "';";
                SqlCommand run_query = new SqlCommand(query, Connect);
        
                SqlDataReader data_found = run_query.ExecuteReader();
        
                while (data_found.Read())
                {
                    message = "Welcome to Race Day..." +
                              " Finish Time:" + data_found["finish_time"].ToString() +
                              " Positions:" + data_found["race_Positions"].ToString() +
                              " Running Pace:" + data_found["Avg_pace"].ToString();
                }
        
                Connect.Close();
            }
            catch (Exception)
            {
                message = "";
            }
        
            return message;
        }

        [HttpPut("{id}")]
        public string update_result(int id, string finish_time, string race_Positions, string Avg_pace,string role)
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(role) || !role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
            {
                return "You are not an organizer, you don't have access.";
            }

            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Update result set finish_time='" + finish_time + "', race_Positions='"+race_Positions+"',Avg_pace='"+Avg_pace+ "' where result_id='" + id+"';";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Results have been updated successfully";
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
