
using System;
using System.Collections.Generic;
using System.Text;
using MyBankApp.Helpers;

namespace MyBankApp.ClassLibrary
{
    public static class CustomerAuth
    {

        

        public static void CreateCustomer()
        {
            Console.WriteLine("Please corrctly fill the following fields");
            Name:
            Console.WriteLine("FirstName(must contain more than three(3) characters): ");
            string firstName = Console.ReadLine();
            while (!Validate.stringInputIsValid(firstName))
            {
                goto Name;
            }
            LastName:
            Console.WriteLine("Surname(must contain more than three(3) characters): ");
            string surName = Console.ReadLine();
            while (!Validate.stringInputIsValid(surName))
            {
                goto LastName;
            }

            Email:
            Console.WriteLine("Email(must contain '@' and '.' and myst be more than 6 characters): ");
            string email = Console.ReadLine();
            while (!Validate.emailIsValid(email))
            {
                goto Email;
            }
            Password:
            Console.WriteLine("Password(must contain more than three(3) characters): ");
            string password = Console.ReadLine();
            while (!Validate.stringInputIsValid(password))
            {
                goto Password;
            }
            Customer newCustomer = new Customer(firstName, surName, email, password);
            Console.WriteLine($"Registration was successful for {firstName} {surName}\n\t\tYour login Id is {newCustomer.CustomerId}");
        }



        public static Customer Login(string customerEmail, string password)
        {
            Customer customer = Customer.GetCurrentCustomer(password, customerEmail);
            if (customer != null)
                customer.customerIsLoggedIn = true;
            return customer;
        }

        public static Customer Logout(Customer loggedInCustomer)
        {
            loggedInCustomer.customerIsLoggedIn = false;
            return null;
        } 
    }
}
