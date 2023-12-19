using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBhelper
    {

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-TVLPB8U;Initial Catalog=FitBlaze;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            return con;
        }
    }
}
