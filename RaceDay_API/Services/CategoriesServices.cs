using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.Json;

namespace RaceDay_API.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly string connection =
            @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

        // CREATE CATEGORY
        public string category(string category_type, string distance, int age, string role)
        {
            if (string.IsNullOrWhiteSpace(role) ||
                !role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase))
            {
                return "You are not an organiser. You don't have access.";
            }

            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = @"
                        INSERT INTO category
                        (category_type, distance, age, role)
                        VALUES
                        (@category_type, @distance, @age, @role)";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@category_type", category_type);
                        cmd.Parameters.AddWithValue("@distance", distance);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@role", "Organizer");

                        int rows = cmd.ExecuteNonQuery();

                        return rows > 0
                            ? "Category saved successfully!"
                            : "Category could not be saved.";
                    }
                }
            }
            catch (Exception Error)
            {
                return "Error saving category: " + Error.Message;
            }
        }

        // GET CATEGORY BY ID
        public string return_info(int id)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = "SELECT * FROM category WHERE category_id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return JsonSerializer.Serialize(new
                                {
                                    category_id = reader["category_id"],
                                    category_type = reader["category_type"],
                                    distance = reader["distance"],
                                    age = reader["age"]
                                });
                            }
                        }
                    }
                }

                return "Category ID not found.";
            }
            catch (Exception Error)
            {
                return "Error: " + Error.Message;
            }
        }

        // GET ALL CATEGORIES
        public string get_all_categories()
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    string query = "SELECT * FROM category";

                    using (SqlCommand cmd = new SqlCommand(query, Connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var categories = new List<Dictionary<string, object>>();

                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] =
                                    reader.IsDBNull(i) ? "" : reader.GetValue(i);
                            }

                            categories.Add(item);
                        }

                        return JsonSerializer.Serialize(categories);
                    }
                }
            }
            catch (Exception Error)
            {
                return "Error: " + Error.Message;
            }
        }
    }
}