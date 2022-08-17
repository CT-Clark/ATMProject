using System;

namespace ATMProjectLibrary
{
    public class Transaction
    {
        public decimal Amount;
        public DateTime Time;

        public Transaction(decimal amount, DateTime time)
        {
            Amount = amount;
            Time = time;
        }

        public override string ToString()
        {
            return($"Amount: ${Amount} | Time: {Time}");
        }
    }
}
