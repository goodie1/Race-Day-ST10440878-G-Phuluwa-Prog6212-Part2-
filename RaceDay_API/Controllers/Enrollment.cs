using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RaceDay_API.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class Enrollment : ControllerBase
    {
        [HttpPost]
        public string enter_event(int authentication_id,int category_id, int event_id, string role)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(role) || !role.Trim().Equals("Participant", StringComparison.OrdinalIgnoreCase))
            {
                return "You are not an participant, you can not enter the race...";
            }

            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"insert into enrollment values('"+authentication_id+ "','" +category_id+ "','" + event_id + "', '"+role+"');";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "You have enterd the race successfully...";
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
