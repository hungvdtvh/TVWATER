using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HDDT_CTNS
{
    public partial class XtraReportTTNS : DevExpress.XtraReports.UI.XtraReport
    {
        string m_tram, m_thang;
        public XtraReportTTNS(string thang)
        {
            InitializeComponent();
            //m_tram = lbtram;
            m_thang = thang;
            this.Lbbangchu.BeforePrint += Lbbangchu_BeforePrint;
            this.Lbbangchu1.BeforePrint += Lbbangchu1_BeforePrint;
        }

        private void Lbbangchu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            label.Text = PhatHanh.DocTienBangChu(System.Convert.ToInt32(GetCurrentColumnValue("t_tt")), " đồng");
        }

        private void Lbbangchu1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            string check = System.Convert.ToString(GetCurrentColumnValue("t_tt1"));
            if (!string.IsNullOrEmpty(check))
            {
                label.Text = PhatHanh.DocTienBangChu(System.Convert.ToInt32(GetCurrentColumnValue("t_tt1")), " đồng");
            }
            else
            {
                label.Text = null;
            }

        }
    }
}
