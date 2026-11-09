
using Microsoft.AspNetCore.Mvc;
using RaceDay_API.Services;


using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly ICategoriesServices _categoriesService;

        public Categories(ICategoriesServices categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public string category(string category_type, string distance, int age, string role)
        {
            return _categoriesService.category(
                category_type, distance, age, role);
        }

        [HttpGet("{id}")]
        public string return_info(int id)
        {

            return _categoriesService.return_info(id);
        }

        [HttpGet]
        public string get_all_categories()
        {
            return _categoriesService.get_all_categories();

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
    }
}