using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MyBank
{
    class BankAccount
    {
        private static int accountNumber = 1058848950;
        public BankAccount(Customer customer, decimal initialDeposit, string accountType)
        {
            CustomerName = $"{customer.FirstName} {customer.LastName}";
            AccountType = accountType;
            AccountNumber = accountNumber;
            CustomerId = customer.CustomerId;
            accountNumber += 123;
            AllBankAccounts.Add(this);
            AllCustomers.Add(customer);
            MakeDeposite(this, initialDeposit, DateTime.Now, "initial Deposit");
        }

        public string CustomerName { get; set; }
        public string AccountType { get; }
        public decimal AccountBalance { get; set; }
        public int AccountNumber { get; }
        public int CustomerId { get; }

        private List<BankTransactions> AllTransactions = new List<BankTransactions>();

        private static List<BankAccount> AllBankAccounts = new List<BankAccount>();

        private static List<Customer> AllCustomers = new List<Customer>();
        public void MakeDeposite(BankAccount account, decimal amount, DateTime date, string note)
        {
            AccountBalance += amount;
            BankTransactions transaction = new BankTransactions(account, amount, AccountBalance, note, date);
            AllTransactions.Add(transaction);

        }
        public static void GetAllAccounts()
        {
            foreach (var transction in AllBankAccounts)
            {
                Console.WriteLine($"{transction.AccountType}\t {transction.AccountNumber}\t {transction.CustomerName}\t {transction.AccountBalance} ");

            }
        }
        public static List<BankAccount> getAllAccounts()
        {
            return BankAccount.AllBankAccounts;
        }
        public static BankAccount Login(int accountNumber)
        {
            BankAccount bankAccount = null;
            foreach (var account in AllBankAccounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }                
            }
            return bankAccount;
        }

        public static void GetAllCustomers()
        {
            foreach (var customer in AllCustomers)
            {
                Console.WriteLine($"{customer.CustomerId}\t {customer.FirstName}\t {customer.LastName}\t {customer.Email}");
            }
        }

        public void MakeWithdrawal(BankAccount account, decimal amount, DateTime date, string note)
        {
            AccountBalance -= amount;
            BankTransactions transaction = new BankTransactions(account, amount, AccountBalance, note, date);
            AllTransactions.Add(transaction);
        }

        public void TransferFunds(BankAccount recipient, decimal amount, DateTime date, string note)
        {
            foreach (var account in AllBankAccounts)
            {
                if(account.AccountNumber == recipient.AccountNumber)
                {
                    MakeWithdrawal(this, amount, DateTime.Now, note);
                    recipient.MakeDeposite(recipient, amount, DateTime.Now, note);
                    Console.WriteLine($"You have successfully transfered {amount} to {recipient.CustomerName}\n\t\tAccount balance: {AccountBalance}");
                    break;
                }
            }
        }

        public void GetTransactionDetails()
        {
            foreach (var transction in AllTransactions)
            {
                Console.WriteLine($"{transction.AccountType}\t {transction.AccountNumber}\t {transction.CustomerFullName}\t {transction.Amount}\t {transction.Note}\t {transction.Date} ");
            }
        }
        public void GetAccountBalance()
        {
            Console.WriteLine($"You have {AccountBalance} left in your account {AccountNumber}");
        }
        public static bool bankAccountExists(string accountNumberstring)
        {
            bool bankAccountExist = false;
            if (AllBankAccounts.Count < 1)
            {
                return bankAccountExist;
            }
            if (int.TryParse(accountNumberstring, out int accountNumber))
            {
                foreach (var bankAccount in AllBankAccounts)
                {
                    if (bankAccount.AccountNumber == accountNumber)
                        bankAccountExist = true;
                }
            }
            else
                bankAccountExist = false;

            return bankAccountExist;
        }
        public static BankAccount GetBankAccount(int accountNumber)
        {
            BankAccount customerBankAccount = null;
           foreach (var account in AllBankAccounts)
           {
                if (accountNumber == account.AccountNumber)
                    customerBankAccount = account;
           }
            return customerBankAccount;
        }

    }
}
