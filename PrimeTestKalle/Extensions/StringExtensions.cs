using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTestKalle.Extensions
{
    public static class StringExtensions
    {

        private const string VALID_NUMBER_CHARS = "0123456789";

        public static bool IsNumeric(this string s)
        {
            var result = false;
            if (s.Length > 0)
            {
                result = true;
                for (var i = 0; i < s.Length; i++)
                {
                    var currentChar = s.Substring(i, 1);
                    var charOk = false;
                    for (var a = 0; a < VALID_NUMBER_CHARS.Length; a++)
                    {
                        if (currentChar == VALID_NUMBER_CHARS.Substring(a, 1))
                        {
                            charOk = true;
                            break;
                        }
                    }

                    if (!charOk)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static int ToInt32(this string s)
        {
            return Convert.ToInt32(s);
        }
    }
}
