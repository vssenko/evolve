using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure
{
    public class HashProvider : IHashProvider
    {
        public string GenerateHash(string hashObject)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(hashObject);
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
