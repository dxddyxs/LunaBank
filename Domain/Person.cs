using System;
using System.Collections.Generic;
using System.Text;

namespace LunaBank.Domain
{
    public class Person
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Nome { get; private set; }
        public string CPF { get; private set; }
    }
}
