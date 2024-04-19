using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BTL_QUANLY_NHANVIEN
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=DESKTOP-S7AOFL6\SQLEXPRESS;Initial Catalog=QLNS;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
    
}
