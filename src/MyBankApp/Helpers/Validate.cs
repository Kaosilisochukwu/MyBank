using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankApp.Helpers
{
    public static class Validate
    {
        public static int validateIntInput()
        {
            return 1;
        }
        public static bool emailIsValid(string email) => email.Contains("@") && email.Contains(".") && email.Length > 6 ? true : false;

        public static bool stringInputIsValid(string stringInput) => stringInput.Length > 3;
    }
}
