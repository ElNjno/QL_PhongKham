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
    public partial class frmShowLuong : Form
    {
        public frmShowLuong()
        {
            InitializeComponent();
        }

        private void frmShowLuong_Load(object sender, EventArgs e)
        {
            LuongNhanVien rptLuong = new LuongNhanVien();
            rptLuong.SetDatabaseLogon("sa", "123", @"ELNINO\ELNINO", "DB_QLNhaThuoc");

            crystalReportViewer1.ReportSource = rptLuong;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();  

        }
    }
}
