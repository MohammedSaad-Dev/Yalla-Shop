using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Login_Request
    {
        [Required(ErrorMessage = "Please enter the Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }
    }
    public class UpdateContactDto
    {
        public string Address { get; set; }
        public string Phone { get; set; }
    }


    public class SignUpRequest
        {
            [Required(ErrorMessage = "Please enter the FristName")]
            public string FristName { get; set; }


            [Required(ErrorMessage = "Please enter the LastName")]
            public string LastName { get; set; }

           [Required(ErrorMessage = "Please enter the email")]
           [EmailAddress(ErrorMessage = "The email format is incorrect")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter the password")]
            [MinLength(6, ErrorMessage = "The password must be at least 6 characters long")]
            public string Password { get; set; }

          
    }

    

    public  static class Users
    {
        public static User Login(Login_Request request) 
        {
            return User.UserData.Login(request.Email, request.Password);
        }


        public static User SignUp(string FirstName, string LastName, string email, string password)
        {
            return User.UserData.SignUp(FirstName, LastName, email, password);


        }

       

        public static bool UpdateContactInfo(int userId, string address, string phone)
        {
            if (userId <= 0 || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
                return false;

            return User.UserData.UpdateUserContactInfo(userId, address, phone);
        }


       

    }
}
