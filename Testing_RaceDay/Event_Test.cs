using System;
using System.Collections.Generic;
using System.Text;
using RaceDay_API.Controllers;

namespace RaceDay_Test
{
    public class Event_Test
    {
        // Validate event name
        private bool IsValidEventName(string event_name) 
        {
            return !string.IsNullOrWhiteSpace(event_name); 
        }
        // Validate date
        private bool IsValidDate(string date) 
        {
            return !string.IsNullOrWhiteSpace(date); 
        }
        // Validate time
        private bool IsValidTime(string time) 
        {
            return !string.IsNullOrWhiteSpace(time); 
        }
        // Validate event type
        private bool IsValidEventType(string event_Type) 
        {
            return !string.IsNullOrWhiteSpace(event_Type); 
        }
        // Validate event description
        private bool IsValidDescription(string Event_description) 
        {
            return !string.IsNullOrWhiteSpace(Event_description); 
        }
        // Validate location
        private bool IsValidLocation(string location) 
        {
            return !string.IsNullOrWhiteSpace(location);
        }
        // Validate race route
        private bool IsValidRaceRoute(string race_route) 
        {
            return !string.IsNullOrWhiteSpace(race_route);
        }
        // Validate weather
        private bool IsValidWeather(string weather) 
        {
            return !string.IsNullOrWhiteSpace(weather); 
        }
        // Validate role
        private bool IsValidRole(string role) 
        {
            return !string.IsNullOrWhiteSpace(role) && role.Trim().Equals( "Organizer", StringComparison.OrdinalIgnoreCase);
        }



        [Fact]
        public void Event_Name_Should_Accept_Valid_Value()
        {
            string event_name = "Johannesburg Marathon"; 
            bool result = IsValidEventName(event_name); 
            Assert.True(result);
        }
        [Fact] 
        public void Event_Name_Should_Reject_Empty_Value()
        { 
            string event_name = ""; 
            bool result = IsValidEventName(event_name);
            Assert.False(result);
        }



        [Fact]
        public void Date_Should_Accept_Valid_Value()
        { 
            string date = "2026-11-15"; 
            bool result = IsValidDate(date);
            Assert.True(result); 
        }
        [Fact] 
        public void Date_Should_Reject_Empty_Value() 
        { 
            string date = "";
            bool result = IsValidDate(date); 
            Assert.False(result); 
        }



        [Fact]
        public void Time_Should_Accept_Valid_Value()
        { 
            string time = "07:00";
            bool result = IsValidTime(time); 
            Assert.True(result); 
        }
        [Fact] 
        public void Time_Should_Reject_Empty_Value() 
        {
            string time = "";
            bool result = IsValidTime(time); 
            Assert.False(result); 
        }



        [Fact]
        public void Event_Type_Should_Accept_Valid_Value()
        { 
            string event_Type = "Marathon"; 
            bool result = IsValidEventType(event_Type); 
            Assert.True(result);
        }
        [Fact] 
        public void Event_Type_Should_Reject_Empty_Value() 
        {
            
            string event_Type = ""; 
            bool result = IsValidEventType(event_Type);
            Assert.False(result); 
        }



        [Fact]
        public void Description_Should_Accept_Valid_Value()
        { 
            string Event_description = "Annual city marathon"; 
            bool result = IsValidDescription(Event_description); 
            Assert.True(result); 
        }
        [Fact]
        public void Description_Should_Reject_Empty_Value() 
        { 
            string Event_description = ""; 
            bool result = IsValidDescription(Event_description);
            Assert.False(result); 
        }



        [Fact]
        public void Location_Should_Accept_Valid_Value()
        { 
            string location = "Johannesburg"; 
            bool result = IsValidLocation(location); 
            Assert.True(result); 
        }
        [Fact]
        public void Location_Should_Reject_Empty_Value() 
        { 
            string location = "";
            bool result = IsValidLocation(location); 
            Assert.False(result); 
        }



        [Fact]
        public void Race_Route_Should_Accept_Valid_Value()
        { 
            string race_route = "City Centre";
            bool result = IsValidRaceRoute(race_route);
            Assert.True(result); 
        }
        [Fact]
        public void Race_Route_Should_Reject_Empty_Value() 
        { 
            string race_route = ""; 
            bool result = IsValidRaceRoute(race_route); 
            Assert.False(result);
        }



        [Fact]
        public void Weather_Should_Accept_Valid_Value()
        {
            string weather = "Sunny"; 
            bool result = IsValidWeather(weather);
            Assert.True(result); 
        }
        [Fact]
        public void Weather_Should_Reject_Empty_Value() 
        { 
            string weather = ""; 
            bool result = IsValidWeather(weather);
            Assert.False(result); 
        }



        [Theory]
        [InlineData("Organizer")]
        [InlineData("organizer")]
        [InlineData("ORGANIZER")]
        public void Role_Should_Accept_Organizer(string role)
        { 
            bool result = IsValidRole(role);
            Assert.True(result); 
        }
        [Theory] 
        [InlineData("Participant")]
        [InlineData("Admin")] 
        [InlineData("")] 
        [InlineData(null)]
        public void Role_Should_Reject_Non_Organizer(string role)
        { 
            bool result = IsValidRole(role);
            Assert.False(result);
        }



        [Fact]
        public void Events_Controller_Should_Not_Be_Null()
        { 
            Events controller = new Events();
            var result = controller; 
            Assert.NotNull(result);
        }




        [Fact]
        public void Create_Event_Should_Reject_Participant()
        { 
            Events controller = new Events(); 
            string result = controller.create_event( "Johannesburg Marathon", "2026-11-15", "07:00", "Marathon", "Annual city marathon", "Johannesburg", "City Centre", "Sunny", "Participant"); 
            Assert.Equal( "You are not an organizer, you don't have access.", result); 
        }



        [Fact]
        public void Update_Event_Should_Reject_Participant()
        { 
            Events controller = new Events(); 
            string result = controller.event_update( 1, "Johannesburg Marathon", "2026-11-15", "07:00", "Marathon", "Annual city marathon", "Johannesburg", "City Centre", "Sunny", "Participant");
            Assert.Equal( "You are not an organizer, you don't have access.", result);
        }



        [Fact]
        public void Delete_Event_Should_Reject_Participant()
        { 
            Events controller = new Events(); 
            string result = controller.events_deleted( 1, "Participant"); 
            Assert.Equal( "You are not an organizer, you don't have access.", result); 
        }
        
    }
}
