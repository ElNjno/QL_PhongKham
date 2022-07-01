using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.IO;

namespace DoAn_Elnino
{
    public partial class frmDuocPham : Form
    {
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        XyLyDatabase db = new XyLyDatabase();
        DataTable dtDuocPham;
        DataColumn[] keylop = new DataColumn[1];
        bool flot = false;
        DataSet ds;
        public frmDuocPham()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMa.DataBindings.Clear();
            txtTen.DataBindings.Clear();
            txtGia.DataBindings.Clear();
            txtNuocSX.DataBindings.Clear();
            txtMa.Clear();
            txtTen.Clear();
            txtGia.Clear();
            txtNuocSX.Clear();
            btnLuu.Enabled = txtMa.Enabled = txtTen.Enabled = txtGia.Enabled = txtNuocSX.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.ReadOnly = false;
            flot = true;
        }

        private void frmDuocPham_Load(object sender, EventArgs e)
        {
            dtDuocPham = db.LayDuLieu("Select * from DUOCPHAM");
            keylop[0] = dtDuocPham.Columns[0];
            dtDuocPham.PrimaryKey = keylop;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtDuocPham;
            dataGridView1.Columns[0].DataPropertyName = "MADP";
            dataGridView1.Columns[1].DataPropertyName = "TENDUOCPHAM";
            dataGridView1.Columns[2].DataPropertyName = "DONGIA";
            dataGridView1.Columns[3].DataPropertyName = "NUOCSANXUAT";
            DuocPham_Databiding();
            btnLuu.Enabled = txtMa.Enabled = txtTen.Enabled = txtGia.Enabled = txtNuocSX.Enabled = false;
        }
        void DuocPham_Databiding()
        {
            txtMa.DataBindings.Add("Text", dtDuocPham, "MADP");
            txtTen.DataBindings.Add("Text", dtDuocPham, "TENDUOCPHAM");
            txtGia.DataBindings.Add("Text", dtDuocPham, "DONGIA");
            txtNuocSX.DataBindings.Add("Text", dtDuocPham, "NUOCSANXUAT");
        }

        private void btnLuu_Click(object sender, System.EventArgs e)
        {
            if (flot)
            {
                if (txtMa.Text != "" || txtTen.Text != "" || txtGia.Text != "" || txtNuocSX.Text != "")
                {
                    DataRow newrow = dtDuocPham.NewRow();
                    newrow[0] = txtMa.Text;
                    newrow[1] = txtTen.Text;
                    newrow[2] = txtGia.Text;
                    newrow[3] = txtNuocSX.Text;
                    dtDuocPham.Rows.Add(newrow);
                }
                DuocPham_Databiding();
            }
            else
            {
                dataGridView1.Refresh();
            }
            txtMa.Enabled = txtTen.Enabled = txtGia.Enabled = txtNuocSX.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            flot = false;

        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
            btnLuu.Enabled = txtMa.Enabled = txtTen.Enabled = txtGia.Enabled = txtNuocSX.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            txtMa.Enabled = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            DataTable dtSV = null;
            if (MessageBox.Show("Bạn có chắc không?", "Cảnh báo xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //kiểm tra khoá ngoại
                //distinct không lấy dữ liệu trùng!
                dtSV = db.LayDuLieu("Select distinct MADP from DUOCPHAM where MADP='" + txtMa.Text + "'");

                DataRow r = dtDuocPham.Rows.Find(txtMa.Text);
                if (r != null)
                {
                    r.Delete();
                }
            }
        }

        private void btnLay_Click(object sender, System.EventArgs e)
        {
            try
            {
                db.UpdateData("select * from DUOCPHAM", dtDuocPham);
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }








    }
}
