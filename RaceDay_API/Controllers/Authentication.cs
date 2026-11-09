using System.Data;
using System.Data.SqlClient;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaceDay_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationService;

        public Authentication(IAuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost("Sign up")]
        public string Sign_up(string name, string surname, string email,string Cell_Number,string role,string password)
        {
            return _authenticationService.Sign_up(name, surname, email, Cell_Number, role, password);
        }

        [HttpPost("Login")]
        public string Log_in(string name,string email,string password)
        {
            return _authenticationService.Log_in(name, email, password);
        }

        [HttpGet("{id}")]
        public string retrive_Organizer(int id)
        {
            return _authenticationService.retrive_Organizer(id);
        }

        [HttpPut("{id}")]
        public string update_Profile(int id,string name, string surname, string email, string Cell_Number,string password)
        {
            return _authenticationService.update_Profile(id, name, surname, email, Cell_Number, password);
        }


        [HttpDelete("{id}")]
        public string remove_Organizer(int id, string role)
        {
            return _authenticationService.remove_Organizer(id, role);
        }

    }
}
