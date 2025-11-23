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
        private string _password;

        public Account(Person holder, string password, string accountNumber = null)
        {
            if (holder == null)
                throw new ArgumentNullException("Account holder cannot be null.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
                throw new ArgumentException("The password must be at least 4 charracters long");


            Holder = holder;
            _password = password;
            AccountNumber = accountNumber ?? GenerateAccountNumber();
            CreatedAt = DateTime.UtcNow;
            Balance = 0;
        }

        private bool Authenticate(string password)
        {
            return _password == password;
        }

        private string GenerateAccountNumber()
        {
            return new Random().Next(10000, 99999).ToString();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than 0");

            Balance += amount;
        }

        public void Withdraw(decimal amount, string password)
        {
            if (!Authenticate(password))
                throw new UnauthorizedAccessException("incorrect password");

            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than 0");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
        }

        public void Transfer(Account destinationAccount, decimal amount, string password)
        {
            if (!Authenticate(password))
                throw new UnauthorizedAccessException("incorrect password");

            if (destinationAccount == null)
                throw new ArgumentException("The destination account cannot be null");

            if (destinationAccount == this)
                throw new ArgumentException("You cannot transfer to the same account");

            Withdraw(amount, password);
            destinationAccount.Deposit(amount);
        }

        public decimal CheckBalance(string password)
        {
            if (!Authenticate(password))
                throw new UnauthorizedAccessException("incorrect password");

            return Balance;
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!Authenticate(currentPassword))
                throw new UnauthorizedAccessException("incorrect password");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 4)
                throw new ArgumentException("The new password must be at least 4 characters long.");

            _password = newPassword;
        }

        public override string ToString()
        {
            return $"Account: {AccountNumber} | Holder: {Holder.Name}";
        }
    }
}
