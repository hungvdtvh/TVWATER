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
using HDDT_CTNS.bsWebservice;
using HDDT_CTNS.plWebservice;
using HDDT_CTNS.portalWebsevice;

namespace HDDT_CTNS
{
    public partial class frmphhdbiloi : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        DataTable dt = new DataTable();
        public frmphhdbiloi()
        {
            InitializeComponent();
            gridView2.ShowFindPanel();
            dateEdit1.EditValue = DateTime.Now.AddMonths(-1);
            dateEdit2.EditValue = DateTime.Now;
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            gridView1.RowClick += GridView1_RowClick;
            gridView1.KeyUp += GridView1_KeyUp;
            
            //.KeyPress += GridView1_KeyPress;
        }

        private void GridView1_KeyUp(object sender, KeyEventArgs e)
        {
            get_data();
        }

        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            get_data();
        }
           

        public static void load_dl(GridControl gridControl1,int thang, int nam)
        {
            string SQL = "select b.so_doc, b.ten_so_doc from in_sodoc a, dmsodoc b where  a.so_doc=b.so_doc and a.thang=" + thang + " and a.nam=" + nam + " order by so_doc";
            GetData.dien_dl_vao_grid(SQL, gridControl1);
        }

        private void cmdview_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
        }

        void get_data()
        {
            dt.Clear();
            txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"so_doc").ToString().Trim();
            txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ten_so_doc").ToString();
            dt = GetData.Get_InvoiceList(txtSodoc.Text, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceListErr");
            txtbd.Text = dt.Rows[0]["stt_order"].ToString();
            txtkt.Text = dt.Rows[dt.Rows.Count-1]["stt_order"].ToString();
            gridControl2.DataSource = dt;
        }

        private void cmdPhathanh_Click(object sender, EventArgs e)
        {
            int m_bd = Convert.ToInt16(txtbd.Text);
            int m_kt = Convert.ToInt16(txtkt.Text);
            int m_bd_indexdt=0;
            int m_kt_indexdt=0;
            string m_XML = "";
            string SQL;
            string reSult = "";
            // int[,] a;
            bsWebservice.BusinessService pl = new bsWebservice.BusinessService();
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
                    m_XML = PhatHanh.phat_hanh_hoa_don_thay_the(txtSodoc.Text.ToString().Trim(), dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
                    // goi webservice phát hành hóa đơn
                    reSult = pl.ReplaceInvoiceAction(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, dt.Rows[j]["m_key"].ToString().Trim(),"",0, Connection.patternHsm, Connection.serial1);
                    if (reSult.Substring(0, 2) == "OK")
                    {
                        // cap nhat so lan ph tang len 1 log file vào table DaPH
                        Connection.ConnectSQLSERVER();
                        SQL = "UPDATE DaPH SET m_lan=m_lan+1 WHERE ma_kh='" + dt.Rows[j]["ma_kh"].ToString().Trim() + "' AND thang=" + Convert.ToInt16(dt.Rows[j]["thang"]) + " AND nam=" + Convert.ToInt16(dt.Rows[j]["nam"]);
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
                }
             
            }
           
        }
    }
}