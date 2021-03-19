using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Data.Util
{
    public class Criptografia
    {
        public static string MD5Encrypt(string valor)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(valor));

            var result = string.Empty;
            foreach (var item in hash)
            {
                result += item.ToString("x2"); //hexadecimal
            }

            return result;
        }
    }
}
