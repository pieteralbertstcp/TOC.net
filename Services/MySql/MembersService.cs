using System.Security.Cryptography;
using System.Text;
using Repository.MySql;

namespace Services.MySql
{
    public class MembersService : wwsaService<members>
    {
        public bool IsValidMember(string username, string password)
        {
            if (username == null || password == null) return false;
            var hashPassword = GenerateMd5Hash(password);
            var result = Where(x => x.username == username && x.password == hashPassword);
            return result != null;
        }

        private static string GenerateMd5Hash(string input)
        {
            if (input == null) return null;
            var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
