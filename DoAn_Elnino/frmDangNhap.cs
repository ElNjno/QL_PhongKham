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
    public partial class frmDangNhap : Form
    {
        XyLyDatabase kn = new XyLyDatabase();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            dt = kn.LayDuLieu("Select * from taiKhoan where  taikhoan = '" + txtuser.Text + "' and  pass = '" + txtpass.Text + "'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Xin chào quản lí " + txtuser.Text + "! Bạn đã đăng nhập thành công!", "Thông báo");
                frmTrangChu a = new frmTrangChu();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!", "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
