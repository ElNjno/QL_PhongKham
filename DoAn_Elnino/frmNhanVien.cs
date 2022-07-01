using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Elnino
{
    public partial class frmNhanVien : Form
    {
        int xuly = 0;
        public frmNhanVien()
        {
            InitializeComponent();
        }

        XyLyDatabase db = new XyLyDatabase();
        DataTable dtNhanVien;
        DataColumn[] keyNhanVien = new DataColumn[1];

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuly = 1;
            txtDiaChi.Enabled = cboTrinhDo.Enabled = cboGioiT.Enabled = txtMaNV.Enabled = txtSDT.Enabled = txtTenNV.Enabled = true;
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            cboGioiT.DataBindings.Clear();
            cboTrinhDo.DataBindings.Clear();
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            btnLuu.Enabled = true;
            btnLuu.Visible = true;
            btnSua.Enabled = btnXoa.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Visible = true;
            txtDiaChi.Enabled = cboTrinhDo.Enabled = txtSDT.Enabled = txtTenNV.Enabled = true;
            dataGridView1.ReadOnly = true;
            btnLuu.Visible = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban Co Muon Xoa Khong", "Canh Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // check khoa ngoai
                DataTable dtSV = null;
                string sql = "select distinct MANV from NHANVIEN where MANV='" + txtMaNV.Text + "'";
                dtSV = db.LayDuLieu(sql);

                DataRow r = dtNhanVien.Rows.Find(txtMaNV.Text);
                if (r != null)
                    r.Delete();

                string data = "select MANV,HOTEN ,GIOITINH ,DIACHI ,DIENTHOAI,TRINHDOCHUYENMON from NHANVIEN";
                db.UpdateData(data, dtNhanVien);
                MessageBox.Show("succsess");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (xuly == 1)
            {
                if (checkDuLieuNhap() == 1)
                {
                    DataRow newrow = dtNhanVien.NewRow();
                    newrow[0] = txtMaNV.Text;
                    newrow[1] = txtTenNV.Text;
                    newrow[2] = cboGioiT.SelectedValue.ToString();
                    newrow[3] = txtDiaChi.Text;
                    newrow[4] = txtSDT.Text;
                    newrow[5] = cboTrinhDo.SelectedValue.ToString();
                    dtNhanVien.Rows.Add(newrow);
                    NhanVien_Databiding();
                    btnLuu.Enabled = false;
                    btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
                    txtMaNV.Clear();
                    txtTenNV.Clear();
                    txtSDT.Clear();
                    txtDiaChi.Clear();
                    try
                    {
                        string sql = "select MANV,HOTEN ,GIOITINH ,DIACHI ,DIENTHOAI,TRINHDOCHUYENMON from NHANVIEN";
                        db.UpdateData(sql, dtNhanVien);
                        MessageBox.Show("succsess");
                        txtDiaChi.Enabled = cboTrinhDo.Enabled = cboGioiT.Enabled = txtMaNV.Enabled = txtSDT.Enabled = txtTenNV.Enabled = false;
                        txtMaNV.DataBindings.Clear();
                        txtTenNV.DataBindings.Clear();
                        txtSDT.DataBindings.Clear();
                        txtDiaChi.DataBindings.Clear();
                        cboGioiT.DataBindings.Clear();
                        cboTrinhDo.DataBindings.Clear();
                        txtMaNV.Clear();
                        txtTenNV.Clear();
                        txtSDT.Clear();
                        txtDiaChi.Clear();
                        btnLuu.Enabled = false;
                        btnLuu.Visible = false;
                        NhanVien_Databiding();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Loi~");
                    }
                }
                else
                {
                    MessageBox.Show("Loi~");
                    txtDiaChi.Enabled = cboTrinhDo.Enabled = cboGioiT.Enabled = txtMaNV.Enabled = txtSDT.Enabled = txtTenNV.Enabled = false;
                    txtMaNV.DataBindings.Clear();
                    txtTenNV.DataBindings.Clear();
                    txtSDT.DataBindings.Clear();
                    txtDiaChi.DataBindings.Clear();
                    cboGioiT.DataBindings.Clear();
                    cboTrinhDo.DataBindings.Clear();
                    txtMaNV.Clear();
                    txtTenNV.Clear();
                    txtSDT.Clear();
                    txtDiaChi.Clear();
                    btnLuu.Enabled = false;
                    btnLuu.Visible = false;
                    NhanVien_Databiding();
                }
                xuly = 0;
            }
            else
            {
                dataGridView1.Refresh();
                btnLuu.Enabled = false;
                btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
                string data = "select MANV,HOTEN ,GIOITINH ,DIACHI ,DIENTHOAI,TRINHDOCHUYENMON from NHANVIEN";
                db.UpdateData(data, dtNhanVien);
                MessageBox.Show("succsess");
            }
            btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            btnLuu.Visible = false;

            string sql = "select MANV,HOTEN ,GIOITINH ,DIACHI ,DIENTHOAI,TRINHDOCHUYENMON from NHANVIEN";
            dtNhanVien = db.LayDuLieu(sql);
            keyNhanVien[0] = dtNhanVien.Columns[0];
            dtNhanVien.PrimaryKey = keyNhanVien;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtNhanVien;
            dataGridView1.Columns[0].DataPropertyName = "MANV";
            dataGridView1.Columns[1].DataPropertyName = "HOTEN";
            dataGridView1.Columns[3].DataPropertyName = "DIACHI";
            dataGridView1.Columns[4].DataPropertyName = "DIENTHOAI";

            DataGridViewComboBoxColumn cboSex = (DataGridViewComboBoxColumn)dataGridView1.Columns[2];
            cboSex.DataSource = dtNhanVien;
            cboSex.DisplayMember = "GIOITINH";
            cboSex.DataPropertyName = "GIOITINH";

            DataGridViewComboBoxColumn cboTD = (DataGridViewComboBoxColumn)dataGridView1.Columns[5];
            cboTD.DataSource = dtNhanVien;
            cboTD.DisplayMember = "TRINHDOCHUYENMON";
            cboTD.DataPropertyName = "TRINHDOCHUYENMON";
            cboGioiT.DataSource = dtNhanVien;
            cboGioiT.DisplayMember = "GIOITINH";
            cboGioiT.ValueMember = "GIOITINH";
            cboTrinhDo.DataSource = dtNhanVien;
            cboTrinhDo.DisplayMember = "TRINHDOCHUYENMON";
            cboTrinhDo.ValueMember = "TRINHDOCHUYENMON";
            cboTrinhDo.Enabled = cboGioiT.Enabled = txtMaNV.Enabled = txtSDT.Enabled = txtTenNV.Enabled = txtDiaChi.Enabled = false;
            NhanVien_Databiding();
        }

        void NhanVien_Databiding()
        {
            txtMaNV.DataBindings.Add("Text", dtNhanVien, "MANV");
            txtTenNV.DataBindings.Add("Text", dtNhanVien, "HOTEN");
            txtSDT.DataBindings.Add("Text", dtNhanVien, "DIENTHOAI");
            txtDiaChi.DataBindings.Add("Text", dtNhanVien, "DIACHI");
        }
        public int checkDuLieuNhap()
        {
            if (txtTenNV.Text == "" || txtMaNV.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
            {
                return 0;
            }
            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmShowLuong a = new frmShowLuong();
            a.Show();
        }
    }
}
