using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Scrypt;

namespace financial_app_api.Services.Security
{
    internal static class PasswordUtil
    {
        private static ScryptEncoder encoder = new ScryptEncoder();
        internal static string CalcHash(string password)
        {
            string hashedPassword = encoder.Encode(password);
            return hashedPassword;
        }
        internal static bool VerifyPassword(string password, string hashedPassword)
        {
            bool isValid = false;


            if (encoder.Compare(password, hashedPassword))
                isValid = true;

            return isValid;
        }
    }
}
