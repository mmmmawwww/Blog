using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project_1._1.Controllers
{
    public class Hash
    {
        public static string GetHash(string text)
        {
            using (var hashAlg = MD5.Create()) // Создаем экземпляр класса реализующего алгоритм MD5
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text)); // Хешируем байты строки text
                var builder = new StringBuilder(hash.Length * 2); // Создаем экземпляр StringBuilder. Этот класс предназначен для эффективного конструирования строк
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2")); // Добавляем к строке очередной байт в виде строки в 16-й системе счисления
                }
                return builder.ToString(); // Возвращаем значение хеша
            }
        }
    }
}