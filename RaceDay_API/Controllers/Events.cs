using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Events : ControllerBase
    {
        [HttpPost]
        public string create_event(string event_name,string date,string time,string event_Type,string Event_description,string location,string race_route,string weather,string role)
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

                string query = @"insert into event values('" + event_name + "','"+date+"','"+time+"','" + event_Type + "','" + Event_description + "','" + location + "','"+ race_route + "','"+ weather + "','"+ role + "');";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                message = "Event saved successfully";
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }

            return message;
        }

        [HttpGet("{id}")]
        public string event_details(int id)
        {
            string message = "ID not found...";

            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Select * from event where event_id='" + id + "';";
                SqlCommand run_query = new SqlCommand(query, Connect);

                SqlDataReader data_found = run_query.ExecuteReader();
                while (data_found.Read())
                {
                    message = "Welcome to Race Day..." +
                              "Event Name:"+data_found["event_name"].ToString()+
                              " Date:" + data_found["date"].ToString()+
                              " Time:" + data_found["time"].ToString()+
                              " Type:"+data_found["event_Type"].ToString()+
                              " Description:"+data_found["Event_description"].ToString()+
                              " Location:"+data_found["location"].ToString()+
                              " Race Route:"+data_found["race_route"].ToString()+
                              " Weather:"+data_found["weather"].ToString();
                }
                Connect.Close();
            }
            catch (Exception Error)
            {

                message = Error.Message;

            }
              return message;
        }

        [HttpPut("{id}")]
        public string event_update(int id,string event_name, string date, string time, string event_Type, string Event_description, string location, string race_route, string weather, string role)
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

                string query = @"Update event set event_name='" + event_name + "',date='"+date+"',time='"+time+"',event_Type='" + event_Type + "',Event_description='" + Event_description + "',location='" + location + "',race_route='" + race_route + "',weather='" + weather + "',role='" + role + "'" +
                                " where event_id='"+id+"';";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                int rowsAffected= run_query.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    message = "Event information has been updated successfully";
                }
                else
                {
                    message = "Event ID not found.";
                }

               
                Connect.Close();
            }
            catch (Exception Error)
            {
                message = Error.Message;
            }
            return message;
        }

        [HttpDelete("{id}")]
        public string events_deleted(int id,string role)
        {
            string message = "";
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();
                if (role != null && role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
                {
                     string query = @"Delete from event where event_id=" + id;
                     SqlCommand run_query = new SqlCommand(query, Connect);
                    
                    int row = run_query.ExecuteNonQuery();
                    if (row > 0)
                    {
                        message = "Event information has been deleted successfully...";
                    }
                    else
                    {
                        message = "Event can not be found...";
                    }
                }
                else
                {
                    message = "You are not an organizer, you don't have access.";
                }

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
