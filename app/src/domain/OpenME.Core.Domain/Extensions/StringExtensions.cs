using System.Text.RegularExpressions;

namespace OpenME.Core.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmailCorrectFormat(
            this string email
        )
        {
            string pattern = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    } 
}