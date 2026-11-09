using System.Data;
using System.Data.SqlClient;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RaceDay_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Result : ControllerBase
    {
        private readonly IResultsServices _resultService;

        public Result(IResultsServices resultService)
        {
            _resultService = resultService;
        }
        [HttpPost]
        public string enter_results(string finish_time,string race_Positions,string Avg_pace, string role)
        {
            return _resultService.enter_results(finish_time, race_Positions, Avg_pace, role);
        
        }

        [HttpGet("{id}")]
        public string retrieve(int id)
        {

            return _resultService.retrieve(id);

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
            return _resultService.update_result(id, finish_time, race_Positions, Avg_pace, role);
        }

        [HttpGet]
        public string get_all_results()
        {
            return _resultService.get_all_results();
        }
    }
}
