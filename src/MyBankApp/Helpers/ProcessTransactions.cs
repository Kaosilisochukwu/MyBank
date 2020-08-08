using MyBankApp.ClassLibrary;
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

        public static string ProcessTransfer(Customer currentCustomer)
        {
            TranferPoint:
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("*  Please enter the amount you want to Transfer or 'E' to Return to previous Menu  *");
            Console.WriteLine("************************************************************************************");
            decimal withdrawalAmount = 0;
            string withdrawalAmountString = Console.ReadLine();
            if (withdrawalAmountString.ToLower() == "e")
                return "e";
            if (decimal.TryParse(withdrawalAmountString, out withdrawalAmount))
            {
                BenefactorAccountNumber:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("******************************************************************************");
                Console.WriteLine("*  Please enter your account number or type 'E'  to Return to previous Menu  *");
                Console.WriteLine("******************************************************************************");
                string bankAccountString = Console.ReadLine();
                if (bankAccountString.ToLower() == "e")
                    return "e";
                bool bankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                if (!bankAccountExist)
                    goto BenefactorAccountNumber;
                BankAccount benefactorBankAccount = BankAccount.GetBankAccount(int.Parse(bankAccountString));
                decimal maximumWithdrawalAmount = benefactorBankAccount.AccountType == "savings" ? benefactorBankAccount.AccountBalance - 100 : benefactorBankAccount.AccountBalance;
                if (maximumWithdrawalAmount < withdrawalAmount)
                {
                    Console.WriteLine($"You can only make a maximum withdrawal of {maximumWithdrawalAmount}");
                    goto TranferPoint;
                }
                Console.WriteLine("Please enter a note for this transaction");
                string note = Console.ReadLine();
                ReceiverPoint:
                Console.WriteLine("************************************************************************************");
                Console.WriteLine("*  Please enter reciever's account number or type 'E'  to Return to previous Menu  *");
                Console.WriteLine("************************************************************************************");
                string receiverBankAccountString = Console.ReadLine();
                if (bankAccountString.ToLower() == "e")
                    return "e";
                bool receiverankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                if (!receiverankAccountExist)
                    goto ReceiverPoint;
                if (bankAccountString == receiverBankAccountString)
                {
                    Console.WriteLine("Invalid transaction");
                    goto TranferPoint;
                }
                BankAccount receiverBankAccount = BankAccount.GetBankAccount(int.Parse(receiverBankAccountString));
                benefactorBankAccount.TransferFunds(receiverBankAccount, withdrawalAmount, DateTime.Now, note);
                Console.WriteLine($"Transaction successful");
                Console.WriteLine($"You now have {benefactorBankAccount.AccountBalance} in your {benefactorBankAccount.AccountNumber} account.");
                Console.WriteLine("*************************************************");
                Console.WriteLine("*  Do you want to perform another transaction?  *");
                Console.WriteLine("*************************************************");
                return "e";
            }
            else
                goto TranferPoint;
        }
        public static string ProcessWithdrawal(Customer currentCustomer)
        {


            WithdrawalPoint:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine("*  Please enter a valid amount to withdraw or 'E' to Return to previous Menu  *");
            Console.WriteLine("*******************************************************************************");
            decimal withdrawalAmount = 0;
            string withdrawalAmountString = Console.ReadLine();
            if (withdrawalAmountString.ToLower() == "e")
                return "e";
            if (decimal.TryParse(withdrawalAmountString, out withdrawalAmount))
            {
                WithdrawalAccountNumber:
                Console.WriteLine("*************************************************************************************************************");
                Console.WriteLine("*  Please enter your account number or type 'E' to Return to previous Menu and then 'O' to open an account  *");
                Console.WriteLine("*************************************************************************************************************");
                string bankAccountString = Console.ReadLine();
                if (bankAccountString.ToLower() == "e")
                    return "e";
                bool bankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                if (!bankAccountExist)
                    goto WithdrawalAccountNumber;
                BankAccount customerBankAccount = BankAccount.GetBankAccount(int.Parse(bankAccountString));
                decimal maximumWithdrawalAmount = customerBankAccount.AccountType == "savings" ? customerBankAccount.AccountBalance - 100 : customerBankAccount.AccountBalance;
                if (maximumWithdrawalAmount < withdrawalAmount)
                {
                    Console.WriteLine($"You can only make a maximum withdrawal of {maximumWithdrawalAmount}");
                    goto WithdrawalPoint;
                }
                Console.WriteLine("Please enter a note for this transaction");
                string note = Console.ReadLine();
                customerBankAccount.MakeWithdrawal(customerBankAccount, withdrawalAmount, DateTime.Now, note);
                Console.WriteLine($"Transaction successful");
                Console.WriteLine($"You now have {customerBankAccount.AccountBalance} in your {customerBankAccount.AccountNumber} account.");
                Console.WriteLine("*************************************************");
                Console.WriteLine("*  Do you want to perform another transaction?  *");
                Console.WriteLine("*************************************************");
                return "e";
            }
            else
                goto WithdrawalPoint;

        }
    }
}
