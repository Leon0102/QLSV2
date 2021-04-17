using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_BT2
{
    class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection cnn;
        public static DBHelper Instance
        {
            get
            {
                if(_Instance==null)
                {
                    string cnnstr = @"Data Source=DESKTOP-4GRVFSF\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True";
                    _Instance = new DBHelper(cnnstr);
                }
                return _Instance;
            }
            private set { }
        }
        public DBHelper(string s)
        {
            cnn = new SqlConnection(s);
        }
        public DataTable GetRecords(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cnn.Open();
                da.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public bool ExecuteDB(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
