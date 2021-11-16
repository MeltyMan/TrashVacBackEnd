using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core
{
    public class Common
    {
        public static string GenerateMd5Hash(string text)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder("");
            for (var i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
