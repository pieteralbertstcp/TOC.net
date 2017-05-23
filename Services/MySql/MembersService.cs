using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Repository.MySql;

namespace Services.MySql
{
    public class MembersService : wwsaService<members>
    {
        public dynamic IsValidMember(string username, string password)
        {
            if (username == null || password == null) return false;
            var hashPassword = GenerateMd5Hash(password);
            return Where(x => x.username == username && x.password == hashPassword && x.is_active.Value).FirstOrDefault();
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

	    public dynamic GetUsernameBasicInfo(string username)
	    {
			var user = Where(x => x.username == username.Trim()).FirstOrDefault();
			dynamic result = new ExpandoObject();
		    if (user == null) return null;
		    result.username = user.username;
		    result.firstname = user.first_name;
		    result.lastname = user.last_name;
		    result.id = user.id;
			return result;
	    }
    }
}
