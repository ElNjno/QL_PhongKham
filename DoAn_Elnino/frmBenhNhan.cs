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
    public partial class frmBenhNhan : Form
    {
        public frmBenhNhan()
        {
            InitializeComponent();
        }
        XyLyDatabase db = new XyLyDatabase();
        DataTable dtBenhNhan;
        DataColumn[] keyBenhNhan = new DataColumn[1];
        int xuly = 0;

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuly = 1;
            txtMaBN.Enabled = txtSDT.Enabled = txtNamSinh.Enabled = txtSDT.Enabled = txtTenBN.Enabled = true;

            txtMaBN.DataBindings.Clear();
            txtNamSinh.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtTenBN.DataBindings.Clear();

            txtMaBN.Clear();
            txtNamSinh.Clear();
            txtSDT.Clear();
            txtTenBN.Clear();
            btnLuu.Enabled = true;
            btnLuu.Visible = true;
            btnSua.Enabled = btnXoa.Enabled = false;
        }

        void BenhNhan_Databiding()
        {
            txtMaBN.DataBindings.Add("Text", dtBenhNhan, "MABN");
            txtTenBN.DataBindings.Add("Text", dtBenhNhan, "HOTEN");
            txtNamSinh.DataBindings.Add("Text", dtBenhNhan, "CCCD");
            txtSDT.DataBindings.Add("Text", dtBenhNhan, "SDT");
            txtMaBN.Enabled = txtSDT.Enabled = txtNamSinh.Enabled = txtSDT.Enabled = txtTenBN.Enabled = false;
            dataGridView1.ReadOnly = true;
            btnLuu.Enabled = false;
            btnLuu.Visible = false;
        }

        private void frmBenhNhan_Load(object sender, EventArgs e)
        {
            string sql = "select MABN,HOTEN,CCCD,SDT from BENHNHAN";
            dtBenhNhan = db.LayDuLieu(sql);
            keyBenhNhan[0] = dtBenhNhan.Columns[0];
            dtBenhNhan.PrimaryKey = keyBenhNhan;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtBenhNhan;
            dataGridView1.Columns[0].DataPropertyName = "MABN";
            dataGridView1.Columns[1].DataPropertyName = "HOTEN";
            dataGridView1.Columns[2].DataPropertyName = "CCCD";
            dataGridView1.Columns[3].DataPropertyName = "SDT";
            BenhNhan_Databiding();
        }

        public int checkDuLieuNhap()
        {
            if (txtTenBN.Text == "" || txtSDT.Text == "" || txtNamSinh.Text == "" || txtSDT.Text == "")
            {
                return 0;
            }
            return 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            if (xuly == 1)
            {
                if (checkDuLieuNhap() == 1)
                {
                    DataRow newrow = dtBenhNhan.NewRow();
                    newrow[0] = txtMaBN.Text;
                    newrow[1] = txtTenBN.Text;
                    newrow[2] = txtNamSinh.Text;
                    newrow[3] = txtSDT.Text;
                    dtBenhNhan.Rows.Add(newrow);
                    BenhNhan_Databiding();
                    btnLuu.Enabled = false;
                    txtMaBN.Clear();
                    txtTenBN.Clear();
                    txtSDT.Clear();
                    txtNamSinh.Clear();
                    try
                    {
                        string sql = "select MABN,HOTEN,CCCD,SDT from BENHNHAN";
                        db.UpdateData(sql, dtBenhNhan);
                        MessageBox.Show("succsess");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Loi~");
                    }
                    txtMaBN.DataBindings.Clear();
                    txtNamSinh.DataBindings.Clear();
                    txtSDT.DataBindings.Clear();
                    txtTenBN.DataBindings.Clear();

                    txtMaBN.Clear();
                    txtNamSinh.Clear();
                    txtSDT.Clear();
                    txtTenBN.Clear();
                }
                else
                {
                    MessageBox.Show("Loi~");
                    txtMaBN.Enabled = txtSDT.Enabled = txtNamSinh.Enabled = txtSDT.Enabled = txtTenBN.Enabled = false;
                    txtMaBN.DataBindings.Clear();
                    txtTenBN.DataBindings.Clear();
                    txtSDT.DataBindings.Clear();
                    txtNamSinh.DataBindings.Clear();

                    txtMaBN.Clear();
                    txtTenBN.Clear();
                    txtSDT.Clear();
                    txtNamSinh.Clear();
                    btnLuu.Enabled = false;
                    btnLuu.Visible = false;
                    BenhNhan_Databiding();

                }
                btnSua.Enabled = btnXoa.Enabled = true;
            }
            else
            {
                dataGridView1.Refresh();
                btnLuu.Enabled = false;
                btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
                dataGridView1.Refresh();
                try
                {
                    // set lai SqlDataAdapter
                    string data = "select MABN,HOTEN,CCCD,SDT from BENHNHAN";
                    db.UpdateData(data, dtBenhNhan);
                    MessageBox.Show("succsess");
                }
                catch (Exception)
                {
                    MessageBox.Show("Loi~");
                }
            }
            BenhNhan_Databiding();
 
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban Co Muon Xoa Khong", "Canh Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // check khoa ngoai
                DataTable dtSV = null;
                string sql = "select distinct MABN from BENHNHAN where MABN='" + txtMaBN.Text + "'";
                dtSV = db.LayDuLieu(sql);
                DataRow r = dtBenhNhan.Rows.Find(txtMaBN.Text);
                if (r != null)
                    r.Delete();

                string data = "select MABN,HOTEN,CCCD,SDT from BENHNHAN";
                db.UpdateData(data, dtBenhNhan);
                MessageBox.Show("succsess");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Visible = true;
            btnLuu.Enabled= txtNamSinh.Enabled  = txtSDT.Enabled = txtTenBN.Enabled = true;
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmBenhAn b = new frmBenhAn();
            b.ShowDialog();
        }
    }
}
   

