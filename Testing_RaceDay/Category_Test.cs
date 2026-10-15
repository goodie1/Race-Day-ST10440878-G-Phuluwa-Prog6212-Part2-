using System;
using System.Collections.Generic;
using System.Text;
using RaceDay_API.Controllers;
namespace RaceDay_Test
{
    public class Category_Test
    {
        // Validate category type
        private bool IsValidCategoryType(string category_type) 
        {
            return !string.IsNullOrWhiteSpace(category_type); 
        } 
        // Validate distance
        private bool IsValidDistance(string distance) 
        {
            return !string.IsNullOrWhiteSpace(distance); 
        }
        // Validate age
        private bool IsValidAge(int age) 
        {
            return age > 0 && age <= 100; 
        }
        // Validate role
        private bool IsValidRole(string role) 
        {
            return !string.IsNullOrWhiteSpace(role) && role.Trim().Equals( "Organizer", StringComparison.OrdinalIgnoreCase);
        }


        [Fact]
        public void Category_Type_Should_Accept_Valid_Value()
        {
            string category_type = "Marathon"; 
            bool result = IsValidCategoryType(category_type); 
            Assert.True(result); 
        }
        [Fact] 
        public void Category_Type_Should_Reject_Empty_Value() 
        { 
            string category_type = ""; 
            bool result = IsValidCategoryType(category_type);
            Assert.False(result); 
        }



        [Fact]
        public void Distance_Should_Accept_Valid_Value()
        { 
            string distance = "21km"; 
            bool result = IsValidDistance(distance); 
            Assert.True(result); 
        }
        [Fact]
        public void Distance_Should_Reject_Empty_Value()
        { 
            string distance = "";
            bool result = IsValidDistance(distance);
            Assert.False(result);
        }



        [Theory]
        [InlineData(18)]
        [InlineData(25)]
        [InlineData(50)]
        public void Age_Should_Accept_Valid_Value(int age)
        { 
            bool result = IsValidAge(age); 
            Assert.True(result); 
        }
        [Theory] 
        [InlineData(0)] 
        [InlineData(-1)] 
        [InlineData(101)] 
        public void Age_Should_Reject_Invalid_Value(int age) 
        {
            bool result = IsValidAge(age);
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
        public void Categories_Controller_Should_Not_Be_Null()
        { 
            Categories controller = new Categories();
            var result = controller; 
            Assert.NotNull(result); 
        }



        [Fact]
        public void Category_Should_Reject_Participant()
        { 
            Categories controller = new Categories();
            string result = controller.category( "Marathon", "21km", 18, "Participant"); 
            Assert.Equal( "You are not an organizer, you don't have access.", result); 
        }



        [Fact]
        public void Return_Info_Should_Return_ID_Not_Found()
        { 
            Categories controller = new Categories(); 
            string result = controller.return_info(-1); 
            Assert.Equal("ID not found...", result);
        }
        
    }

}
