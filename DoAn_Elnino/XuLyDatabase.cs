using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_Elnino
{
        class XyLyDatabase
        {
            SqlConnection connect;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            public XyLyDatabase()
            {
                string ketnoi = @"Data Source = ELNINO\ELNINO; Initial catalog=DB_QLNhaThuoc; User ID= sa; Password = 123;";
                connect = new SqlConnection(ketnoi);
            }
            public DataTable LayDuLieu(string sql)
            {
                da = new SqlDataAdapter(sql, connect);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            // cap nhap du lieu ca 1 ban
            public void UpdateData(DataTable table)
            {
                SqlCommandBuilder combuider = new SqlCommandBuilder(da);
                da.Update(table);
            }
            public void UpdateData(string sql, DataTable table)
            {
                da = new SqlDataAdapter(sql, connect);
                SqlCommandBuilder combuider = new SqlCommandBuilder(da);
                da.Update(table);
            }

            // them xoa sua 1 domg = command
            public void ThemXoaSua(string sql)
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                SqlCommand com = new SqlCommand(sql, connect);
                com.ExecuteNonQuery();
                connect.Close();
            }
        }

}

