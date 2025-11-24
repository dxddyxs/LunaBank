using System;
using System.Collections.Generic;
using System.Text;

namespace LunaBank.Domain
{
    public class Bank
    {
        public string Name { get; private set; }
        public string BankCode { get; private set; }

        private List<Person> _customers;
        private List<Account> _accounts;

        public Bank(string name, string bankCode)
        {
            SetName(name);
            SetBankCode(bankCode);

            _customers = new List<Person>();
            _accounts = new List<Account>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Bank name cannot be null or empty.");

            Name = name;
        }

        public void SetBankCode(string bankCode)
        {
            if (string.IsNullOrWhiteSpace(bankCode) || bankCode.Length != 3)
                throw new ArgumentException("The bank code must have at least 3 characters.");

            BankCode = bankCode;
        }

        public Person RegisterCustomer(string name, string cpf) 
        {
            if (_customers.Any(c => c.CPF == cpf))
                throw new InvalidOperationException("A customer with this CPF already exists.");

            var customer = new Person(name, cpf);
            _customers.Add(customer);
            return customer;
        }

        public Account OpenAccount(string cpf, string password)
        {
            var customer = _customers.FirstOrDefault(c => c.CPF == cpf);
            if (customer == null)
                throw new InvalidOperationException("Customer not found. Please register them first.");

            var account = new Account(customer, password);
            _accounts.Add(account);
            return account;
        }

        public Account FindAccount(string accountNumber)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public Person FindCustomer(string cpf)
        {
            return _customers.FirstOrDefault(c => c.CPF == cpf);
        }

        public List<Account> GetAllAccounts() 
        {
            return new List<Account>(_accounts);
        }

        public List<Person> GetAllCustomers() 
        {
            return new List<Person>(_customers);
        }

        public List<Account> GetAccountsByCustomer(string cpf) 
        {
            return _accounts.Where(a => a.Holder.CPF == cpf).ToList();
        }

        public decimal GetTotalBankBalance() 
        {
            return _accounts.Sum(a => a.Balance);
        }

        public void ShowBankStatistics()
        {
            Console.WriteLine($"=== Statistics | {Name} ===");
            Console.WriteLine($"Total clients: {_customers.Count}");
            Console.WriteLine($"Total accounts: {_accounts.Count}");
            Console.WriteLine($"Total balance: R$ {GetTotalBankBalance():F2}");
            Console.WriteLine("=====================================");
        }
    }
}
