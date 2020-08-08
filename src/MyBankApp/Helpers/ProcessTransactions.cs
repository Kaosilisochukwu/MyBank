using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankApp.Helpers
{
    public static class ProcessTransactions
    {
        public static string ProcessDeposit(Customer currentCustomer)
        {
            DepositPoint:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************************************");
            Console.WriteLine("*  Please enter a minimum of '50'  or 'E' to Return to previous Menu *");
            Console.WriteLine("**********************************************************************");
            decimal depositAmount = 0;
            string depositAmountString = Console.ReadLine();
            if (depositAmountString.ToLower() == "e")
                return "e";
            if (decimal.TryParse(depositAmountString, out depositAmount) && depositAmount >= 50)
            {
                DepositAccountNumber:
                Console.WriteLine("******************************************************************************");
                Console.WriteLine("*  Please enter your account number or type 'E'  to Return to previous Menu  *");
                Console.WriteLine("******************************************************************************");
                string bankAccountString = Console.ReadLine();
                if (bankAccountString.ToLower() == "e")
                    return "e";
                bool bankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                if (!bankAccountExist)
                    goto DepositAccountNumber;
                BankAccount customerBankAccount = BankAccount.GetBankAccount(int.Parse(bankAccountString));
                Console.WriteLine("Please enter a note for this transaction");
                string note = Console.ReadLine();
                customerBankAccount.MakeDeposite(customerBankAccount, depositAmount, DateTime.Now, note);
                Console.WriteLine($"Transaction successful");
                Console.WriteLine($"You now have {customerBankAccount.AccountBalance} in your {customerBankAccount.AccountNumber} account.");
                Console.WriteLine("*************************************************");
                Console.WriteLine("*  Do you want to perform another transaction?  *");
                Console.WriteLine("*************************************************");
                return "e";
            }
            else
                goto DepositPoint;
        }

        public static void ProcessTransfer(Customer currentCustomer)
        {

        }
        public static void ProcessWithdrawal(Customer currentCustomer)
        {

        }
    }
}
