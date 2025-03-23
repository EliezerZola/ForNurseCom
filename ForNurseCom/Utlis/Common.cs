using System.Text;

namespace ForNurseCom.Utils
{
    public class Common
    {
        public static string Hashpassord(string s)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var asBystes = Encoding.Default.GetBytes(s);
            var hashed = sha.ComputeHash(asBystes);

            return Convert.ToBase64String(hashed);
        }
    }
}
