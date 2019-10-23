using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HDDT_CTNS
{
    public partial class XtraReportBNN : DevExpress.XtraReports.UI.XtraReport
    {
        string m_tram,m_thang;
        public XtraReportBNN(string lbtram, string thang)
        {
            InitializeComponent();
            m_tram = lbtram;
            m_thang = thang;
            this.Lbbangchu.BeforePrint += Lbbangchu_BeforePrint;
            this.Lbtram.BeforePrint += Lbtram_BeforePrint;
           // this.Lbthang.BeforePrint += Lbthang_BeforePrint;
        }

        private void Lbthang_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            label.Text = m_thang;
        }

        private void Lbtram_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            label.Text = m_tram;
        }

        private void Lbbangchu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            label.Text = PhatHanh.DocTienBangChu(System.Convert.ToInt32(GetCurrentColumnValue("t_tt")), " đồng");
        }


    }
}
