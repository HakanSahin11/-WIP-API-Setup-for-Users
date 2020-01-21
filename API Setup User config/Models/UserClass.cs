using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Setup_User_config.Models
{
    public class UserClass
    {
        public static readonly string Name = "Users";
        public UserClass(int id, string email, string password, string userType, string firstName, string lastName, char gender, string country, string city, string address, string jobTitle, int age)
        {
            _id = id;
            Email = email;
            Password = password;
            UserType = userType;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
            JobTitle = jobTitle;
            Age = age;
        }

        public int _id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
    }
}
