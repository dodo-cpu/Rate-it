using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    //By Johann
    public static class SecurityHelper
    {

        /// <summary>
        /// Returns true, if the input contains SQL commands and false otherwise
        /// </summary>
        /// <param name="stringToCheck">the input to check for SQL commands</param>
        /// <returns>true if input contains sql command</returns>
        public static bool ContainsSQLCommands(string stringToCheck)
        {
            bool containsSQLChars = false;

            string[] SQLStrings =
            {
                "'",
                "--",
                ";--",
                ";",
                "/*",
                "*/",
                "@@",
                "@",
                "char",
                "nchar",
                "varchar",
                "nvarchar",
                "alter",
                "begin",
                "cast",
                "create",
                "cursor",
                "declare",
                "delete",
                "drop",
                "end",
                "exec",
                "execute",
                "fetch",
                "insert",
                "kill",
                "select",
                "sys",
                "sysobjects",
                "syscolumns",
                "table",
                "update"
            };

            foreach (string SQLString in SQLStrings)
            {
                if (stringToCheck.Contains(SQLString))
                {
                    containsSQLChars = true;
                }
            }

            return containsSQLChars;
        }

        /// <summary>
        /// Returns a Hash of the input string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }


    }
}
