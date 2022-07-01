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
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien a = new frmNhanVien();
            a.Show();
        }

        private void btnBenhNhan_Click(object sender, EventArgs e)
        {
            frmBenhNhan a = new frmBenhNhan();
            a.ShowDialog();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            frmChiTietDichVu b = new frmChiTietDichVu();
            b.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDuocPham b = new frmDuocPham();
            b.ShowDialog();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {


        }
    }
}
