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
    public partial class frmChiTietDichVu : Form
    {
        XyLyDatabase db = new XyLyDatabase();
        DataTable dtCTDichVu;
        DataColumn[] keyDichVu = new DataColumn[1];
        bool themmoi = false;
        public frmChiTietDichVu()
        {
            InitializeComponent();
        }
        void CTDichVu_DataBiding()
        {
            txtMa.DataBindings.Add("Text", dtCTDichVu, "MAXN");
            txtXetNghiem.DataBindings.Add("Text", dtCTDichVu, "LOAIXN");
            txtGia.DataBindings.Add("Text", dtCTDichVu, "GIATIEN");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thêm không?", "Cảnh báo thêm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                themmoi = true;
                txtMa.DataBindings.Clear();
                txtXetNghiem.DataBindings.Clear();
                txtGia.DataBindings.Clear();
                txtMa.Clear();
                txtGia.Clear();
                txtMa.Enabled = txtGia.Enabled = txtXetNghiem.Enabled = true;
                btnLuu.Enabled = true;
                btnThem.Enabled = btnXoa.Enabled = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("Vậy thì suy nghĩ thêm đi nhé!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (themmoi)
            {
                if (txtMa.Text != "" || txtGia.Text != "")
                {
                    DataRow newrow = dtCTDichVu.NewRow();
                    newrow[0] = txtMa.Text;
                    newrow[1] = txtXetNghiem.Text;
                    newrow[2] = txtGia.Text;
                    dtCTDichVu.Rows.Add(newrow);
                }
                CTDichVu_DataBiding();
            }
            else
            {
                dataGridView1.Refresh();
            }
            txtMa.Enabled = txtGia.Enabled = txtXetNghiem.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.UpdateData("select * from CTDICHVU", dtCTDichVu);
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtSV = null;
            if (MessageBox.Show("Bạn có chắc không?", "Cảnh báo xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //kiểm tra khoá ngoại
                //distinct không lấy dữ liệu trùng!
                dtSV = db.LayDuLieu("Select distinct MAXN from CTDICHVU where MAXN='" + txtMa.Text + "'");

                    DataRow r = dtCTDichVu.Rows.Find(txtMa.Text);
                    if (r != null)
                    {
                        r.Delete();
                    }               
            }
        }

        private void frmChiTietDichVu_Load(object sender, EventArgs e)
        {
            dtCTDichVu = db.LayDuLieu("select * from CTDICHVU");
            keyDichVu[0] = dtCTDichVu.Columns[0];
            dtCTDichVu.PrimaryKey = keyDichVu;

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtCTDichVu;
            dataGridView1.Columns[0].DataPropertyName = "MAXN";
            dataGridView1.Columns[1].DataPropertyName = "LOAIXN";
            dataGridView1.Columns[2].DataPropertyName = "GIATIEN";
            CTDichVu_DataBiding();
        }

    }    
}
