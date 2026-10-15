using System;
using System.Collections.Generic;
using System.Text;
using RaceDay_API.Controllers;

namespace RaceDay_Test
{
    public class Result_Test
    {
        // Validate finish time
        private bool IsValidFinishTime(string finish_time) 
        {
            return !string.IsNullOrWhiteSpace(finish_time); 
        }
        // Validate race positions
        private bool IsValidRacePositions(string race_Positions) 
        {
            return !string.IsNullOrWhiteSpace(race_Positions); 
        }
        // Validate average pace
        private bool IsValidAveragePace(string Avg_pace) 
        {
            return !string.IsNullOrWhiteSpace(Avg_pace);
        }
        // Validate role
        private bool IsValidRole(string role) 
        {
            return !string.IsNullOrWhiteSpace(role) && role.Trim().Equals( "Organizer", StringComparison.OrdinalIgnoreCase); 
        }



        [Fact]
        public void Finish_Time_Should_Accept_Valid_Value()
        {
            string finish_time = "01:30:45"; 
            bool result = IsValidFinishTime(finish_time); 
            Assert.True(result); 
        }
        [Fact]
        public void Finish_Time_Should_Reject_Empty_Value() 
        { 
            string finish_time = "";
            bool result = IsValidFinishTime(finish_time);
            Assert.False(result); 
        }



        [Fact]
        public void Race_Positions_Should_Accept_Valid_Value()
        {
            string race_Positions = "1"; 
            bool result = IsValidRacePositions(race_Positions);
            Assert.True(result); 
        }
        [Fact] 
        public void Race_Positions_Should_Reject_Empty_Value() 
        {
            string race_Positions = ""; 
            bool result = IsValidRacePositions(race_Positions); 
            Assert.False(result); 
        }



        [Fact]
        public void Average_Pace_Should_Accept_Valid_Value()
        { 
            string Avg_pace = "5:30 min/km"; 
            bool result = IsValidAveragePace(Avg_pace); 
            Assert.True(result); 
        }
        [Fact]
        public void Average_Pace_Should_Reject_Empty_Value() 
        {
            string Avg_pace = ""; 
            bool result = IsValidAveragePace(Avg_pace); 
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
        public void Result_Controller_Should_Not_Be_Null()
        { 
            Result controller = new Result(); 
            var result = controller; 
            Assert.NotNull(result);
        }



        [Fact]
        public void Enter_Results_Should_Reject_Participant()
        { 
            Result controller = new Result();
            string result = controller.enter_results( "01:30:45", "1", "5:30 min/km", "Participant"); 
            Assert.Equal( "You are not an organizer, you don't have access.", result);
        }



        [Fact]
        public void Update_Result_Should_Reject_Participant()
        { 
            Result controller = new Result();
            string result = controller.update_result( 1, "01:30:45", "1", "5:30 min/km", "Participant"); 
            Assert.Equal( "You are not an organizer, you don't have access.", result); 
        }



        [Fact]
        public void Enter_Results_Should_Reject_Empty_Role()
        {
            Result controller = new Result(); 
            string result = controller.enter_results( "01:30:45", "1", "5:30 min/km", ""); 
            Assert.Equal( "You are not an organizer, you don't have access.", result); 
        }



        [Fact]
        public void Retrieve_Invalid_ID_Should_Return_Empty_String()
        {
            Result controller = new Result(); 
            string result = controller.retrieve(-1);
            Assert.Equal("", result); 
        }
        
    }
}
