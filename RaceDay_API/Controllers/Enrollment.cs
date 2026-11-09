using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDay_API.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Enrollment : ControllerBase
    {
        private readonly IEnrollmentServices _enrollmentServices;
        public Enrollment(IEnrollmentServices enrollmentServices)
        {
            _enrollmentServices = enrollmentServices;
        }

        [HttpPost]
        public string enter_event(int authentication_id,int category_id, int event_id, string role)
        {
            return _enrollmentServices.enter_event(authentication_id,category_id,event_id,role);
        }

    }
}
