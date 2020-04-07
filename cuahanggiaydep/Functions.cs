using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace CuaHangGiayDep
{
    class Functions
    {
        public static SqlConnection con;
        public static void connection()
        {
             con= new SqlConnection();
            con.ConnectionString = "Data Source=LAPTOP-R8P0OA2J\\SQLEXPRESS;Initial Catalog=CuaHangGiayDep;Integrated Security=True";
            con.Open();

        }
        public static void Disconnect()
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            return tbl;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma,string ten)
        {
            
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tbl= new DataTable();
            adp.Fill(tbl);
            cbo.DisplayMember = ten;
            cbo.DataSource = tbl;
            cbo.ValueMember = ma; //Trường giá trị
            
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader;
            
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }


        public static bool IsDate(string date)
        {
            string[] elements = date.Split('/');
            if ((Convert.ToInt32(elements[0]) >= 1) && (Convert.ToInt32(elements[0]) <= 31) && (Convert.ToInt32(elements[1]) >= 1) && (Convert.ToInt32(elements[1]) <= 12) && (Convert.ToInt32(elements[2]) >= 1900))
                return true;
            else return false;
        }
        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
        public static void RunSQLDel(string sql)
        {
            SqlCommand cmd = new SqlCommand(); 
           
            cmd.Connection = Functions.con; 
            cmd.CommandText = sql; //Gán lệnh SQL
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter adp= new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            adp.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

    }
}
