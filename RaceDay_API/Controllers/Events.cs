using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaceDay_API.Services;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Events : ControllerBase
    {
        private readonly IEventsServices _eventsService;
        public  Events(IEventsServices eventsService)
        {
            _eventsService = eventsService;
        }
        [HttpPost]
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
            return _eventsService.create_event(
                event_name, date, time, event_Type,
                Event_description, location, race_route,
                weather, organiser_id, role);
        }

        [HttpGet("{id}")]
        public string event_details(int id)
        {
            return _eventsService.event_details(id);
        }

        [HttpPut("{id}")]
        public string event_update(int id,string event_name, string date, string time, string event_Type, string Event_description, string location, string race_route, string weather, string role)
        {
            return _eventsService.event_update(id,event_name,date,time,event_Type,Event_description,location,race_route,weather,role);
        }

        [HttpDelete("{id}")]
        public string events_deleted(int id,string role)
        {
            return _eventsService.events_deleted(id,role);
        }

        [HttpGet("organiser/{id}")]
        public string get_organiser_events(int id)
        {
            return _eventsService.get_organiser_events(id);
        }

        [HttpGet]
        public string get_all_events()
        {
            return _eventsService.get_all_events();
        }
    }
}
