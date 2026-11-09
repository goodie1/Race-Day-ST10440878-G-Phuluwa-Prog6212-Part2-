using System.Data.SqlClient;
using System.Text.Json;
using System.Collections.Generic;

namespace RaceDay_API.Services
{
    public class ResultsServices : IResultsServices
    {
        private readonly string connection =
            @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

        // ENTER RESULTS
        public string enter_results(string finish_time,
            string race_Positions, string Avg_pace, string role)
        {
            if (string.IsNullOrWhiteSpace(role) ||
                !role.Trim().Equals("Organizer",
                StringComparison.OrdinalIgnoreCase))
            {
                return "Access denied. Only organisers can enter results.";
            }

            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = @"
                        INSERT INTO result
                        (finish_time, race_Positions, Avg_pace, role)
                        VALUES
                        (@finish_time, @race_Positions, @Avg_pace, @role)";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@finish_time", finish_time);
                        cmd.Parameters.AddWithValue("@race_Positions", race_Positions);
                        cmd.Parameters.AddWithValue("@Avg_pace", Avg_pace);
                        cmd.Parameters.AddWithValue("@role", "Organizer");

                        int rows = cmd.ExecuteNonQuery();

                        return rows > 0
                            ? "Results saved successfully!"
                            : "Results could not be saved.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error saving results: " + ex.Message;
            }
        }

        // RETRIEVE ONE RESULT
        public string retrieve(int id)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = "SELECT * FROM result WHERE result_id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return JsonSerializer.Serialize(new
                                {
                                    result_id = reader["result_id"],
                                    finish_time = reader["finish_time"],
                                    race_Positions = reader["race_Positions"],
                                    Avg_pace = reader["Avg_pace"]
                                });
                            }
                        }
                    }
                }

                return "Result ID not found.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // UPDATE RESULT
        public string update_result(int id, string finish_time,
            string race_Positions, string Avg_pace, string role)
        {
            if (string.IsNullOrWhiteSpace(role) ||
                !role.Trim().Equals("Organizer",
                StringComparison.OrdinalIgnoreCase))
            {
                return "Access denied. Only organisers can update results.";
            }

            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = @"
                        UPDATE result
                        SET finish_time = @finish_time,
                            race_Positions = @race_Positions,
                            Avg_pace = @Avg_pace
                        WHERE result_id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@finish_time", finish_time);
                        cmd.Parameters.AddWithValue("@race_Positions", race_Positions);
                        cmd.Parameters.AddWithValue("@Avg_pace", Avg_pace);
                        cmd.Parameters.AddWithValue("@id", id);

                        int rows = cmd.ExecuteNonQuery();

                        return rows > 0
                            ? "Results updated successfully!"
                            : "Result ID not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error updating results: " + ex.Message;
            }
        }

        // GET ALL RESULTS
        public string get_all_results()
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = "SELECT * FROM result";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var results = new List<Dictionary<string, object>>();

                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] =
                                    reader.IsDBNull(i) ? "" : reader.GetValue(i);
                            }

                            results.Add(item);
                        }

                        return JsonSerializer.Serialize(results);
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