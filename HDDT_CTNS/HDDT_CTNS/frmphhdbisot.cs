using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace HDDT_CTNS
{
    public partial class frmphhdbisot : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        DataTable dt = new DataTable();
        public frmphhdbisot()
        {
            InitializeComponent();
            dateEdit1.EditValue = DateTime.Now;
            dateEdit2.EditValue = DateTime.Now;
            get_data();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            get_data();
        }

        void get_data()
        {
            try
            {
                dt.Clear();
                dt = GetData.Get_InvoiceListMiss(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceListMiss");
                txtbd.Text = dt.Rows[0]["stt_order"].ToString();
                txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int m_bd = Convert.ToInt16(txtbd.Text);
            int m_kt = Convert.ToInt16(txtkt.Text);
            int m_bd_indexdt = 0;
            int m_kt_indexdt = 0;
            string m_XML = "";
            string SQL;
            string reSult = "";
            // int[,] a;
            
            if (m_bd < Convert.ToInt16(dt.Rows[0]["stt_order"]) || m_kt > Convert.ToInt16(dt.Rows[dt.Rows.Count - 1]["stt_order"]))
            {
                MessageBox.Show("Số in nhỏ hơn số bắt đầu hoặc số kết thúc lớn hơn dữ liệu");
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["stt_order"]) == m_bd)
                        m_bd_indexdt = i;
                    if (Convert.ToInt32(dt.Rows[i]["stt_order"]) == m_kt)
                        m_kt_indexdt = i;
                }
                //a = PhatHanh.phan_doan(dt.Rows.Count, Connection.m_so_hoa_don);
                for (int j = m_bd_indexdt; j <= m_kt_indexdt; j++)
                {
                    // goi webservice phát hành hóa đơn
                    m_XML = PhatHanh.phat_hanh_hoa_don(dt.Rows[j]["ten_so_doc"].ToString().Trim(), dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year,"","");
                    plWebservice.PublishService pl = new plWebservice.PublishService();
                    pl.Timeout = Connection.pl_timeout;
                    reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1, 0);

                    if (reSult.Substring(0, 2) == "OK")
                    {
                        // cap nhat so lan ph tang len 1 log file vào table DaPH
                        //string ngay_ct1 = Connection.convert_date(dt.Rows[j]["ngay_ct0"].ToString().Substring(0, 10));
                        Connection.ConnectSQLSERVER();
                        SQL = "INSERT INTO DaPH(ma_kh,ngay_ct0,nam,thang,so_doc,status,m_key,m_lan)" +
                                                     "VALUES('" + dt.Rows[j]["ma_kh"].ToString().Trim() + "','" + Connection.convert_date(dt.Rows[j]["ngay_ct0"].ToString().Substring(0, 10)) + "'," + Convert.ToInt16(dt.Rows[j]["nam"]) + "," + Convert.ToInt16(dt.Rows[j]["thang"]) + ",'" + dt.Rows[j]["so_doc"].ToString().Trim() + "',1,'" + dt.Rows[j]["m_key"].ToString().Trim() + "',1)";
                        using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
                        {
                            try
                            {
                                c.ExecuteNonQuery();
                            }
                            finally
                            {
                                c.Dispose();
                            }
                        }
                        // end cap nhat so lan ph tang len 1 log file vào table DaPH

                    }
                    else
                    {
                        MessageBox.Show("Chưa phát hành hóa đơn thứ " + j.ToString());
                    }
                }

            }

        }
    }
}