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
    public partial class frmDichVu : Form
    {
        XyLyDatabase db = new XyLyDatabase();
        DataTable dtDichVu, dtChiTietDichVu, dtBenhAn;
        DataColumn[] keydtDichVu = new DataColumn[1];
        bool flag = false;
        public frmDichVu()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = true;

            disableText(false);
            txtKetQua.DataBindings.Clear();
            txtMaDV.DataBindings.Clear();
            txtngay.DataBindings.Clear();
            cboTenxetnghiem.DataBindings.Clear();
            comTenBN.DataBindings.Clear();
            txtKetQua.Clear();
            txtMaDV.Clear();
            txtngay.Clear();


            btnLuu.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = false;
        }
        public Boolean checkValidValue()
        {
            if (txtMaDV.Text == "" || txtKetQua.Text == "" || txtngay.Text == "")
            {

                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = false;

            if (!checkValidValue())
            {
                MessageBox.Show("nhập thiếu thông tin , nhập cho đủ coi");
            }
            else
            {

                if (flag)
                {
                    DataRow newRow = dtDichVu.NewRow();
                    newRow[0] = txtMaDV.Text;
                    newRow[1] = cboTenxetnghiem.SelectedValue.ToString();
                    newRow[2] = comTenBN.SelectedValue.ToString();
                    newRow[3] = txtKetQua.Text;
                    newRow[4] = txtngay.Text;
                    disableText(true);
                    try
                    {
                        dtDichVu.Rows.Add(newRow);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("chi tiết hóa đơn đã tồn tại!");
                        btnLuu.Enabled = true;
                        btnXoa.Enabled = btnSua.Enabled = false;
                        return;
                        throw;
                    }
                    DB_Databiding();
                }
                else
                {
                    disableText(true);
                    dataGridView1.Refresh();
                }
            }
            db.UpdateData("select * from DICHVU", dtDichVu);
            MessageBox.Show("cập nhật dữ liệu thành công");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            disableText(false);
            dataGridView1.ReadOnly = true;
            //dataBinding();
            flag = false;

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void disableText(bool enable)
        {
            txtMaDV.Enabled = cboTenxetnghiem.Enabled = comTenBN.Enabled = txtKetQua.Enabled = txtngay.Enabled = !enable;
        }
        private void frmDichVu_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = btnSua.Enabled =  false;
            disableText(true);

            dtDichVu = db.LayDuLieu("select * from DICHVU");
            dtChiTietDichVu = db.LayDuLieu("select * from CTDICHVU");
            dtBenhAn = db.LayDuLieu("select * from BENHAN");
            //dtBenhNhan = db.LayDuLieu("select * from BENHNHAN");
            keydtDichVu[0] = dtDichVu.Columns[0];
            dtDichVu.PrimaryKey = keydtDichVu;



            dataGridView1.DataSource = dtDichVu;
            dataGridView1.ReadOnly = true;

            cboTenxetnghiem.DataSource = dtChiTietDichVu;
            cboTenxetnghiem.DisplayMember = "LOAIXN";
            cboTenxetnghiem.ValueMember = "MAXN";

            comTenBN.DataSource = dtBenhAn;
            comTenBN.DisplayMember = "maba";
            comTenBN.ValueMember = "maba";

            DB_Databiding();
        }
        void DB_Databiding()
        {
            txtMaDV.DataBindings.Add("Text", dtDichVu, "MADV");
            cboTenxetnghiem.DataBindings.Add("SelectedValue", dtDichVu, "MAXN");
            comTenBN.DataBindings.Add("SelectedValue", dtDichVu, "MABA");
            txtKetQua.DataBindings.Add("Text", dtDichVu, "KETQUA");
            txtngay.DataBindings.Add("Text", dtDichVu, "NGAYXETNGHIEM");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            disableText(true);
            try
            {

                if (MessageBox.Show("có chắc là muốn xóa chưa", "cảnh báo xóa ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && dtDichVu.Rows.Count != 0)
                {

                    string maDV = txtMaDV.Text;
                    //string maDuocPham = cboMADP.SelectedValue.ToString();


                    DataRow r = null;

                    for (int i = 0; i < dtDichVu.Rows.Count; i++)
                    {
                        if (dtDichVu.Rows[i].ItemArray[0].ToString() == maDV.ToString())
                        {
                            r = dtDichVu.Rows[i];
                            break;
                        }
                    }

                    if (r != null)
                    {
                        r.Delete();
                        MessageBox.Show("đã xóa thành công");
                    }


                }
                else
                {
                    MessageBox.Show("còn gì nữa đâu mà xóa ");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("còn gì nữa đâu mà xóa ");
            }

        }
    }
}
