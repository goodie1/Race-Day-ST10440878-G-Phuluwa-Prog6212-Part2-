using System;
using System.Collections.Generic;
using System.Text;
using RaceDay_API.Controllers;
namespace RaceDay_Test
{
    public class Enrollment_Test
    {
        // Validate authentication ID
        private bool IsValidAuthenticationId(int authentication_id)
        {
            return authentication_id > 0; 
        }

        // Validate category ID
        private bool IsValidCategoryId(int category_id) 
        {
            return category_id > 0; 
        }
        
        // Validate event ID
        private bool IsValidEventId(int event_id) 
        { return event_id > 0;
        }
        
        // Validate role
        private bool IsValidRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role) && role.Trim().Equals( "Participant", StringComparison.OrdinalIgnoreCase); 
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void Authentication_ID_Should_Accept_Valid_ID(int authentication_id)
        { 
            bool result = IsValidAuthenticationId(authentication_id); 
            Assert.True(result); 
        }
        [Theory] 
        [InlineData(0)] 
        [InlineData(-1)]
        public void Authentication_ID_Should_Reject_Invalid_ID(int authentication_id)
        { 
            bool result = IsValidAuthenticationId(authentication_id);
            Assert.False(result); 
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Category_ID_Should_Accept_Valid_ID(int category_id)
        { 
            bool result = IsValidCategoryId(category_id);
            Assert.True(result); 
        }
        [Theory] 
        [InlineData(0)] 
        [InlineData(-1)] 
        public void Category_ID_Should_Reject_Invalid_ID(int category_id) 
        { 
            bool result = IsValidCategoryId(category_id); 
            Assert.False(result); 
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void Event_ID_Should_Accept_Valid_ID(int event_id)
        { 
            bool result = IsValidEventId(event_id); 
            Assert.True(result); 
        }
        [Theory] 
        [InlineData(0)]
        [InlineData(-1)] 
        public void Event_ID_Should_Reject_Invalid_ID(int event_id) 
        { 
            bool result = IsValidEventId(event_id); 
            Assert.False(result); 
        }



        [Theory]
        [InlineData("Participant")]
        [InlineData("participant")]
        [InlineData("PARTICIPANT")]
        public void Role_Should_Accept_Participant(string role)
        { 
            bool result = IsValidRole(role);
            Assert.True(result);
        }
        [Theory] 
        [InlineData("Organizer")] 
        [InlineData("Admin")] 
        [InlineData("")]
        [InlineData(null)] 
        public void Role_Should_Reject_Invalid_Role(string role) 
        { 
            bool result = IsValidRole(role); 
            Assert.False(result);
        }




        [Fact]
        public void Enrollment_Controller_Should_Not_Be_Null()
        { 
            Enrollment controller = new Enrollment();
            var result = controller; 
            Assert.NotNull(result);
        }



        [Fact]
        public void Enter_Event_Should_Reject_Organizer()
        { 
            Enrollment controller = new Enrollment();
            string result = controller.enter_event( 1, 1, 1, "Organizer"); 
            Assert.Equal( "You are not an participant, you can not enter the race...", result); 
        }



        [Fact]
        public void Enter_Event_Should_Reject_Invalid_Role()
        { 
            Enrollment controller = new Enrollment();
            string result = controller.enter_event( 1, 1, 1, "Admin"); 
            Assert.Equal( "You are not an participant, you can not enter the race...", result);
        }



        [Fact]
        public void Enter_Event_Should_Reject_Empty_Role()
        {
            Enrollment controller = new Enrollment(); 
            string result = controller.enter_event( 1, 1, 1, ""); 
            Assert.Equal( "You are not an participant, you can not enter the race...", result); 
        }
        
    }
}
