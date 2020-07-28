using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank
{
    class Customer
    {
        private static int customerId = 1;
        public Customer(string firstName, string lastName, string email)
        {            
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CustomerId = customerId;
            customerId += 1;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; }
    }
}
