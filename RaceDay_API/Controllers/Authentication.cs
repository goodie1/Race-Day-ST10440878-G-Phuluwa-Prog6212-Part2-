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
