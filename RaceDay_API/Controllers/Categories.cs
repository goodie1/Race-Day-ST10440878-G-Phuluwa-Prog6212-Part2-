[HttpGet("{id}")]
public string return_info(int id)
{
    string message = "ID not found...";

    try
    {
        string connection = @"Data Source=(Localdb)\ProjectModels;database=Raceday;";

        SqlConnection Connect = new SqlConnection(connection);
        Connect.Open();

        string query = @"Select * from category where category_id=" + id;
        SqlCommand run_query = new SqlCommand(query, Connect);

        SqlDataReader data_found = run_query.ExecuteReader();

        while (data_found.Read())
        {
            message = "Welcome to Rcae Day..." +
                     " Race Type:" + data_found["category_type"].ToString() +
                     " Distance:" + data_found["distance"].ToString() +
                     " Age:" + data_found["age"].ToString();
        }

        data_found.Close();
        Connect.Close();
    }
    catch (Exception)
    {
        message = "ID not found...";
    }

    return message;
}
