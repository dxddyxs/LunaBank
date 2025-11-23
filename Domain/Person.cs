using LunaBank.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace LunaBank.Domain
{
    public class Person
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string CPF { get; private set; }

        public Person(string name, string cpf)
        {
            SetName(name);
            SetCPF(cpf);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.");
            Name = name;
        }

        public void SetCPF(string cpf)
        {
            if (!CPFUtils.IsValid(cpf))
                throw new ArgumentException("Invalid CPF.");
            CPF = cpf;
        }
    }
}
