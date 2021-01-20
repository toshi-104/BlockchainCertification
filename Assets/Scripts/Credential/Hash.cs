using System.Security.Cryptography;
using System.Text;

namespace Credential {
    public static class Hash {
        public static string GetHash(string message) {
            var encoded = Encoding.UTF8.GetBytes(message);
            var sha256 = new SHA256CryptoServiceProvider();
            var hash = sha256.ComputeHash(encoded);

            var hashText = new StringBuilder();
            foreach (var t in hash) {
                hashText.AppendFormat("{0:X2}", t);
            }

            return hashText.ToString();
        }
    }
}
