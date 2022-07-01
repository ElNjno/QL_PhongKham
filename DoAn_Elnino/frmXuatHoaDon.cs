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
    public partial class frmXuatHoaDon : Form
    {
        public frmXuatHoaDon(DataTable dt)
        {
            InitializeComponent();           
            XuatHD rptLuong = new XuatHD();
            rptLuong.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rptLuong;
            crystalReportViewer1.RefreshReport();

        }

        private void frmXuatHoaDon_Load(object sender, EventArgs e)
        {
            //XuatHD reportDocument = new XuatHD();
            //reportDocument.Load(@"tenReport.rpt");
            //reportDocument.SetDataSource(dataTable);
            //crystalReportViewer1.ReportSource = reportDocument;
            //crystalReportViewer1.RefreshReport();
        }

    }
}
