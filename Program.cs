using System;

namespace MyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            BankController.RunBank();
            BankAccount.GetAllAccounts();
            BankAccount.GetAllCustomers();
        }
    }
}
