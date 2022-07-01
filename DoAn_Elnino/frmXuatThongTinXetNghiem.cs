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
    public partial class frmXuatThongTinXetNghiem : Form
    {
        public frmXuatThongTinXetNghiem(DataTable dt)
        {
            InitializeComponent();
            XuatHDDV rptLuong = new XuatHDDV();

            rptLuong.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rptLuong;
            crystalReportViewer1.Refresh();  

        }

        private void frmXuatThongTinXetNghiem_Load(object sender, EventArgs e)
        {

        }
    }
}
