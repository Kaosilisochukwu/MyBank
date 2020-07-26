using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank
{
    class BankTransactions
    {
        public BankTransactions(BankAccount customerAccount, decimal amountTransacted, decimal balance, string note, DateTime date)
        {
            AccountNumber = customerAccount.AccountNumber;
            CustomerFullName = customerAccount.CustomerName;
            AccountType = customerAccount.AccountType;
            Amount = amountTransacted;
            Note = note;
            Date = date;
            AccountBalance = amountTransacted;
        }

        public string AccountType { get; }
        public string CustomerFullName { get; } 
        public int AccountNumber { get; }   
        public decimal Amount { get; }
        public string Note { get; } 
        public DateTime Date { get; }
        public decimal AccountBalance { get; set; }
    }
}
