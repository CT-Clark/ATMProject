using System;
using System.Collections.Generic;

namespace ATMProjectLibrary
{
    public class BankAccount
    {
        public string Name { get; set; }
        private int PIN { get; set; }
        public int CardNumber { get; set; }
        public List<Transaction> TransactionList { get; set; } = new List<Transaction>();
        public bool LoggedIn { get; set; } = false;
        public int DailyTransactionsLeft = 10;
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (Transaction t in TransactionList)
                {
                    balance += t.Amount;
                }
                return balance;
            }
        }

        public BankAccount(string name, int pin, int cardNumber, decimal initialAmount)
        {
            Name = name;
            PIN = pin;
            CardNumber = cardNumber;
            TransactionList.Add(new Transaction(initialAmount, DateTime.Now));
        }

        public void MakeAWithdrawal(decimal amount)
        {
            try
            {
                if (amount > Balance) throw new ArgumentOutOfRangeException();
                if (amount > 1000) throw new ArgumentOutOfRangeException();
                if (amount < 0) throw new ArgumentOutOfRangeException();
                if (DailyTransactionsLeft < 1) throw new InvalidOperationException();


                TransactionList.Add(new Transaction(amount, DateTime.Now));

            }
            catch (ArgumentOutOfRangeException) when (amount > Balance)
            {
                Console.WriteLine("There are insufficient funds in this account.");
            }
            catch (ArgumentOutOfRangeException) when (amount > 1000)
            {
                Console.WriteLine("The amount requested exceeds the maximum requestable amount.");
            }
            catch (ArgumentOutOfRangeException) when (amount < 0)
            {
                Console.WriteLine("You cannot withdraw a negative amount.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There are an insufficient amount of transactions available. Please wait until tomorrow.");
            }
        }

        public void MakeADeposit(decimal amount)
        {
            try
            {
                if (amount < 0) throw new ArgumentOutOfRangeException();

                TransactionList.Add(new Transaction(amount, DateTime.Now));
            }
            catch (ArgumentOutOfRangeException) when (amount < 0)
            {
                Console.WriteLine("You cannot deposit a negative amount.");
            }
        }

        public bool VerifyPIN(int pin)
        {
            return (pin == PIN);
        }
    }
}
