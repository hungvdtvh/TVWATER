using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using HDDT_CTNS.bsWebservice;
using HDDT_CTNS.plWebservice;
using HDDT_CTNS.portalWebsevice;


namespace HDDT_CTNS
{
    public partial class frmintheosodoc : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        public frmintheosodoc()
        {
            InitializeComponent();
            dateEdit1.EditValue = DateTime.Now;
            dngayPH.EditValue = DateTime.Now;            
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            //Connection.EnableControl(this, false);
            //EnableControl(false);
            gridView1.ShowFindPanel();
        }

        public void load_dl(GridControl gridControl1, int thang, int nam)
        {
            String SQL = "select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from (select rtrim(a.ten_so_doc) as ten_so_doc, b.* from dmsodoc a inner join (select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien from phW where thang=" + thang+" and nam="+nam+ " group by so_doc) b on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c where c.so_doc not in (select so_doc from in_sodoc d where d.thang=" + thang + " and d.nam=" + nam +" ) order by so_doc";
            GetData.dien_dl_vao_grid(SQL, gridControl1);
            load_dl_grid2(thang,nam);

        }
        public void load_dl_grid2(int thang, int nam)
        {
           string SQL = "select b.so_doc, b.ten_so_doc from in_sodoc a, dmsodoc b where  a.so_doc=b.so_doc and a.thang=" + thang + " and a.nam=" + nam + " order by so_doc";
           GetData.dien_dl_vao_grid(SQL, gridControl2);
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
        }

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, status, 1);
        }

        private void cmdPhathanh_Click(object sender, EventArgs e)
        {
            label3.Text = "Đang phát hành hóa đơn";           
            cmdPhathanh.Enabled = false;
            //System.Threading.Thread.Sleep(5000);
            //gridView1.DeleteRow(1);
            // DataTable dt=  GetData.Get_InvoiceList("045", dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            
            string m_sodoc = "";
            string m_XML = "";
            string SQL;
            string reSult = "";
            DataTable dt = new DataTable();
            int[,] a;
            
            try
            {
               
                if (isCheck())
                {
                    //Bat đau phat hanh hoa don
                    for (int k = 0; k < gridView1.RowCount; k++)
                    {
                        if (Convert.ToBoolean(gridView1.GetRowCellValue(k, status)))
                        {
                            dt.Clear();
                            m_sodoc = gridView1.GetRowCellValue(k, so_doc).ToString().Trim();
                            string m_ten_so_doc= gridView1.GetRowCellValue(k, ten_so_doc).ToString().Trim();
                            dt = GetData.Get_InvoiceList(m_sodoc, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceList");

                            //string m_sodoc= gridView1.GetRowCellValue(i, status)
                            //DataTable dt =  gridView1.GetRowCellValue(i, status)
                            Connection.ConnectSQLSERVER();

                            a = PhatHanh.phan_doan(dt.Rows.Count, Connection.m_so_hoa_don);
                            int t = a.GetLength(1);
                            for (int i = 0; i < a.GetLength(1); i++)
                            {
                                m_XML = PhatHanh.phat_hanh_hoa_don(m_ten_so_doc, dt, a[0, i], a[1, i], dateEdit1.DateTime.Month, dateEdit1.DateTime.Year,"", dngayPH.Text);
                                plWebservice.PublishService pl = new plWebservice.PublishService();
                                pl.Timeout = Connection.pl_timeout;
                                reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1, 0);
                                if (reSult.Substring(0, 2) == "OK")
                                {
                                    // ghi log file vào table DaPH
                                   // Connection.ConnectSQLSERVER();
                                    for (int j = a[0, i]; j <= a[1, i]; j++)
                                    {
                                        //string ngay_ct2 = Connection.convert_date(dt.Rows[j]["ngay_ct0"].ToString().Substring(0, 10));
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
                                    }
                                   
                                    // end ghi log file vào table DaPH
                                    // MessageBox.Show(a[0,i].ToString());
                                    //MessageBox.Show(a[1, i].ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Chưa phát hành hóa đơn đoạn "+ dt.Rows[a[0, i]]["stt_order"].ToString() + " đến " + dt.Rows[a[1, i]]["stt_order"].ToString()+" của sổ đọc "+ m_sodoc);
                                    return;
                                }
                            }
                            label3.Text = "Đã phát hành hóa đơn xong";
                            MessageBox.Show("Đã phát hành hóa đơn xong !");
                            if (reSult.Substring(0, 2) == "OK")
                            {
                                SQL = "INSERT INTO in_sodoc(nam,thang,so_doc,status)" +
                                                 "VALUES(" + dateEdit1.DateTime.Year + "," + dateEdit1.DateTime.Month + ",'" + gridView1.GetRowCellValue(k, "so_doc") + "',1)";

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
                                //gridView1.DeleteRow(k);
                                //dt.Rows[k].Delete();
                                //load_dl_grid2(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
                            }
                        }
                        
                    }
                        
                }
                else
                    MessageBox.Show("Chưa chọn sổ đọc nào");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                
                load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
                Connection.DisconnectData();
                cmdPhathanh.Enabled = true;
                
            }
        }
            
       
        private void cmdUncheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, status, 0);
        }

        public bool isCheck()
        {
           bool m_return = false;
            for (int i = 0; i < gridView1.RowCount; i++)
                if (Convert.ToBoolean(gridView1.GetRowCellValue(i, status)))
                {
                    m_return = true;
                    break;
                }
            return m_return;
        }

        

    }
}