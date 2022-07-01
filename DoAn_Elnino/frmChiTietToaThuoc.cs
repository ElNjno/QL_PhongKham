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
    public partial class frmChiTietToaThuoc : Form
    {
        XyLyDatabase db = new XyLyDatabase();
        DataTable dtChiTietToaThuoc, dtToathuoc, dtDuocpham;
        DataColumn[] keyChiTietToaThuoc = new DataColumn[2];
        bool flag = false;
        public frmChiTietToaThuoc()
        {
            InitializeComponent();
        }
        public Boolean checkValidValue()
        {
            if (txtGhiChu.Text == "" || txtsoluong.Text == "" || txtThanhTien.Text == "")
            {

                return false;
            }
            return true;
        }
        private void frmChiTietToaThuoc_Load(object sender, EventArgs e)
        {

            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;

            disableText(true);

            dtChiTietToaThuoc = db.LayDuLieu("select * from CHITIETTOATHUOC");
            dtToathuoc = db.LayDuLieu("select * from TOATHUOC");
            dtDuocpham = db.LayDuLieu("select * from DUOCPHAM");

            keyChiTietToaThuoc[0] = dtChiTietToaThuoc.Columns[0];
            keyChiTietToaThuoc[1] = dtChiTietToaThuoc.Columns[1];
            dtChiTietToaThuoc.PrimaryKey = keyChiTietToaThuoc;



            dataGridView1.DataSource = dtChiTietToaThuoc;
            dataGridView1.ReadOnly = true;

            cbbMaToaThuoc.DataSource = dtToathuoc;
            cbbMaToaThuoc.DisplayMember = "matt";
            cbbMaToaThuoc.ValueMember = "matt";

            DataTable dtba = db.LayDuLieu("select * from benhan");
            cboMABA1.DataSource = dtba;
            cboMABA1.DisplayMember = "MABA";
            cboMABA1.ValueMember = "MABA";


            cboMADP.DataSource = dtDuocpham;
            cboMADP.DisplayMember = "tenduocpham";
            cboMADP.ValueMember = "madp";

            DB_Databiding();

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
                    DataRow newRow = dtChiTietToaThuoc.NewRow();
                    newRow[0] = cbbMaToaThuoc.SelectedValue.ToString();
                    newRow[1] = cboMADP.SelectedValue.ToString();
                    try
                    {
                        newRow[2] = txtsoluong.Text;
                        newRow[3] = txtThanhTien.Text;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("số lượng hoặc thành tiền thì nhập số dô ");
                        btnXoa.Enabled = btnSua.Enabled = false;
                        btnThem.Enabled = btnLuu.Enabled = true;
                        return;
                    }


                    newRow[4] = txtGhiChu.Text;
                    disableText(true);
                    try
                    {
                        dtChiTietToaThuoc.Rows.Add(newRow);
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
            db.UpdateData("select * from CHITIETTOATHUOC", dtChiTietToaThuoc);
        }
        public void disableText(bool enable)
        {
            cbbMaToaThuoc.Enabled = cboMADP.Enabled = txtsoluong.Enabled = txtThanhTien.Enabled = txtGhiChu.Enabled = !enable;
        }
        void DB_Databiding()
        {
            cbbMaToaThuoc.DataBindings.Add("SelectedValue", dtChiTietToaThuoc, "MATT");
            cboMADP.DataBindings.Add("SelectedValue", dtChiTietToaThuoc, "MADP");
            txtsoluong.DataBindings.Add("Text", dtChiTietToaThuoc, "SOLUONG");
            txtThanhTien.DataBindings.Add("Text", dtChiTietToaThuoc, "THANHTIEN");
            txtGhiChu.DataBindings.Add("Text", dtChiTietToaThuoc, "GHICHU");

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = true;

            disableText(false);
            cbbMaToaThuoc.DataBindings.Clear();
            txtsoluong.DataBindings.Clear();
            txtThanhTien.DataBindings.Clear();
            cboMADP.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            txtsoluong.Clear();
            txtThanhTien.Clear();
            txtGhiChu.Clear();

            btnLuu.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = false;


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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            disableText(true);
            try
            {

                if (MessageBox.Show("có chắc là muốn xóa chưa", "cảnh báo xóa ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && dtChiTietToaThuoc.Rows.Count != 0)
                {
                    string maToaThuoc = cbbMaToaThuoc.Text;
                    string maDuocPham = cboMADP.SelectedValue.ToString();


                    DataRow r = null;

                    for (int i = 0; i < dtChiTietToaThuoc.Rows.Count; i++)
                    {
                        if (dtChiTietToaThuoc.Rows[i].ItemArray[0].ToString() == maToaThuoc.ToString() && dtChiTietToaThuoc.Rows[i].ItemArray[1].ToString() == maDuocPham.ToString())
                        {
                            r = dtChiTietToaThuoc.Rows[i];
                            break;
                        }
                    }
                    //DataRow r = dtChiTietToaThuoc.Rows.Find(cbbMaToaThuoc.Text);
                    //DataRow r = dtChiTietToaThuoc.Rows[0];


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

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMATT1.Text == "")
            {
                MessageBox.Show("nho dien du thong tin nha");
                return;
            }
            else
            {
                DataRow newRow = dtToathuoc.NewRow();
                newRow[0] = txtMATT1.Text;
                newRow[1] = cboMABA1.SelectedValue.ToString();
                dtToathuoc.Rows.Add(newRow);

                try
                {
                    db.UpdateData("select * from TOATHUOC ", dtToathuoc);
                    MessageBox.Show("cập nhật dữ liệu thành công");
                }
                catch (Exception)
                {
                    MessageBox.Show("cập nhật dữ liệu THấT BạI");
                    throw;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBenhAn b = new frmBenhAn();
            b.ShowDialog();
        }

    }
}
