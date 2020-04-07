using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quanlykhachsan
{
    public partial class FrmKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        private string sql;
        public FrmKhachSan()
        {
            InitializeComponent();
        }

        private void FrmKhachSan_Load_1(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-R8P0OA2J\\SQLEXPRESS;Initial Catalog=QL_khachsan;Integrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();

            loadDataToGridview();
        }

        private void loadDataToGridview()
        {
            string sql = "select * from tblPhong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tabletblPhong = new DataTable();
            adp.Fill(tabletblPhong);


            DataGridView_tblPhong.DataSource = tabletblPhong;

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from tblPhong where Maphong = '" + txtMaphong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDataToGridview();
        }

        private void DataGridView_tblPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaphong.Text = DataGridView_tblPhong.CurrentRow.Cells["Maphong"].Value.ToString();
            txtTenphong.Text = DataGridView_tblPhong.CurrentRow.Cells["Tenphong"].Value.ToString();
            txtDongia.Text = DataGridView_tblPhong.CurrentRow.Cells["Dongia"].Value.ToString();
            txtMaphong.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            txtTenphong.Text = "";
            txtDongia.Text = "";
            txtMaphong.Text = "";
            txtMaphong.Enabled = true;
        }
        private bool CheckKey(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tablePhong = new DataTable();
            adp.Fill(tablePhong);
            if (tablePhong.Rows.Count > 0)
                return true;
            else return false;
        }
        private void btn_luu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaphong.Text == "")
            {
                MessageBox.Show("nhap ma phong");
                txtMaphong.Focus();
                return;
            }
            if (txtTenphong.Text == "")
            {
                MessageBox.Show("nhap ten phong");
                txtTenphong.Focus();
            }
            if (txtDongia.Text == "")
            {
                MessageBox.Show("nhap don gia");
                txtDongia.Focus();

            }
            sql = "select Maphong from tblPhong where Maphong='" + txtMaphong.Text + "'";
            if(CheckKey(sql))
            {
                MessageBox.Show("mã phòng này đã có, bạn nhập mã khác", "thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaphong.Focus();
                txtMaphong.Text = "";
                return;
            }
            else
            {
                sql = "insert into tblPhong values ('" + txtMaphong.Text + "','" + txtTenphong.Text + "'";
                if (txtDongia.Text != "")
                    sql = sql + "," + txtDongia.Text.Trim();
                sql = sql + ")";

                MessageBox.Show(sql);
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    loadDataToGridview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return;
            }
            
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Bạn có muốn thoát không ?", " Thông báo",

                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                System.Windows.Forms.DialogResult.Yes)

            Application.Exit();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql;
            
            if (txtMaphong.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã phòng nào" , " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (txtTenphong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên phòng", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtTenphong.Focus();
                return;
            }
            if (txtDongia.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đơn giá", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDongia.Focus();
                return;
            }
            sql = " UPDATE tblPhong SET Tenphong =  '" + txtTenphong.Text.ToString() +
" ',Dongia='" + txtDongia.Text.Trim().ToString() +"' WHERE Maphong='" + txtMaphong.Text +"'";

            MessageBox.Show(sql);
            
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                loadDataToGridview();
            
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            txtDongia.Text = "";
            txtMaphong.Text = "";
            txtTenphong.Text = "";
            btn_huy.Enabled = false;
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            txtMaphong.Enabled = false;
        }

        
    }
}
