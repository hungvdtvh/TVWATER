using System;
using System.Xml;
using System.IO;
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
using System.IO.Compression;
using System.Globalization;

namespace HDDT_CTNS
{
    public partial class frmzipfile : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        public frmzipfile()
        {
            InitializeComponent();
            dateEdit1.EditValue = DateTime.Now;
            dngayPH.EditValue = DateTime.Now;          
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            //Connection.EnableControl(this, false);
            //EnableControl(false);
            gridView1.ShowFindPanel();
            //dngayPH.Enabled = false;
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
            label3.Text = "Đang zip file";           
            cmdPhathanh.Enabled = false;
            //if (xtraSaveFileDialog1.ShowDialog()==DialogResult.OK)
            //{
            //    txtLuuFile.Text = xtraSaveFileDialog1.FileName;
            //}
            //System.Threading.Thread.Sleep(5000);
            //gridView1.DeleteRow(1);
            // DataTable dt=  GetData.Get_InvoiceList("045", dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            
            string m_sodoc = "";
            Boolean check = false;
            string m_XML = "";
            string SQL;
            string reSult = "";
            DataTable dt = new DataTable();
            int[,] a;
            string tenFile = "";
            
            try
            {
                xtraSaveFileDialog1.Filter = "(*.zip)|*.zip";
                xtraSaveFileDialog1.FilterIndex = 2;
                //xtraSaveFileDialog1.RestoreDirectory = true;
                Boolean ktra = false;
                int m_cancle = 0;
                while (!ktra)
                {
                    if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        txtLuuFile.Text = xtraSaveFileDialog1.FileName;
                        ktra = true;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn nơi lưu file");
                        //nếu không chọn lưu file 5 lần thoát khỏi vòng lập
                        m_cancle++;
                        if (m_cancle==3)
                        {
                            return;
                        }
                        //xtraSaveFileDialog1.ShowDialog();
                        //txtLuuFile.Text = xtraSaveFileDialog1.FileName;
                    }

                }
                //if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    txtLuuFile.Text = xtraSaveFileDialog1.FileName;                  
                //}
                //else
                //{
                //    MessageBox.Show("Vui lòng chọn nơi lưu file");
                //    xtraSaveFileDialog1.ShowDialog();
                //    txtLuuFile.Text = xtraSaveFileDialog1.FileName;
                //}
                if (isCheck())
                {
                    //Bat đau phat hanh hoa don
                    for (int k = 0; k < gridView1.RowCount; k++)
                    {
                      //  MessageBox.Show(k.ToString());
                        if (Convert.ToBoolean(gridView1.GetRowCellValue(k, status)))                       
                            m_sodoc +=  gridView1.GetRowCellValue(k, so_doc).ToString().Trim()+",";

                        
                    }
                    m_sodoc = "'"+ m_sodoc.Substring(0, m_sodoc.Length - 1)+"'";                   
                    dt = GetData.Get_InvoiceList(m_sodoc, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceList_SD");
                    if (dt.Rows.Count>0)
                    {
                        //string m_file = "PH_" + "_"+ dateEdit1.DateTime.Year.ToString()+ (dateEdit1.DateTime.Month<10? '0'+dateEdit1.DateTime.Month.ToString():dateEdit1.DateTime.Month.ToString())+"01";
                        m_XML = PhatHanh.phat_hanh_hoa_don_zip("", dt, 0, dt.Rows.Count-1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dngayPH.Text);
                        if (m_XML != null && PhatHanh.checkNull == false)
                        {
                            //string line = m_XML;
                            //string strPath = Environment.GetFolderPath(
                            //         System.Environment.SpecialFolder.DesktopDirectory);
                            string strStartupPath = Application.StartupPath;
                            string strXmlDataFilePath = strStartupPath + "\\" + "einv" + ".xml";
                            System.IO.StreamWriter file = new System.IO.StreamWriter(strXmlDataFilePath);
                            file.WriteLine(m_XML);
                            file.Close();

                            DataTable dt_zip = new DataTable();

                            //if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\InvZipFiles"))
                            //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\InvZipFiles");

                            string fileName = "";

                            if (Directory.Exists(txtLuuFile.Text))
                            {
                                fileName = txtLuuFile.Text;
                            }
                            else
                            {
                                //string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                                fileName =  txtLuuFile.Text;
                            }
                            // string fileName2 = Directory.GetCurrentDirectory() + @"\InvZipFiles\" + "Cuoc_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "222" + ".zip";
                            Stream CompStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                            using (Stream RawStream2 = new FileStream(strXmlDataFilePath.Trim(), FileMode.Open, FileAccess.ReadWrite))
                            {
                                Compress(RawStream2, CompStream);
                            }

                            CompStream.Close();
                            label3.Text = "Đã tạo file hóa đơn xong";
                            MessageBox.Show("Đã tạo file hóa đơn xong !");
                            check = true;
                            Connection.ConnectSQLSERVER();
                            for (int bien = 0; bien < dt.Rows.Count; bien++)
                            {
                                //string ngay_ct = dt.Rows[bien]["ngay_ct0"].ToString().Substring(0, 10);
                                //DateTime ngay = DateTime.ParseExact(ngay_ct, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                //string s1 = ngay_ct.Substring(0, 2);
                                //string s2 = ngay_ct.Substring(3, 2);
                                //string s3 = ngay_ct.Substring(6, 4);
                                //ngay_ct = s2 +"/"+ s1 +"/" +s3;
                                //string ngay_ct = Connection.convert_date(dt.Rows[bien]["ngay_ct0"].ToString().Substring(0, 10));
                                SQL = "INSERT INTO DaPH(ma_kh,ngay_ct0,nam,thang,so_doc,status,m_key,m_lan)" +
                                              "VALUES('" + dt.Rows[bien]["ma_kh"].ToString().Trim() + "','" + Connection.convert_date(dt.Rows[bien]["ngay_ct0"].ToString().Substring(0, 10)) + "'," + Convert.ToInt16(dt.Rows[bien]["nam"]) + "," + Convert.ToInt16(dt.Rows[bien]["thang"]) + ",'" + dt.Rows[bien]["so_doc"].ToString().Trim() + "',1,'" + dt.Rows[bien]["m_key"].ToString().Trim() + "',1)";

                                using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
                                {
                                    c.CommandTimeout = 50000;
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
                        }
                        else
                        {
                            MessageBox.Show("Tồn tại khách hàng chưa có tên","Lổi",MessageBoxButtons.OK,MessageBoxIcon.Error);                            
                            return;
                        }
                        if (check)
                        {                            
                            //System.Threading.Thread.Sleep(2000);
                            for (int m = 0; m < gridView1.RowCount; m++)
                            {
                                if (Convert.ToBoolean(gridView1.GetRowCellValue(m, status)))
                                {
                                    //Connection.ConnectSQLSERVER();
                                    SQL = "INSERT INTO in_sodoc(nam,thang,so_doc,status)" +
                                                             "VALUES(" + dateEdit1.DateTime.Year + "," + dateEdit1.DateTime.Month + ",'" + gridView1.GetRowCellValue(m, "so_doc") + "',1)";

                                    using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
                                    {
                                        c.CommandTimeout = 50000;
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
                            }
                            //XmlDocument xdoc = new XmlDocument();
                            //xdoc.LoadXml(m_XML);
                            //xdoc.Save(Connection.p_path+"myfilename.xml"); 
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
        private void Compress(Stream data, Stream outData)
        {
            string str = "";
            try
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
                {
                    zipStream.SetLevel(3);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry("data.xml");
                    newEntry.DateTime = DateTime.UtcNow;
                    //newEntry.Size = data.Length;
                    zipStream.PutNextEntry(newEntry);

                    //data.CopyTo(zipStream);
                    //CopyStream(data, zipStream);
                    //zipStream.Write(data, 0, data.Length);
                    // zipStream.Finish();
                    //zipStream.Close();//?

                    //byte[] buffer = new byte[32768];
                    //int read;
                    //while ((read = data.Read(buffer, 0, buffer.Length)) > 0)
                    //{
                    //    zipStream.Write(buffer, 0, read);
                    //}

                    data.Position = 0;
                    int size = (data.CanSeek) ? Math.Min((int)(data.Length - data.Position), 0x2000) : 0x2000;
                    byte[] buffer = new byte[size];
                    int n;
                    do
                    {
                        n = data.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, n);
                    } while (n != 0);
                    zipStream.CloseEntry();
                    zipStream.Flush();
                    zipStream.Close();
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

        }        
    }
}



//////dt.Clear();
//////                            m_sodoc = gridView1.GetRowCellValue(k, so_doc).ToString().Trim();
//////string m_ten_so_doc = gridView1.GetRowCellValue(k, ten_so_doc).ToString().Trim();
//////dt = GetData.Get_InvoiceList(m_sodoc, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceList_SD");

//////                            //string m_sodoc= gridView1.GetRowCellValue(i, status)
//////                            //DataTable dt =  gridView1.GetRowCellValue(i, status)
//////                            Connection.ConnectSQLSERVER();

//////                            a = PhatHanh.phan_doan(dt.Rows.Count, Connection.m_so_hoa_don);
//////                            int t = a.GetLength(1);
//////                            for (int i = 0; i<a.GetLength(1); i++)
//////                            {
//////                                m_XML = PhatHanh.phat_hanh_hoa_don(m_ten_so_doc, dt, a[0, i], a[1, i], dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
//////                                plWebservice.PublishService pl = new plWebservice.PublishService();
//////pl.Timeout = Connection.pl_timeout;
//////                                reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1, 0);
//////                                if (reSult.Substring(0, 2) == "OK")
//////                                {
//////                                    // ghi log file vào table DaPH
//////                                   // Connection.ConnectSQLSERVER();
//////                                    for (int j = a[0, i]; j <= a[1, i]; j++)
//////                                    {
//////                                        SQL = "INSERT INTO DaPH(ma_kh,ngay_ct0,nam,thang,so_doc,status,m_key,m_lan)" +
//////                                                      "VALUES('" + dt.Rows[j]["ma_kh"].ToString().Trim() + "','" + dt.Rows[j]["ngay_ct0"].ToString().Substring(0,10) + "'," + Convert.ToInt16(dt.Rows[j]["nam"]) + "," + Convert.ToInt16(dt.Rows[j]["thang"]) + ",'" + dt.Rows[j]["so_doc"].ToString().Trim() + "',1,'" + dt.Rows[j]["m_key"].ToString().Trim() + "',1)";

//////                                        using (SqlCommand c = new SqlCommand(SQL, Connection.conn))
//////                                        {
//////                                            try
//////                                            {
//////                                                c.ExecuteNonQuery();
//////                                            }
//////                                            finally
//////                                            {
//////                                                c.Dispose();
//////                                            }
//////                                        }
//////                                    }