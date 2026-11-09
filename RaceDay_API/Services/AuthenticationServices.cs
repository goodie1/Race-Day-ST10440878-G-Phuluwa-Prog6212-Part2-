using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace RaceDay_API.Services
{
    public class AuthenticationServices: IAuthenticationServices
    {
        private readonly PasswordHasher<object> passwordHasher = new PasswordHasher<object>();

        private readonly string connection =
            @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

        public string Sign_up(string name, string surname, string email, string Cell_Number, string role, string password)
        {
            string message = " ";
            try
            {
                string hashedPasword = passwordHasher.HashPassword(null, password);
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"insert into authentication values('" + name + "','" + surname + "','" + email + "', '" + Cell_Number + "','" + role + "', @password);";
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

        public string Log_in(string name, string email, string password)
        {
            string message = "";

            try
            {
                using (SqlConnection Connect = new SqlConnection(connection))
                {
                    Connect.Open();

                    // Retrieve the user using their name and email
                    string query = @"
                SELECT authentication_id, name, surname, email, role, password
                FROM authentication
                WHERE name = @name AND email = @email";

                    using (SqlCommand run_query = new SqlCommand(query, Connect))
                    {
                        run_query.Parameters.AddWithValue("@name", name);
                        run_query.Parameters.AddWithValue("@email", email);

                        using (SqlDataReader data_found = run_query.ExecuteReader())
                        {
                            if (data_found.Read())
                            {
                                // Retrieve the stored password hash
                                string storedPassword = data_found["password"].ToString();

                                // Verify the entered password
                                var result = passwordHasher.VerifyHashedPassword(
                                    null,
                                    storedPassword,
                                    password
                                );

                                if (result == PasswordVerificationResult.Success ||
                                    result == PasswordVerificationResult.SuccessRehashNeeded)
                                {
                                    // Retrieve user information
                                    int id = Convert.ToInt32(data_found["authentication_id"]);

                                    string userName = data_found["name"].ToString();
                                    string surname = data_found["surname"].ToString();
                                    string role = data_found["role"].ToString();

                                    // Return the user's information as JSON
                                    message = System.Text.Json.JsonSerializer.Serialize(new
                                    {
                                        success = true,
                                        message = "You are now logged in.",
                                        authentication_id = id,
                                        name = userName,
                                        surname = surname,
                                        role = role
                                    });
                                }
                                else
                                {
                                    message = System.Text.Json.JsonSerializer.Serialize(new
                                    {
                                        success = false,
                                        message = "Incorrect password."
                                    });
                                }
                            }
                            else
                            {
                                message = System.Text.Json.JsonSerializer.Serialize(new
                                {
                                    success = false,
                                    message = "Account not found."
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                message = System.Text.Json.JsonSerializer.Serialize(new
                {
                    success = false,
                    message = Error.Message
                });
            }

            return message;
        }

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
            catch (Exception Error)
            {

                message = Error.Message;

            }

            return message;
        }

        public string update_Profile(int id, string name, string surname, string email, string Cell_Number, string password)
        {
            string message = "";
            try
            {
                string hashedPasword = passwordHasher.HashPassword(null, password);
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Update authentication set name='" + name + "',surname='" + surname + "',email='" + email + "',Cell_Number='" + Cell_Number + "',password='" + password + "' where authentication_id='" + id + "' ;";
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

        public string remove_Organizer(int id, string role)
        {
            string message = "ID not found";
            try
            {
                string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

                SqlConnection Connect = new SqlConnection(connection);
                Connect.Open();

                string query = @"Delete from authentication where authentication_id='" + id + "';";
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
