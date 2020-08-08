using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankApp.ClassLibrary
{
    public class LoginDetails
    {

        public LoginDetails(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
