using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace WorkLogForm.Connection
{
    class Connection
    {
        private static Configuration cfg = null;

        public static Configuration getConfiguration()
        {
            if (cfg == null)
            {
                cfg = new Configuration();
                cfg.AddAssembly("ClassLibrary");
            }
            return cfg;
        }

        private static SqlConnection connection = null;

        public static SqlConnection getSqlConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection("UID=sa;PWD=iti240;Database=kjqb;server=115.24.161.202;");
            }
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show("连接失败："+e.Message);
                    return null;
                }
            }
            return connection;
        }

    }
}
