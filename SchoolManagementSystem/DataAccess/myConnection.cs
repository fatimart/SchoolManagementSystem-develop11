using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace SchoolManagementSystem.DataAccess
{
    public class myConnection
    {
        public static SqlConnection GetConnection ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            return con;
        }
    }
}
