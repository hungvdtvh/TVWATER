using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HDDT_CTNS
{
    public partial class masterReport : DevExpress.XtraReports.UI.XtraReport
    {
        public masterReport()
        {
            InitializeComponent();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           ((XRSubreport)sender).ReportSource.FilterString = "[stt_order] = 10";
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           ((XRSubreport)sender).ReportSource.FilterString = "[stt_order] = 20";
        }
    }
}
