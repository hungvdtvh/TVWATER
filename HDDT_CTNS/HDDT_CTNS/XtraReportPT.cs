using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HDDT_CTNS
{
    public partial class XtraReportPT : DevExpress.XtraReports.UI.XtraReport
    {
        string m_tram, m_thang;       
        public XtraReportPT(string lbtram, string thang)
        {
            InitializeComponent();
            m_tram = lbtram;            
            m_thang = thang;
            this.Lbbangchu.BeforePrint += Lbbangchu_BeforePrint;
            this.Lbbangchu1.BeforePrint += Lbbangchu1_BeforePrint;
            //this.Lbtram.BeforePrint += Lbtram_BeforePrint;
            //this.Lbtram1.BeforePrint += Lbtram1_BeforePrint;
            // this.Lbthang.BeforePrint += Lbthang_BeforePrint;
        }
        private void Lbtram_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            label.Text = m_tram;
        }

        private void Lbtram1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = (XRLabel)sender;
            if (m_tram!= null)
            {
                label.Text = m_tram;
            }
            else
            {
                label.Text = null;
            }
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
