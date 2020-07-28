using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank
{
    class BankController
    {
        public static void RunBank()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to the International Bank of Rivendell, Middle Earth\nHow Can We help you today?");
            Console.ForegroundColor = ConsoleColor.Blue;
            Register:
            Console.WriteLine("\t\tTo Register an account, press 'R'\n\t\tTo log into an existing account press 'L'\n\t\tTo exit type 'E'");
            string registerChoice = Console.ReadLine().ToLower();
            while(registerChoice != "r" && registerChoice != "l" && registerChoice != "e")
            {
                Console.WriteLine("Please type in a correct option");
                goto Register;
            }
            Console.WriteLine("Please corrctly fill the following fields");
            Name:
            Console.WriteLine("FirstName(must contain more than two(2) characters): ");
            string firstName = Console.ReadLine();
            while (!stringInputIsValid(firstName))
            {
                goto Name;
            }
            LastName:
            Console.WriteLine("Surname(must contain more than two(2) characters): ");
            string surName = Console.ReadLine();
            while (!stringInputIsValid(surName))
            {
                goto LastName;
            }
            Email:
            Console.WriteLine("Email(must contain more than two(2) characters): ");
            string email = Console.ReadLine();
            while (!stringInputIsValid(email))
            {
                goto Email;
            }
            Customer currentCustomer = new Customer(firstName, surName, email);
            Console.WriteLine($"Registration was successful for {currentCustomer.FirstName} {currentCustomer.LastName}\n\t\tYour login Id is {currentCustomer.CustomerId}");
            Console.ReadLine();
            Beginning:
            Console.WriteLine("\t\tTo create an account, press 'O'\n\t\tTo log into an existing account press 'L'");
            string choice = Console.ReadLine();
            while (choice.ToLower() == "o" || choice.ToLower() == "l")
            {
                if (choice.ToLower() == "o")
                {
                    //Console.WriteLine("Please corrctly fill the following fields");
                    //Console.WriteLine("FirstName: ");
                    //string firstName = Console.ReadLine();
                    //Console.WriteLine("Surname: ");
                    //string surName = Console.ReadLine();
                    //Console.WriteLine("Email: ");
                    //string email = Console.ReadLine();
                    AccountType:
                    Console.WriteLine("Account type: ");
                    Console.WriteLine("\tfor savings account, type 'S'\n\tfor current account, type 'C'\n\tto exit, type 'E'");
                    string input = Console.ReadLine().ToLower();
                    string accountType = input == "s" ? "savings"
                                         : input == "c" ? "current"
                                         : input == "e" ? "exit" : "continue";
                    if (accountType == "continue")
                        goto AccountType;
                    if (accountType == "exit")
                        break;
                    InitialDeposit:
                    Console.WriteLine("Initial Deposit: \n\t1000 and above for current account\n\t100 and above for savings account\n\tType '0' to exit");
                    decimal initalDeposit;
                    bool isDecimal = decimal.TryParse(Console.ReadLine(), out initalDeposit);
                    if (isDecimal)
                    {
                        if (initalDeposit == 0)
                            break;
                        if (accountType == "savings" && initalDeposit < 100)
                        {
                            goto InitialDeposit;
                        }
                        else if (accountType == "current" && initalDeposit < 1000)
                        {
                            goto InitialDeposit;
                        }
                    }
                    else if(!isDecimal)
                    {
                        goto InitialDeposit;
                    }
                    Customer newCustomer = new Customer(firstName, surName, email);
                    BankAccount newAccount = new BankAccount(newCustomer, initalDeposit, accountType);
                    Console.WriteLine($"A {newAccount.AccountType} account has been created for {newAccount.CustomerName} with an initial deposite of {newAccount.AccountBalance}\n\t\tAccount number: {newAccount.AccountNumber}");
                    BankAccount.GetAllAccounts();
                    newAccount.GetAccountBalance();
                    Console.WriteLine("\t\tTo perform another action type 'Y'\n\t\tTo exit type 'E'");
                    string anotherTransaction = Console.ReadLine();
                    if(anotherTransaction.ToLower() == "y")
                        goto Beginning;
                    if (anotherTransaction.ToLower() == "e")
                        break;
                }
                if (choice.ToLower() == "l")
                {
                    AccountNumber:
                    Console.WriteLine("Please type in your account number to login or '0' to exit");
                    int accountNumber= 0;
                   // customerAccount;
                    string accountNumberString = Console.ReadLine();
                    if (accountNumberString.Length == 10 && int.TryParse(accountNumberString, out accountNumber))
                    {
                        BankAccount customerAccount = BankAccount.Login(accountNumber);
                        Console.WriteLine(customerAccount.CustomerName);
                        //Console.ReadLine();
                        TransactionChoice:
                        Console.WriteLine($"Welcome {customerAccount.CustomerName}\n\t\tTo make deposit type 'D'\n\t\tTo make Withdrawal, type 'W'\n\t\tTo transfer Funds, type 'T'\n\t\tTo check your balance, type 'C'\n\t\tTo get transaction Details type 'G'\n\t\tTo exit, type 'E'");
                        string transactionOption = Console.ReadLine().ToLower();
                        if(transactionOption == "d")
                        {
                            DepositPoint:
                            Console.WriteLine("Please enter a minimum of '50'");
                            decimal depositAmount = 0;
                            string depositAmountString = Console.ReadLine();
                            if (decimal.TryParse(depositAmountString, out depositAmount))
                            {
                                Console.WriteLine("Please enter a note for this transaction");
                                string note = Console.ReadLine();
                                customerAccount.MakeDeposite(customerAccount, depositAmount, DateTime.Now, note);
                                Console.WriteLine($"Transaction successful");
                                Console.WriteLine($"You now have {customerAccount.AccountBalance} in your {customerAccount.AccountNumber} account.");
                            }
                            else
                                goto DepositPoint;      
                        }else if (transactionOption == "w")
                        {
                            WithdrawalPoint:
                            string accountType = customerAccount.AccountType;
                            decimal amountToWithdraw = 0;
                            decimal maximumWithrawalAmount = accountType == "savings" ? customerAccount.AccountBalance - 100 : customerAccount.AccountBalance;
                            Console.WriteLine($"You can make a maximum withdrawal of {maximumWithrawalAmount}");
                            string stringWithdrawalAmount = Console.ReadLine();
                            if(decimal.TryParse(stringWithdrawalAmount, out amountToWithdraw))
                            {
                                if (amountToWithdraw > maximumWithrawalAmount || amountToWithdraw < 1)
                                    goto WithdrawalPoint;
                                Console.WriteLine("Please enter a note for this transaction");
                                string note = Console.ReadLine();
                                customerAccount.MakeWithdrawal(customerAccount, amountToWithdraw, DateTime.Now, note);
                            }
                            Console.ReadLine();
                        }
                        else if (transactionOption == "t")
                        {
                            string accountType = customerAccount.AccountType;
                            TransferAmountPoint:
                            decimal amountToTransfer = 0;
                            decimal maximumWithrawalAmount = accountType == "savings" ? customerAccount.AccountBalance - 100 : customerAccount.AccountBalance;
                            Console.WriteLine($"How much do you want to transfer (You can make a maximum withdrawal of {maximumWithrawalAmount})");
                            string stringAmountToTransfer = Console.ReadLine();
                            if (decimal.TryParse(stringAmountToTransfer, out amountToTransfer))
                            {
                                if (amountToTransfer > maximumWithrawalAmount || amountToTransfer < 1)
                                    goto TransferAmountPoint;
                                Console.WriteLine("Please enter a note for this transaction");
                                string note = Console.ReadLine();
                                ReceiverPoint:
                                Console.WriteLine("Please type in a valid receiver account number or '0' to exit");
                                int receiverAccountNumber = 0;
                                string receiverAccountNumberString = Console.ReadLine();
                                if (accountNumberString.Length == 10 && int.TryParse(receiverAccountNumberString, out receiverAccountNumber))
                                {
                                    Console.WriteLine(receiverAccountNumber);
                                    List<BankAccount> allBankAccounts = BankAccount.getAllAccounts();
                                    foreach (var customer in allBankAccounts)
                                    {
                                        if (customer.AccountNumber == receiverAccountNumber)
                                        {
                                            customerAccount.TransferFunds(customer, amountToTransfer, DateTime.Now, note);
                                            //Console.WriteLine($"You have successfully transfered {amountToTransfer} to {customer.CustomerName}\n\t\tAccount balance: {customerAccount.AccountBalance}");
                                            goto TransferAmountPoint;
                                        }
                                    }
                                }
                                goto ReceiverPoint;
                            }
                            else
                                goto TransferAmountPoint;
                            
                        }
                        else if (transactionOption == "c")
                        {
                            customerAccount.GetAccountBalance();
                        }
                        else if (transactionOption == "g")
                        {
                            customerAccount.GetTransactionDetails();
                        }
                        else if (transactionOption == "e")
                        {
                            break;
                        }
                        else
                        {
                            goto TransactionChoice;
                        }
                    }
                    else if (int.TryParse(accountNumberString, out accountNumber) && accountNumber == 0)
                    {
                        break;
                    }
                    else
                       goto AccountNumber;
                    BankAccount bankAccount = BankAccount.Login(accountNumber);
                }
                Console.WriteLine("Please enter a valid key");
                choice = Console.ReadLine();
            }
            
        }

        static bool stringInputIsValid(string stringInput) => stringInput.Length > 2;
    }
}
