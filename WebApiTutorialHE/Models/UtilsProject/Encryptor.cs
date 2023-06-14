using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace WebApiTutorialHE.Models.UtilsProject
{
    public class Encryptor
    {
        public static string MD5Hash(string data)
        {
            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] b = Encoding.UTF8.GetBytes(data);
            b = myMD5.ComputeHash(b);

            StringBuilder s = new StringBuilder();
            foreach (byte p in b)
            {
                s.Append(p.ToString("x").ToLower());
            }

            return s.ToString();
        }
        public static string SHA256Encode(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển đổi chuỗi thành mảng bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Tính toán giá trị băm
                byte[] hashBytes = sha256Hash.ComputeHash(inputBytes);

                // Chuyển đổi giá trị băm thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
