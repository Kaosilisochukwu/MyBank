using MyBankApp.ClassLibrary;
using MyBankApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankApp
{
    class BankController
    {
        public static void RunBank()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("**********************************************************************************************");
            Console.WriteLine("*  Welcome to the International Bank of Rivendell, Middle Earth. How Can We help you today?  *");
            Console.WriteLine("**********************************************************************************************");
            Register:
            //Code to register a customer
            Console.ForegroundColor = ConsoleColor.Blue;            
            Console.WriteLine("\t\tTo Register an account, press 'R'\n\t\tTo log into an existing account press 'L'\n\t\tTo exit type 'E'");
            string registerChoice = Console.ReadLine().ToLower();
            if (registerChoice == "e")
                goto End;
            while (registerChoice != "r" && registerChoice != "l" && registerChoice != "e")
            {
                Console.WriteLine("Please type in a correct option");
                goto Register;
            }
            if(registerChoice == "r")
            {
                CustomerAuth.CreateCustomer();
                Console.WriteLine("What will you like to do next?");
                goto Register;
            }
            else if(registerChoice == "l")
            {
                //To login
                LoginDetails:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("**************************************************************************");
                Console.WriteLine("*  Please type in your valid login Id and Email to login or 'E' to exit  *");
                Console.WriteLine("**************************************************************************");
                Console.WriteLine("\t\t\tEnter your Email: ");
                string customerEmail = Console.ReadLine();
                if (customerEmail.ToLower() == "e")
                    goto Register;
                Console.WriteLine("\t\t\tEnter your password: ");
                string password = Console.ReadLine();
                if(!Customer.customerExists(password, customerEmail))
                {
                    Console.WriteLine("Please enter valid Details");
                    goto LoginDetails;
                }
                LoginDetails customerDetails = new LoginDetails(customerEmail, password);
                Customer currentCustomer = CustomerAuth.Login(customerDetails.Email, customerDetails.Password);
                if(currentCustomer == null)
                {
                    Console.WriteLine("Wrong user Input");
                    goto LoginDetails;
                }

                ActionCenter:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\t\tTo create an account, press 'O'\n\t\tTo make deposit type 'D'\n\t\tTo make Withdrawal, type 'W'\n\t\tTo transfer Funds, type 'T'\n\t\tTo check your balance, type 'C'\n\t\tTo get transaction Details type 'G'\n\t\tTo logout, type 'E'");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "e")
                    goto Register;
                if(choice.ToLower() == "o")
                {
                    AccountType:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("Account type: ");
                    Console.WriteLine("\tfor savings account, type 'S'\n\tfor current account, type 'C'\n\tto logout, type 'E'");
                    string input = Console.ReadLine().ToLower();
                    string accountType = input == "s" ? "savings"
                                         : input == "c" ? "current"
                                         : input == "e" ? "exit" : "continue";
                    if (accountType == "continue")
                        goto AccountType;
                    if (accountType == "exit")
                        goto ActionCenter;
                    InitialDeposit:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Initial Deposit: \n\t1000 and above for current account\n\t100 and above for savings account\n\tType '0' to logout");
                    decimal initalDeposit;
                    bool isDecimal = decimal.TryParse(Console.ReadLine(), out initalDeposit);
                    if (isDecimal)
                    {
                        if (initalDeposit == 0)
                        {
                            Console.WriteLine("\t\t***********************************");
                            Console.WriteLine("\t\t*  Please enter the right amount  *");
                            Console.WriteLine("\t\t***********************************");
                            goto ActionCenter;
                        }                    
                        if (accountType == "savings" && initalDeposit < 100)
                        {
                            Console.WriteLine("\t\t***********************************");
                            Console.WriteLine("\t\t*  Please enter the right amount  *");
                            Console.WriteLine("\t\t***********************************");
                            goto InitialDeposit;
                        }
                        else if (accountType == "current" && initalDeposit < 1000)
                        {
                            Console.WriteLine("\t\t***********************************");
                            Console.WriteLine("\t\t*  Please enter the right amount  *");
                            Console.WriteLine("\t\t***********************************");
                            goto InitialDeposit;
                        }
                    }
                    else if (!isDecimal)
                    {
                        goto InitialDeposit;
                    }

                    BankAccount newAccount = new BankAccount(currentCustomer, initalDeposit, accountType);
                    Console.WriteLine($"A {newAccount.AccountType} account has been created for {newAccount.CustomerName} with an initial deposite of {newAccount.AccountBalance}\n\t\tAccount number: {newAccount.AccountNumber}");
                    Console.WriteLine("Do you want to perform another transaction?");
                    goto ActionCenter;
                }
                else if (choice.ToLower() == "d")
                {
                    string nextAction = ProcessTransactions.ProcessDeposit(currentCustomer);
                    if (nextAction == "e")
                        goto ActionCenter;
                }
                else if (choice.ToLower() == "w")
                {
                    WithdrawalPoint:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*******************************************************************************");
                    Console.WriteLine("*  Please enter a valid amount to withdraw or 'E' to Return to previous Menu  *");
                    Console.WriteLine("*******************************************************************************");
                    decimal withdrawalAmount = 0;
                    string withdrawalAmountString = Console.ReadLine();
                    if (withdrawalAmountString.ToLower() == "e")
                        goto ActionCenter;
                    if (decimal.TryParse(withdrawalAmountString, out withdrawalAmount))
                    {
                        WithdrawalAccountNumber:
                        Console.WriteLine("*************************************************************************************************************");
                        Console.WriteLine("*  Please enter your account number or type 'E' to Return to previous Menu and then 'O' to open an account  *");
                        Console.WriteLine("*************************************************************************************************************");
                        string bankAccountString = Console.ReadLine();
                        if (bankAccountString.ToLower() == "e")
                            goto ActionCenter;
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
                        goto ActionCenter;
                    }
                    else
                        goto WithdrawalPoint;
                }
                else if (choice.ToLower() == "t")
                {
                    TranferPoint:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("************************************************************************************");
                    Console.WriteLine("*  Please enter the amount you want to Transfer or 'E' to Return to previous Menu  *");
                    Console.WriteLine("************************************************************************************");
                    decimal withdrawalAmount = 0;
                    string withdrawalAmountString = Console.ReadLine();
                    if (withdrawalAmountString.ToLower() == "e")
                        goto ActionCenter;
                    if (decimal.TryParse(withdrawalAmountString, out withdrawalAmount))
                    {
                        BenefactorAccountNumber:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("******************************************************************************");
                        Console.WriteLine("*  Please enter your account number or type 'E'  to Return to previous Menu  *");
                        Console.WriteLine("******************************************************************************");
                        string bankAccountString = Console.ReadLine();
                        if (bankAccountString.ToLower() == "e")
                            goto ActionCenter;
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
                            goto ActionCenter;
                        bool receiverankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                        if (!receiverankAccountExist)
                            goto ReceiverPoint;
                        if(bankAccountString == receiverBankAccountString)
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
                        goto ActionCenter;
                    }
                    else
                        goto TranferPoint;
                }
                else if (choice.ToLower() == "c")
                {
                    CustomerAccountNumber:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("******************************************************************************");
                    Console.WriteLine("*  Please enter your account number or type 'E'  to Return to previous Menu  *");
                    Console.WriteLine("******************************************************************************");
                    string bankAccountString = Console.ReadLine();
                    if (bankAccountString.ToLower() == "e")
                        goto ActionCenter;
                    bool bankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                    if (!bankAccountExist)
                        goto CustomerAccountNumber;
                    BankAccount customerBankAccount = BankAccount.GetBankAccount(int.Parse(bankAccountString));
                    customerBankAccount.GetAccountBalance();
                    Console.WriteLine("*************************************************");
                    Console.WriteLine("*  Do you want to perform another transaction?  *");
                    Console.WriteLine("*************************************************");
                    goto ActionCenter;
                }
                else if (choice.ToLower() == "g")
                {
                    CustomerAccountNumber:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("******************************************************************************");
                    Console.WriteLine("*  Please enter your account number or type 'E'  to Return to previous Menu  *");
                    Console.WriteLine("******************************************************************************");
                    string bankAccountString = Console.ReadLine();
                    if (bankAccountString.ToLower() == "e")
                        goto ActionCenter;
                    bool bankAccountExist = BankAccount.bankAccountExists(bankAccountString);
                    if (!bankAccountExist)
                        goto CustomerAccountNumber;
                    BankAccount customerBankAccount = BankAccount.GetBankAccount(int.Parse(bankAccountString));
                    customerBankAccount.GetTransactionDetails(customerBankAccount.AccountNumber);
                    Console.WriteLine("*************************************************");
                    Console.WriteLine("*  Do you want to perform another transaction?  *");
                    Console.WriteLine("*************************************************");
                    goto ActionCenter;
                }
                else if (choice.ToLower() == "e")
                {
                    CustomerAuth.Logout(currentCustomer);
                    goto ActionCenter;
                }
                else
                {
                    goto ActionCenter;
                }
            }
            End:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***************************************************");
            Console.WriteLine("*  Thanks for banking with us. Do visit us again  *");
            Console.WriteLine("***************************************************");
        }




       
    }
}
