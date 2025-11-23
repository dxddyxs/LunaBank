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

            cpf = cpf.Replace(".", "").Replace("-", "");

            return cpf.Length == 11;
        }
    }
}
