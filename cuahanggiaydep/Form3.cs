using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CuaHangGiayDep
{
    public partial class Form3 : Form
    {
        
        DataTable tblMua;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtMaMua.Enabled = false;

            loadDataToGridview();
        }
        private void loadDataToGridview()
        {
            string sql = "select * from Mua";
            tblMua = Functions.GetDataToTable(sql);

            DataGridView_Mua.DataSource = tblMua;

        }

        private void DataGridView_Mua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaMua.Text = DataGridView_Mua.CurrentRow.Cells["MaMua"].Value.ToString();
           txtTenMua.Text = DataGridView_Mua.CurrentRow.Cells["TenMua"].Value.ToString();
            txtMaMua.Enabled = false;
        }
        private void ResetValue()
        {
            txtMaMua.Text = "";
            txtTenMua.Text = "";
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblMua.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if(txtMaMua.Text=="")
            {
                MessageBox.Show("ban chua chon ma mua");
            }
            if(MessageBox.Show("ban co muon xoa khong?","thong bao",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                sql = "delete from Mua where MaMua='" + txtMaMua.Text + "'";
                Functions.RunSQLDel(sql);
                loadDataToGridview();
                ResetValue();
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            ResetValue();
            txtMaMua.Enabled = true;
            txtMaMua.Focus();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblMua.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
                return;
            }
            if (txtMaMua.Text == "")
            {
                MessageBox.Show("nhap ma mùa");
                txtMaMua.Focus();
                
            }
            if (txtTenMua.Text == "")
            {
                MessageBox.Show("nhap ten mùa");
                txtTenMua.Focus();
            }

             sql = "select MaMua from Mua where MaMua='" + txtMaMua.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mùa này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMua.Focus();
               
                return;
            }
            sql="insert into Mua Values('"+txtMaMua.Text+"','"+txtTenMua.Text+"')";
            Functions.RunSQLDel(sql);
            loadDataToGridview();
            ResetValue();
        }

        
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblMua.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if (txtMaMua.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã mùa nào", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaMua.Focus();
            }
            if (txtTenMua.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên mùa", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtTenMua.Focus();
                
            }
            sql = " UPDATE Mua SET TenMua =  '" + txtTenMua.Text.ToString() + "' WHERE MaMua='" + txtMaMua.Text + "'";
            Functions.RunSQLDel(sql);
            ResetValue();
            loadDataToGridview();
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            ResetValue();
            btn_huy.Enabled = false;
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            txtMaMua.Enabled = false;
        }
    }
}
