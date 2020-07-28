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
            AllCustomers.Add(this);
        }

        private static List<Customer> AllCustomers = new List<Customer>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; }


        public static bool customerExists(int id, string email)
        {
            bool customerExist = false;
            if (AllCustomers.Count < 1)
            {
                return customerExist;
            }
            foreach (var customer in AllCustomers)
            {
                if (customer.CustomerId == id && customer.Email.ToLower() == email.ToLower())
                    customerExist = true;
            }
            return customerExist;
        }

        public static Customer GetCurrentCustomer(int id, string email)
        {
            Customer currentCustomer = null;
            foreach (var customer in AllCustomers)
            {
                if (customer.CustomerId == id && customer.Email == email)
                {
                    currentCustomer = customer;
                }
            }
            return currentCustomer;
        }
    }

}
