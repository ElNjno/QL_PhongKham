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
    public partial class frmBenhAn : Form
    {
        XyLyDatabase db = new XyLyDatabase();
        DataTable dtBenhAn, dtBenhNhan, dtNhanVien;
        DataColumn[] keyBenhAn = new DataColumn[1];        
        public frmBenhAn()
        {
            InitializeComponent();
        }

        void BenhAn_Databiding()
        {
            txtMaBA.DataBindings.Add("Text", dtBenhAn, "MABA");
            txtNgayKham.DataBindings.Add("Text", dtBenhAn, "NGAYLAPBENHAN");
            txtTinhTrang.DataBindings.Add("Text", dtBenhAn, "TINHTRANGBENHNHAN");
            cboTenBenhNhan.DataBindings.Add("SelectedValue", dtBenhAn, "MABN");
            cboTenBS.DataBindings.Add("SelectedValue", dtBenhAn, "MANV");
        }
        public int checkDuLieuNhap()
        {
            if (txtMaBA.Text == "" || txtNgayKham.Text == "" || txtTinhTrang.Text == "" )
            {
                return 0;
            }
            return 1;
        }
        private void frmBenhAn_Load(object sender, EventArgs e)
        {
            txtMaBA.Enabled = txtNgayKham.Enabled = txtTinhTrang.Enabled = cboTenBenhNhan.Enabled = cboTenBS.Enabled = false;
            btnLuu.Visible = false;
            txtMaBA.Enabled = txtNgayKham.Enabled = txtTinhTrang.Enabled = false;
            dtBenhAn = db.LayDuLieu("select * from BENHAN");
            dtBenhNhan = db.LayDuLieu("select * from BENHNHAN");
            dtNhanVien = db.LayDuLieu("select * from NHANVIEN");

            keyBenhAn[0] = dtBenhAn.Columns[0];
            dtBenhAn.PrimaryKey = keyBenhAn;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtBenhAn;
            dataGridView1.Columns[0].DataPropertyName = "MABA";
            dataGridView1.Columns[3].DataPropertyName = "TINHTRANGBENHNHAN";
            dataGridView1.Columns[4].DataPropertyName = "NGAYLAPBENHAN";

            DataGridViewComboBoxColumn cboTenBN = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
            cboTenBN.DataSource = dtBenhNhan;
            cboTenBN.DisplayMember = "HOTEN";
            cboTenBN.ValueMember = "MABN"; // lien ket trong ban benhnhan
            cboTenBN.DataPropertyName = "MABN";

            DataGridViewComboBoxColumn cboBacSy = (DataGridViewComboBoxColumn)dataGridView1.Columns[2];
            cboBacSy.DataSource = dtNhanVien;
            cboBacSy.DisplayMember = "HOTEN";
            cboBacSy.ValueMember = "MANV"; // lien ket trong ban benhnhan
            cboBacSy.DataPropertyName = "MANV";

            // nap len cbo
            cboTenBS.DataSource = dtNhanVien;
            cboTenBS.DisplayMember = "HOTEN";
            cboTenBS.ValueMember = "MANV";
            cboTenBenhNhan.DataSource = dtBenhNhan;
            cboTenBenhNhan.DisplayMember = "HOTEN";
            cboTenBenhNhan.ValueMember = "MABN";
            BenhAn_Databiding();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaBA.Enabled = txtNgayKham.Enabled = txtTinhTrang.Enabled = cboTenBenhNhan.Enabled = cboTenBS.Enabled = true;
            txtMaBA.Enabled = txtNgayKham.Enabled = txtTinhTrang.Enabled =  true;
            txtMaBA.DataBindings.Clear();
            txtNgayKham.DataBindings.Clear();
            txtTinhTrang.DataBindings.Clear();
            cboTenBenhNhan.DataBindings.Clear();
            cboTenBS.DataBindings.Clear();

            txtNgayKham.Clear();
            txtMaBA.Clear();
            txtTinhTrang.Clear();
            btnLuu.Enabled = true;
            btnLuu.Visible = true;
            btnXoa.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (checkDuLieuNhap() == 1)
            {
                DataRow newrow = dtBenhAn.NewRow();
                newrow[0] = txtMaBA.Text;
                newrow[1] = cboTenBenhNhan.SelectedValue.ToString();
                newrow[2] = cboTenBS.SelectedValue.ToString();
                newrow[3] = txtTinhTrang.Text;
                newrow[4] = txtNgayKham.Text;
                dtBenhAn.Rows.Add(newrow);
                BenhAn_Databiding();
                btnLuu.Enabled = false;
                btnThem.Enabled =btnXoa.Enabled = true;
                try
                {
                    // set lai SqlDataAdapter
                    string sql = "select * from BENHAN";
                    db.UpdateData(sql, dtBenhAn);
                    MessageBox.Show("succsess");
                }
                catch (Exception)
                {
                    MessageBox.Show("Loi~");
                }
            }

            else
            {
                MessageBox.Show("Loi");
                BenhAn_Databiding();
                btnLuu.Enabled = false;
                btnThem.Enabled =  btnXoa.Enabled = true;
            }
            txtMaBA.Enabled = txtNgayKham.Enabled = txtTinhTrang.Enabled = cboTenBenhNhan.Enabled = cboTenBS.Enabled = false;
            btnLuu.Visible = false;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban Co Muon Xoa Khong", "Canh Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // check khoa ngoai
                DataTable dtSV = null;

                dtSV = db.LayDuLieu("select distinct MABA from BENHAN where MABA='" + txtMaBA.Text + "'");

                    DataRow r = dtBenhAn.Rows.Find(txtMaBA.Text);
                    if (r != null)
                        r.Delete();                
                    else                
                    MessageBox.Show("Loi~");
                    try
                    {
                        // set lai SqlDataAdapter
                        string sql = "select * from BENHAN";
                        db.UpdateData(sql, dtBenhAn);
                        MessageBox.Show("succsess");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Loi~");
                    }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenbn = cboTenBenhNhan.Text;
            string sql = "select * from V_XUATHDBNaaaaa(N'" + tenbn + "')";
            dtBenhAn = db.LayDuLieu(sql);
            frmXuatHoaDon a = new frmXuatHoaDon(dtBenhAn);
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tenbn = cboTenBenhNhan.Text;
            string sql = "select * from V_ThongTinXetNghiem(N'" + tenbn + "')";
            dtBenhAn = db.LayDuLieu(sql);
            frmXuatThongTinXetNghiem a = new frmXuatThongTinXetNghiem(dtBenhAn);
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmChiTietToaThuoc b = new frmChiTietToaThuoc();
            b.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDichVu b = new frmDichVu();
            b.ShowDialog();
        }
    }
}
