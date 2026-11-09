using System.Data.SqlClient;
using System.Collections.Generic;

namespace RaceDay_API.Services
{
    public class EventsServices: IEventsServices
    {
        private readonly string connection =
            @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

        public string create_event(
    string event_name,
    string date,
    string time,
    string event_Type,
    string Event_description,
    string location,
    string race_route,
    string weather,
    int organiser_id,
    string role)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    // STEP 1: Check whether the user exists and is an organiser
                    string checkRole = @"
                SELECT role
                FROM authentication
                WHERE authentication_id = @organiser_id";

                    using (SqlCommand checkCommand = new SqlCommand(checkRole, Connect))
                    {
                        checkCommand.Parameters.AddWithValue("@organiser_id", organiser_id);

                        object result = checkCommand.ExecuteScalar();

                        if (result == null ||
                            !result.ToString().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
                        {
                            return "Access denied. Only registered organisers can create events.";
                        }
                    }

                    // STEP 2: Save the event information
                    string query = @"
                INSERT INTO event
                (
                    event_name,
                    date,
                    time,
                    event_type,
                    event_description,
                    location,
                    race_route,
                    weather,
                    role,
                    organiser_id
                )
                VALUES
                (
                    @event_name,
                    @date,
                    @time,
                    @event_type,
                    @event_description,
                    @location,
                    @race_route,
                    @weather,
                    @role,
                    @organiser_id
                )";

                    using (SqlCommand run_query = new SqlCommand(query, Connect))
                    {
                        run_query.Parameters.AddWithValue("@event_name", event_name);
                        run_query.Parameters.AddWithValue("@date", date);
                        run_query.Parameters.AddWithValue("@time", time);
                        run_query.Parameters.AddWithValue("@event_type", event_Type);
                        run_query.Parameters.AddWithValue("@event_description", Event_description ?? "");
                        run_query.Parameters.AddWithValue("@location", location);
                        run_query.Parameters.AddWithValue("@race_route", race_route ?? "");
                        run_query.Parameters.AddWithValue("@weather", weather ?? "");
                        run_query.Parameters.AddWithValue("@role", "Organizer");
                        run_query.Parameters.AddWithValue("@organiser_id", organiser_id);

                        int rowsAffected = run_query.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Event saved successfully!";
                        }
                        else
                        {
                            return "Event could not be saved.";
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                return "Error saving event: " + Error.Message;
            }
        }

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
                              "Event Name:" + data_found["event_name"].ToString() +
                              " Date:" + data_found["date"].ToString() +
                              " Time:" + data_found["time"].ToString() +
                              " Type:" + data_found["event_Type"].ToString() +
                              " Description:" + data_found["Event_description"].ToString() +
                              " Location:" + data_found["location"].ToString() +
                              " Race Route:" + data_found["race_route"].ToString() +
                              " Weather:" + data_found["weather"].ToString();
                }
                Connect.Close();
            }
            catch (Exception Error)
            {

                message = Error.Message;

            }
            return message;
        }

        public string event_update(int id, string event_name, string date, string time, string event_Type, string Event_description, string location, string race_route, string weather, string role)
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

                string query = @"Update event set event_name='" + event_name + "',date='" + date + "',time='" + time + "',event_Type='" + event_Type + "',Event_description='" + Event_description + "',location='" + location + "',race_route='" + race_route + "',weather='" + weather + "',role='" + role + "'" +
                                " where event_id='" + id + "';";
                SqlCommand run_query = new SqlCommand(query, Connect);
                run_query.ExecuteNonQuery();

                int rowsAffected = run_query.ExecuteNonQuery();
                if (rowsAffected > 0)
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

        public string events_deleted(int id, string role)
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

        public string get_organiser_events(int organiser_id)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = @"
                SELECT * FROM event
                WHERE organiser_id = @organiser_id";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@organiser_id", organiser_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var events = new List<Dictionary<string, object>>();

                            while (reader.Read())
                            {
                                var item = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    item[reader.GetName(i)] =
                                        reader.IsDBNull(i) ? "" : reader.GetValue(i);
                                }

                                events.Add(item);
                            }

                            return System.Text.Json.JsonSerializer.Serialize(events);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string get_all_events()
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = "SELECT * FROM event";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var events = new List<Dictionary<string, object>>();

                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] =
                                    reader.IsDBNull(i) ? "" : reader.GetValue(i);
                            }

                            events.Add(item);
                        }

                        return System.Text.Json.JsonSerializer.Serialize(events);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
