using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDDT_CTNS
{
    public partial class frmInLaiBN : Form
    {
        DataTable dt = new DataTable();
        Boolean ischeck = false;
        public frmInLaiBN()
        {            
            InitializeComponent();
            dateEdit2.EditValue = DateTime.Now;
            //load_dl(gridControl1, dateEdit2.DateTime.Month, dateEdit2.DateTime.Year);
            dateEdit1.EditValue = DateTime.Now;
            btnInLaiBN.Enabled = false;
            //dateEdit1.Enabled = false;
        }

        private void load_dl(GridControl gridControl1, int month, int year)
        {
            get_data();            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Browse đến file cần import
            //OpenFileDialog ofd = new OpenFileDialog();
            XtraOpenFileDialog ofd = new XtraOpenFileDialog();
            // Lấy đường dẫn file import vừa chọn
            txtFilePath.Text = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
            //Nếu file excel không tồn tại thì ngưng ct
            if (!ValidInput())
                return;

            // Đọc dữ liệu từ tập tin excel trả về DataTable
            string pro = "GetInvoiceErr";
            DataTable data = new DataTable();
            //Đọc dữ liệu từ file excel
            DataTable dt = ReadDataFromExcelFile();
            //xóa bỏ dữ liệu excel cũ
            ClearExcelOld();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tamp = dt.Rows[i]["makh"].ToString();
                //insert data trong file excel vào bảng tạm trong sql
                data = GetInvoiceErr(pro, tamp);
            }
            //gridControl1.DataSource = data;
        }

        private void ClearExcelOld()
        {
            try
            {
                Connection.ConnectSQLSERVER();
                string SQL1 = "DELETE FROM InvoiceErr";
                bool t1 = GetData.Excute_SQL_Command(SQL1);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi:" + ex.Message);
            }
            finally
            {
                Connection.DisconnectData();
            }
        }

        private bool ValidInput()
        {
            if (txtFilePath.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng chọn tập tin excel cần import");
                return false;
            }
            return true;
        }
        public DataTable ReadDataFromExcelFile()
        {
            FileInfo existingFile = new FileInfo(txtFilePath.Text.Trim());
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                var start = worksheet.Dimension.Start;
                var end = worksheet.Dimension.End;
                DataTable dttb = new DataTable();
                dttb.Columns.Add("makh");
                for (int row = 2; row <= end.Row; row++)
                {
                    object cellValue = worksheet.Cells[row, 1].Text; // This got me the actual value I needed.
                    Console.WriteLine(cellValue.ToString());
                    dttb.Rows.Add(cellValue);
                }
                return dttb;
            }
        }
        public DataTable GetInvoiceErr(string In_voice, string makh)
        {
            DataTable mc = new DataTable();
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = In_voice;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@makh", makh));
                // cmd.Parameters.Add(new SqlParameter("@sSo_doc",SqlDbType.NVarChar));
                // cmd.Parameters["@sSo_doc"].Value = sSo_doc;
                //cmd.Parameters.Add(new SqlParameter("@sSo_doc", sSo_doc));
                //cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
                //cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
                cmd.Connection = Connection.conn;
                cmd.CommandTimeout = 50000;
                // System.Threading.Thread.Sleep(500);

                using (SqlDataAdapter m = new SqlDataAdapter(cmd))
                {
                    mc.Clear();
                    m.Fill(mc);
                    m.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
            finally
            {
                Connection.DisconnectData();
            }
            return mc;
        }

        private void btnInLaiBN_Click(object sender, EventArgs e)
        {
            DataTable dtBN = new DataTable();
            //txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "so_doc").ToString().Trim();
            //txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ten_so_doc").ToString();

            ////txtbd.Text = dt.Rows[0]["stt_order"].ToString();
            ////txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
            dtBN = GetData.Get_InvoiceListBNErr(dateEdit2.DateTime.Month, dateEdit2.DateTime.Year, Convert.ToInt32(txtbd.Text), Convert.ToInt32(txtkt.Text), "InvoiceListExcelErr");

            //XtraReportBN rp = new XtraReportBN(txtTensodoc.Text.Trim(),dateEdit1.Text);
            XtraReportTTNS rp = new XtraReportTTNS( dateEdit2.Text);
            //masterReport rp = new masterReport();
            //XtraReportPT rp = new XtraReportPT();
            rp.DataSource = dtBN;
            rp.CreateDocument();
            rp.ShowPreviewDialog();
        }

        private void btnLayDuLieu_Click(object sender, EventArgs e)
        {
            get_data();

        }
        void get_data()
        {
            dt.Clear();
            //txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "so_doc").ToString().Trim();
            //txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ten_so_doc").ToString();
            //dt = GetData.Get_InvoiceList(txtSodoc.Text, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceListErrB");
            dt = GetData.Get_InvoiceListMiss(dateEdit2.DateTime.Month, dateEdit2.DateTime.Year, "InvoiceListErrC");
            txtbd.Text = dt.Rows[0]["stt_order"].ToString();
            txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
            if (ischeck)
            {
                btnInLaiBN.Enabled = true;
            }
            gridControl1.DataSource = dt;
        }

        private void btnThayThe_Click(object sender, EventArgs e)
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
                //for (int k = 0; k < gridView1.RowCount; k++)
                //{
                    //string m_ten_so_doc = gridView1.GetRowCellValue(k, ten_so_doc).ToString().Trim();
                    for (int j = m_bd_indexdt; j <= m_kt_indexdt; j++)
                    {
                    string m_ten_so_doc = gridView1.GetRowCellValue(m_bd_indexdt, ten_so_doc).ToString().Trim();
                    string m_keyR = dt.Rows[j]["m_key"].ToString().Trim();
                        m_XML = PhatHanh.phat_hanh_hoa_don_thay_the(m_ten_so_doc, dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dateEdit1.Text);
                        // goi webservice phát hành hóa đơn
                        //string m_keyR = dt.Rows[j]["m_key"].ToString();

                        //string m_keyR = "1804660104$1";
                        bsWebservice.BusinessService pl = new bsWebservice.BusinessService();
                        pl.Timeout = Connection.pl_timeout;
                        reSult = pl.ReplaceInvoie(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, m_keyR, "", 0);
                        if (reSult.Substring(0, 2) == "OK")
                        {
                            // cap nhat so lan ph tang len 1 log file vào table DaPH
                            Connection.ConnectSQLSERVER();
                            //,m_key = left(m_key, 10) + '$' + rtrim(convert(nchar, m_lan))
                            SQL = "UPDATE DaPH SET m_lan=m_lan+1,m_key='" + dt.Rows[j]["m_key"].ToString().Trim() + "' WHERE ma_kh='" + dt.Rows[j]["ma_kh"].ToString().Trim() + "' AND thang=" + Convert.ToInt16(dt.Rows[j]["thang"]) + " AND nam=" + Convert.ToInt16(dt.Rows[j]["nam"]);
                            using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
                            {
                                try
                                {
                                    c.ExecuteNonQuery();
                                }
                                finally
                                {
                                    c.Dispose();
                                    Connection.DisconnectData();
                                }
                            }
                            // end cap nhat so lan ph tang len 1 log file vào table DaPH

                        }
                        else
                        {
                            MessageBox.Show("Chưa phát hành hóa đơn thứ " + j.ToString());
                        }
                    //}
                    }
                ischeck = true;
                get_data();               
            }
        }
    }
}
