using System;
using System.Collections.Generic;
using System.Text;

namespace LunaBank.Domain
{
    public class Account
    {
        public string AccountNumber { get; }
        public Person Holder { get; }
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; }

        public Account(Person holder, string accountNumber = null)
        {
            if (holder == null)
                throw new ArgumentNullException("Account holder cannot be null.");

            Holder = holder;
            AccountNumber = accountNumber ?? GenerateAccountNumber();
            CreatedAt = DateTime.UtcNow;
            Balance = 0;
        }

        private string GenerateAccountNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") +
                new Random().Next(1000, 9999).ToString();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than 0");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than 0");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
        }

        public void Transfer(Account destinationAccount, decimal amount)
        {
            if (destinationAccount == null)
                throw new ArgumentException("The destination account cannot be null");

            if (destinationAccount == this)
                throw new ArgumentException("You cannot transfer to the same account");

            Withdraw(amount);

            destinationAccount.Deposit(amount);
        }

        public override string ToString()
        {
            return $"Account: {AccountNumber} | Holder: {Holder.Name} | Balance: {Balance:F2}";
        }
    }
}
