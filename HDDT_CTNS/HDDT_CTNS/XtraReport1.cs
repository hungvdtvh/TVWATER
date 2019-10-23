using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HDDT_CTNS
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        string m_tram;
        public XtraReport1(string lbtram)
        {
            InitializeComponent();
            m_tram = lbtram;
            this.Lbbangchu.BeforePrint += Lbbangchu_BeforePrint;
            this.Lbtram.BeforePrint += Lbtram_BeforePrint;


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
