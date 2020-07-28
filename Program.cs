using System;

namespace MyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            BankController.RunBank();
            Customer first = new Customer("Kaosilisochukwu", "Nwizu", "kcharlse94@gmail.com");
            Customer second = new Customer("Obinwanne", "Nwizu", "kbibis@gmail.com");
            BankAccount acc1 = new BankAccount(first, 500, "savings");
            BankAccount bis1 = new BankAccount(second, 2700, "current");
            BankAccount bis2 = new BankAccount(second, 8900, "savings");
            bis1.MakeDeposite(bis1, 4409, DateTime.Now, "Investment");
            bis2.MakeWithdrawal(bis2, 4763, DateTime.Now, "Enjoyment");
            bis2.MakeWithdrawal(bis2, 2762, DateTime.Now.AddDays(1), "Donation");
            BankAccount acc2 = new BankAccount(first, 1000, "current");
            Console.WriteLine(acc1.AccountNumber);
            Console.WriteLine(acc1.AccountType);
            Console.WriteLine(acc2.AccountType);
            Console.WriteLine(acc2.AccountNumber);
            Console.WriteLine(acc2.AccountBalance);
            acc1.MakeDeposite(acc1, 1200, DateTime.Now, "savings");
            Console.WriteLine(acc1.AccountBalance);
            acc1.MakeWithdrawal(acc1, 500, DateTime.Now, "rent");
            acc1.TransferFunds(bis1, 200, DateTime.Now, "Levy");
            Console.WriteLine("\n");
            acc1.GetAccountBalance();
            acc1.GetTransactionDetails();
            acc2.GetTransactionDetails();
            Console.WriteLine(acc1.AccountBalance);
            Console.WriteLine(acc2.AccountBalance);
            Console.WriteLine(first.FirstName);
            bis1.GetTransactionDetails();
            Console.WriteLine("Hello World!");
            bis2.GetTransactionDetails();
            Console.WriteLine("\n");
            BankAccount.GetAllAccounts();
            BankAccount.GetAllCustomers();
            bis1.GetAccountBalance();
        }
    }
}
