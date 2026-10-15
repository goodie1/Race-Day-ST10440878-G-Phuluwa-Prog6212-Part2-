using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RaceDay_API.Controllers;

namespace RaceDay_Test
{
    public class Authentication_Test
    {

        // Check that the name contains letters only.
        private bool IsValidName(string name) 
        { 
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$"); 
        }

        // Check that the surname contains letters only.
        private bool IsValidSurname(string surname) 
        {
            return !string.IsNullOrWhiteSpace(surname) && Regex.IsMatch(surname, @"^[a-zA-Z]+$"); 
        }

        // Check that the email ends with @gmail.com.
        private bool IsValidEmail(string email) 
        { 
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$", RegexOptions.IgnoreCase); 
        }

        // contains 10 digits and starts with 0.
        private bool IsValidCellNumber(string cellNumber)
        { 
            string number = cellNumber.ToString(); 
            return Regex.IsMatch(number, @"^0[6-8][0-9]{8}$"); 
        }

        // Check that the role is Participant or Organizer.
        private bool IsValidRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role) && (role.Trim().Equals("Participant", StringComparison.OrdinalIgnoreCase) || role.Trim().Equals("Organizer", StringComparison.OrdinalIgnoreCase)); 
        }

        // Check that the password is not empty.
        private bool IsValidPassword(string password) 
        {
            return !string.IsNullOrWhiteSpace(password); 
        }

        [Fact]
        public void Name_Should_Accept_Letters()
        { 
            
            string name = "Amanda"; 
            bool result = IsValidName(name); 
            Assert.True(result); 
        }

        [Fact]
        public void Name_Should_Reject_Numbers()
        { 
            
            string name = "Amanda123"; 
            bool result = IsValidName(name); 
            Assert.False(result); 
        }

        [Fact]
        public void Name_Should_Reject_Empty_Value()
        {  
            string name = "";  
            bool result = IsValidName(name);  
            Assert.False(result); 
        }



        [Fact]
        public void Surname_Should_Accept_Letters()
        {  
            string surname = "Smith"; 
            bool result = IsValidSurname(surname); 
            Assert.True(result); 
        } 
        [Fact] 
        public void Surname_Should_Reject_Numbers() 
        {  
            string surname = "Smith123";  
            bool result = IsValidSurname(surname);  
            Assert.False(result); 
        }



        [Fact]
        public void Email_Should_Accept_Gmail_Address()
        {  
            string email = "amanda@gmail.com"; 
            bool result = IsValidEmail(email); 
            Assert.True(result); 
        } 
        [Fact] 
        public void Email_Should_Reject_Hotmail_Address() 
        {  
            string email = "amanda@hotmail.com";  
            bool result = IsValidEmail(email);  
            Assert.False(result);
        } 
        [Fact] 
        public void Email_Should_Reject_Invalid_Email() 
        {  
            string email = "amandagmail.com"; 
            bool result = IsValidEmail(email);
            Assert.False(result); 
        }
        [Fact] 
        public void Email_Should_Reject_Empty_Value() 
        {  string email = "";  
            bool result = IsValidEmail(email); 
            Assert.False(result); 
        }


        [Theory]
        [InlineData("0712345678")]
        [InlineData("0612345678")]
        [InlineData("0812345678")]
        public void Cell_Number_Should_Accept_Valid_Number(string cellNumber)
        {
            // Arrange and Act
            bool result = IsValidCellNumber(cellNumber);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData("123")]
        [InlineData("0912345678")]
        [InlineData("071234567")]
        [InlineData("07123456789")]
        public void Cell_Number_Should_Reject_Invalid_Number(string cellNumber)
        {
            // Arrange and Act
            bool result = IsValidCellNumber(cellNumber);

            // Assert
            Assert.False(result);
        }




        [Theory]
        [InlineData("Participant")]
        [InlineData("Organizer")]
        [InlineData("participant")]
        [InlineData("organizer")]
        public void Role_Should_Accept_Valid_Roles(string role)
        { 
            bool result = IsValidRole(role); 
            Assert.True(result); 
        }
        [Theory] 
        [InlineData("Admin")] 
        [InlineData("Manager")]
        [InlineData("")] 
        public void Role_Should_Reject_Invalid_Roles(string role) 
        { 
            bool result = IsValidRole(role);
            Assert.False(result); 
        }


        [Fact]
        public void Password_Should_Accept_NonEmpty_Value()
        { 
            string password = "RaceDay123!"; 
            bool result = IsValidPassword(password); 
            Assert.True(result); 
        }
        [Theory] 
        [InlineData("")]
        [InlineData(" ")] 
        [InlineData(null)]
        public void Password_Should_Reject_Empty_Value( string password) 
        { 
            bool result = IsValidPassword(password);
            Assert.False(result); 
        }

        [Fact]
        public void Authentication_Controller_Should_Not_Be_Null()
        { 
            Authentication controller = new Authentication(); 
            var result = controller; 
            Assert.NotNull(result); 
        }


        [Fact]
        public void Retrieve_Invalid_ID_Should_Return_ID_Not_Found()
        { 
            Authentication controller = new Authentication(); 
            int id = -1; 
            string result = controller.retrive_Organizer(id); 
            Assert.Equal("ID not found...", result); 
        }
        
    }


}
