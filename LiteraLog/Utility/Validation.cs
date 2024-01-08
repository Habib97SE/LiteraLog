using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace LiteraLog.Utility
{
    public static class Validation
    {
        /// <summary>
        /// The password should contain at least 8 characters. 
        /// At least one uppercase, one lowercase and one special character(symbol). 
        /// </summary>
        /// 
        /// <param name="password">the password that user entered.</param>
        /// <returns>True if password passes the criteria, otherwise false.</returns>
        public static bool passwordIsValid (string password)
        {
            if (password.IsNullOrEmpty())
            {
                return false;
            }
            if (password.Length < 8)
            {
                return false;
            }
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            // Check if the password matches the pattern
            return Regex.IsMatch(password, pattern);
        }

        /// <summary>
        /// Check if the provided email address is valid or not.
        /// </summary>
        /// 
        /// <param name="email">the email address that user entered.</param>
        /// <returns>Returns true if it is valid, otherwise returns false.</returns>
        public static bool emailIsValid (string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
