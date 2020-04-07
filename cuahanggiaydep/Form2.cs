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
    public partial class FrmNhanVien : Form
    {
        DataTable tblNhanVien;
       
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Enabled = false;
            loadDataToGridview();
            Functions.FillCombo("select MaCV,TenCV from CongViec", cboMaCV, "MaCV", "TenCV");
            cboMaCV.SelectedIndex = -1;
            
        }
        private void loadDataToGridview()
        {
            string sql = "select *from NhanVien";
            tblNhanVien = Functions.GetDataToTable(sql);
            DataGridView_NhanVien.DataSource = tblNhanVien;

        }


        private void DataGridView_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaCV;
            txtMaNV.Text = DataGridView_NhanVien.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = DataGridView_NhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            if (DataGridView_NhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            mskNgaySinh.Text = DataGridView_NhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtDienThoai.Text = DataGridView_NhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtDiaChi.Text = DataGridView_NhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
          MaCV= DataGridView_NhanVien.CurrentRow.Cells["MaCV"].Value.ToString();
            
            cboMaCV.Text = Functions.GetFieldValues("select TenCV from CongViec where MaCV='"+MaCV+"'");
            txtMaNV.Enabled = false;
        }
        private void ResetValue()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            cboMaCV.Text = "";
            mskNgaySinh.Text = "";
            chkGioiTinh.Checked = false;
        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblNhanVien.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if(txtMaNV.Text=="")
            {
                MessageBox.Show("ban chua chon ma nhan vien");
            }
            if (MessageBox.Show("ban muon xoa khong", "thong bao",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
            {
                sql = "delete from NhanVien where MaNV ='" + txtMaNV.Text + "'";
                Functions.RunSQLDel(sql);
                loadDataToGridview();
                ResetValue();
            }
           
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            ResetValue();
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblNhanVien.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
                return;
            }
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("nhap ma nhan vien");
                txtMaNV.Focus();
                txtMaNV.Focus();
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("nhap ten nhan vien");
                txtTenNV.Focus();
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("nhap dia chi");
                txtDiaChi.Focus();

            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("nhap dien thoai");
                txtDienThoai.Focus();

            }
            if (chkGioiTinh.Checked == true)
                MessageBox.Show("nam");
            else
                MessageBox.Show("nu");
            chkGioiTinh.Focus();
            if (cboMaCV.Text == "")
            {
                MessageBox.Show("nhap ma cong viec");
                cboMaCV.Focus();
            }
            if (mskNgaySinh.Text == "")
            {
                MessageBox.Show("nhap ngay sinh");
                mskNgaySinh.Focus();
            }
            if (!Functions.IsDate(mskNgaySinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // mskNgaySinh.Text = "";
                mskNgaySinh.Focus();
                return;
            }

            sql = "select MaNV from NhanVien where MaNV ='" + txtMaNV.Text.Trim() + "'";
            if(Functions.CheckKey(sql))
            {
                MessageBox.Show("ma nay da co, ban nhap ma khac");
                txtMaNV.Focus();
                return;
            }
            sql = "insert into NhanVien values('" + txtMaNV.Text + "','" + txtTenNV.Text + "','" + txtDiaChi.Text + "','" + txtDienThoai.Text + "','" + mskNgaySinh.Text + "'," + chkGioiTinh.Checked + "','" + cboMaCV.Text + "')";
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
            if(tblNhanVien.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu");
            }

            if (txtMaNV.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã nhan vien nào", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaNV.Focus();
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhan vien", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtTenNV.Focus();
               
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
               
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                
            }
            if (chkGioiTinh.Checked == true)
            {
                MessageBox.Show("nhap gioi tinh", "thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkGioiTinh.Focus();
                
            }
            if (cboMaCV.Text == "")
            {
                MessageBox.Show("nhap mã công việc", "thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
               cboMaCV.Focus();
               
            }
            if (mskNgaySinh.Text == "")
            {
                MessageBox.Show("nhap ngay sinh", "thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
               mskNgaySinh.Focus();
                
            }
            if (!Functions.IsDate(mskNgaySinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaySinh.Text = "";
                mskNgaySinh.Focus();
                return;
            }
            sql = " UPDATE NhanVien SET TenNV =  '" + txtTenNV.Text.ToString() +
" ',DiaChi='" + txtDiaChi.Text.Trim().ToString() + "',DienThoai='" + txtDienThoai.Text.Trim().ToString() + "',GioiTinh='" + chkGioiTinh.Checked
.ToString() + "',NgaySinh='" + mskNgaySinh.Text.Trim().ToString() + "',MaCV='" + cboMaCV.Text.Trim().ToString() +
" 'WHERE MaNV='" + txtMaNV.Text + "'";
            Functions.RunSQLDel(sql);
            loadDataToGridview();
            ResetValue();

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            ResetValue();
            btn_huy.Enabled = false;
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            txtMaNV.Enabled = false;
        }

        
    } 
}
