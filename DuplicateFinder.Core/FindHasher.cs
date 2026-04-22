
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;

namespace DuplicateFinder.Core
{
    public class FindHasher
    {
        public string GetHash(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            using (FileStream fstream = File.OpenRead(path))
            {
                SHA256 sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(fstream);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
