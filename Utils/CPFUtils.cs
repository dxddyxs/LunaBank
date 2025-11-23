using System;
using System.Collections.Generic;
using System.Text;

namespace LunaBank.Utils
{
    public static class CPFUtils
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (10 - i);

            int firstDigit = 11 - (sum % 11);
            if (firstDigit > 9) firstDigit = 0;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (11 - i);

            int secondDigit = 11 - (sum % 11);
            if (secondDigit > 9) secondDigit = 0;

            return (cpf[9] - '0' == firstDigit) && (cpf[10] - '0' == secondDigit);
        }
    }
}
