using System;

namespace MyBankApp
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
