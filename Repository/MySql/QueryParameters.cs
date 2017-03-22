using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Repository.MySql
{
    public static class QueryParameters
    {
        public static object CreateBlankMySqlParams()
        {
            return new MySqlParameter();
        }
        public static object CreateMysqlQueryParams(string key, object value)
        {
            return new MySqlParameter(key, value);
        }
    }
}
