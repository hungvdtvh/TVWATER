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
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using HDDT_CTNS.bsWebservice;
using HDDT_CTNS.plWebservice;
using HDDT_CTNS.portalWebsevice;

namespace HDDT_CTNS
{
    public partial class frminthongbao : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        DataTable dt = new DataTable();
        int so_lan_in=0;
        public frminthongbao()
        {
            InitializeComponent();
            gridView2.ShowFindPanel();
            dateEdit1.EditValue = DateTime.Now;
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
            // String SQL = "select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from (select rtrim(a.ten_so_doc) as ten_so_doc, b.* from dmsodoc a inner join (select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien from phW where thang=" + thang + " and nam=" + nam + " group by so_doc) b on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c where c.so_doc not in (select so_doc from in_sodoc d where d.thang=" + thang + " and d.nam=" + nam + " ) order by so_doc";
            String SQL = "select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from (select rtrim(a.ten_so_doc) as ten_so_doc, b.* from dmsodoc a inner join (select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien from phW where thang=" + thang + " and nam=" + nam + " group by so_doc) b on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c  order by so_doc";
            GetData.dien_dl_vao_grid(SQL, gridControl1);
        }

        private void cmdview_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            //update du lieu gan da in lan dau = 0
            //try
            //{
            //    Connection.ConnectSQLSERVER();
            //    string SQL = "UPDATE phW SET m_lan_in=0";
            //    using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
            //    {
            //        c.CommandTimeout = 50000;
            //        try
            //        {
            //            c.ExecuteNonQuery();
            //        }
            //        finally
            //        {
            //            c.Dispose();
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //finally
            //{
            //    Connection.DisconnectData();
            //}
        }

        void get_data()
        {
            dt.Clear();
            txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"so_doc").ToString().Trim();
            txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ten_so_doc").ToString();
            dt = GetData.Get_InvoiceList(txtSodoc.Text, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceListErrB");
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
            plWebservice.PublishService pl = new plWebservice.PublishService();
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
                    m_XML = PhatHanh.phat_hanh_hoa_don(txtTensodoc.Text.Trim(), dt, j,j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year,"","");
                    reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1, 0);
                    System.Threading.Thread.Sleep(2000);
                    if (reSult.Substring(0, 2) == "OK")
                    {
                        //string ngay_ct4 = Connection.convert_date(dt.Rows[j]["ngay_ct0"].ToString().Substring(0, 10));
                        // cap nhat so lan ph tang len 1 log file vào table DaPH
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
                // kiem tra xem nếu tất cả các row trong so doc co key thi inser vao table in_sodoc de danh dau so doc nay da in het roi
                if (!isCheckNULL(dt))
                {
                    Connection.ConnectSQLSERVER();
                    SQL = "INSERT INTO in_sodoc(nam,thang,so_doc,status)" +
                                         "VALUES(" + Convert.ToInt16(dt.Rows[m_kt_indexdt]["nam"]) + "," + Convert.ToInt16(dt.Rows[m_kt_indexdt]["thang"]) + ",'" + dt.Rows[m_kt_indexdt]["so_doc"].ToString().Trim() + "',1)";

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
                    Connection.DisconnectData();
                }

            }
           
        }

        private void cmdXem_Click(object sender, EventArgs e)
        {
            XtraReport1 rp = new XtraReport1(txtTensodoc.Text.Trim());            
            rp.DataSource = dt;
            rp.CreateDocument();
            rp.ShowPreviewDialog();        
        }

        public bool isCheckNULL(DataTable dt)
        {
            bool m_return = false;
            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i]["m_key"] == DBNull.Value)
                {
                    m_return = true;
                    break;
                }
            return m_return;
        }

        private void cmdInBN_Click(object sender, EventArgs e)
        {            
            DataTable dtBN = new DataTable();
            DataTable dtCheckPrint = new DataTable();
            txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "so_doc").ToString().Trim();
            txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ten_so_doc").ToString();
            ////txtbd.Text = dt.Rows[0]["stt_order"].ToString();
            ////txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
            //so_lan_in = GetData.Get_CheckPrint(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, txtSodoc.Text);
            dtBN = GetData.Get_InvoiceListBN(txtSodoc.Text, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, Convert.ToInt32(txtbd.Text), Convert.ToInt32(txtkt.Text), "InvoiceListBN");
            so_lan_in = GetData.Get_CheckPrint(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, txtSodoc.Text);
            if (so_lan_in>1)
            {
                DialogResult dialogResult= MessageBox.Show(String.Format("Sổ đọc {0} - {1} đã được in lần thứ {2}, bạn có muốn tiếp tục in?", txtSodoc.Text, txtTensodoc.Text, so_lan_in), 
                    "Cảnh báo",
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);
                if (dialogResult== DialogResult.No)
                {
                    return;
                }

            }
            //XtraReportBN rp = new XtraReportBN(txtTensodoc.Text.Trim(),dateEdit1.Text);
            //XtraReportTTNS rp = new XtraReportTTNS(txtTensodoc.Text.Trim(), dateEdit1.Text);
            XtraReportTTNS rp = new XtraReportTTNS( dateEdit1.Text);
            //masterReport rp = new masterReport();
            //XtraReportPT rp = new XtraReportPT();
            rp.DataSource = dtBN;
            rp.CreateDocument();
            rp.ShowPreviewDialog();

        }
    }
}