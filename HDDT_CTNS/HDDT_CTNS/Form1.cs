using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraBars.Helpers;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using HDDT_CTNS.plWebservice;
using HDDT_CTNS.portalWebsevice;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;

namespace HDDT_CTNS
{
    public partial class Form1 : RibbonForm
    {
       // OleDbDataAdapter a;
        Form form = null;
        Form form1 = null;
       
        private static AutoResetEvent resetEvent = new AutoResetEvent(false);      
        string NotExitDir = "";
        string mess = "";
        string reSult = "";
        [DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        public Form1()
        {
            InitializeComponent();
            InitSkinGallery();            
            this.Text = "VNPT Envoice    - start at : " + DateTime.Now.ToString();
            //if (Connection.m_lan == 1)
            //{
            //    this.WindowState = FormWindowState.Minimized;
            //    this.SetVisibleCore(false);
            //}
            // get_data();
            Enable_btn();
            this.ResizeEnd +=new EventHandler(Form1_ResizeEnd);           
          
            //  backgroundWorker4.RunWorkerAsync();   
            

        }
        void InitSkinGallery()
        {
           // SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        void Enable_btn()
        {
            if (!Connection.m_admin)
            {
                brIntheosodoc.Enabled = false;
                brPHtheoso.Enabled = false;
                brBPhlai.Enabled = false;
                brPhsot.Enabled = false;
                barButtonItem7.Enabled = false;
                barButtonItem8.Enabled = false;
            }
        }


        //protected override void OnLoad(EventArgs args)
        //{
        //    base.OnLoad(args);
        //    Application.Idle += new EventHandler(OnLoaded);
        //}

        //private void OnLoaded(object sender,EventArgs args)
        //{
        //    Application.Idle -= new EventHandler(OnLoaded);
        //    backgroundWorker4.RunWorkerAsync();
        //}
 
        
    
        //private void timer1_Tick(object sender, EventArgs e) 
        //{
        //    try
        //    {
        //        timer1.Enabled = false;
        //        if (Directory.Exists(@Connection.path_excel))
        //        {
        //            string[] filePaths = Directory.GetFiles(@Connection.path_excel);
        //            plWebService.PublishService pl = new plWebService.PublishService();
        //            //plWebServiceCT406.PublishService pl = new plWebServiceCT406.PublishService();
        //            foreach (string m_file in filePaths)
        //            {
        //                // DataTable dtb = new DataTable();
        //               // string XMLStr = ExceltoDataTable.ConvertExcelToDatatable(m_file);
        //                string XMLStr = "<Invoices><Inv><key>20180102105043464A1</key><Invoice><CusCode>2018011</CusCode><CusName/><Buyer>MÃ THỊ HỒNG ĐÀO  - GD484842110642784001   66</Buyer><CusAddress>Phường 2, Thành Phố Trà Vinh, Trà Vinh</CusAddress><CusPhone/><CusTaxCode/><PaymentMethod/><KindOfService/><Products><Product><ProdName>Ngày giường chuyên khoa</ProdName><ProdUnit/><Amount>224260</Amount></Product><Product><ProdName>Xét nghiệm</ProdName><ProdUnit/><Amount>80380</Amount></Product><Product><ProdName>Thăm dò chức năng</ProdName><ProdUnit/><Amount>9180</Amount></Product><Product><ProdName>Thuốc,VTYT</ProdName><ProdUnit/><Amount>75029</Amount></Product></Products><Total>388849</Total><Amount>388849</Amount><AmountInWords>Ba trăm tám mươi tám nghìn tám trăm bốn mươi chín đồng</AmountInWords><ArisingDate>02/01/2018</ArisingDate><PaymentStatus>1</PaymentStatus></Invoice></Inv><Inv><key>20180102105043464B1</key><Invoice><CusCode>2018022</CusCode><CusName/><Buyer>MÃ THỊ HỒNG ĐÀO  - GD484842110642784001   66</Buyer><CusAddress>Phường 2, Thành Phố Trà Vinh, Trà Vinh</CusAddress><CusPhone/><CusTaxCode/><PaymentMethod/><KindOfService/><Products><Product><ProdName>Ngày giường chuyên khoa</ProdName><ProdUnit/><Amount>224260</Amount></Product><Product><ProdName>Xét nghiệm</ProdName><ProdUnit/><Amount>80380</Amount></Product><Product><ProdName>Thăm dò chức năng</ProdName><ProdUnit/><Amount>9180</Amount></Product><Product><ProdName>Thuốc,VTYT</ProdName><ProdUnit/><Amount>75029</Amount></Product></Products><Total>388849</Total><Amount>388849</Amount><AmountInWords>Ba trăm tám mươi tám nghìn tám trăm bốn mươi chín đồng</AmountInWords><ArisingDate>02/01/2018</ArisingDate><PaymentStatus>1</PaymentStatus></Invoice></Inv></Invoices>";
        //                if (XMLStr != "")
        //                {
        //                    reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, XMLStr, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1, 0);


        //                    if (reSult.Substring(0, 2) == "OK")
        //                    {
        //                        alertControl1.Show(this, "Thông báo", "Phát hành hóa đơn: " + ExceltoDataTable.ten_kh + " thành công");
        //                        //MessageBox.Show("Phát hành hóa đơn thành công");
        //                        string SQL = "INSERT INTO hoa_don(m_key,ten_kh,ten_ct,dia_chi,ngay,so_tien) VALUES('" + ExceltoDataTable.key + "','" + ExceltoDataTable.ten_kh + "','" + ExceltoDataTable.ten_ct + "','" + ExceltoDataTable.dia_chi + "','" + ExceltoDataTable.ngay_ph_nguoc + "'," + ExceltoDataTable.tong_tien + ")";

        //                        Connection.ConnectToData();
        //                        using (OleDbCommand a = new OleDbCommand(SQL, Connection.conn))
        //                        {
        //                            try
        //                            {
        //                                a.ExecuteNonQuery();
        //                            }
        //                            catch { }// ( Exception ex) { MessageBox.Show(ex.Message); }
        //                            finally
        //                            {
        //                                a.Dispose();
        //                                //MessageBox.Show("Đăng ký thành công !");
        //                                Connection.conn.Close();
        //                            }
        //                        }
        //                        // File.Delete(m_file);
        //                        if (!Directory.Exists("SUCCESS"))
        //                        {
        //                            Directory.CreateDirectory(@Connection.path_excel + "SUCCESS");
        //                        }
        //                        File.Move(m_file, @Connection.path_excel + "SUCCESS\\" + ExceltoDataTable.key + ".xls");
        //                    }
        //                    else
        //                        alertControl1.Show(this, "Thông báo", "Lỗi phát hành hóa đơn: " + reSult + "   :" + ExceltoDataTable.ten_kh);
        //                }
        //                ////else
        //                ////    MessageBox.Show("Phát hành hóa đơn không thành công " + reSult);
        //            }                   
                    
        //        }
        //        else
        //        {
        //            mess = "Thư mục: " + Connection.path_excel + " không tồn tại không thể phát hành hóa đơn điện tử được !";
        //            alertControl1.Show(this, "Thông báo", mess);
        //        } 
        //    }
        //    //catch (Exception)
        //    //{

        //    //    throw;
        //    //}
        //    finally
        //    {
               
        //        timer1.Enabled = true;
        //    }
        //}



        //void get_data()
        //{
        //    gridControl1.DataSource = null;
        //    //show_marqueeProgressBar();
        //    string SQL = "SELECT * FROM hoa_don ORDER BY ngay";

        //    Connection.ConnectToData();
        //    using (OleDbDataAdapter ar = new OleDbDataAdapter(SQL, Connection.conn))
        //    {                
        //        ar.SelectCommand.CommandTimeout = 600;
        //        DataTable t = new DataTable();
        //        try
        //        {
        //            t.Clear();
        //            ar.Fill(t);
        //        }
        //        finally
        //        {
        //            ar.Dispose();
        //            gridControl1.DataSource = t;
        //            Connection.conn.Close();
        //        }
        //    }
        //}


     

    

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;            
            foreach (Form frm in fc)
            {
                if (frm.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void CloseAllFormOpened()
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name != "Form1" && frm.Name != "")
                {
                    frm.Hide();
                }
            }         
        }

        void Form1_ResizeEnd(Object sender, System.EventArgs e)
        { 
            FormCollection fc = Application.OpenForms;
            //foreach (Form frm in fc)
            //{
            //    if (frm.Name != "Form1")
            //    {
            //        frm.Height = splitContainerControl.Panel2.Height;
            //        frm.Width = splitContainerControl.Panel2.Width;
            //        frm.Refresh();
            //    }
            //}
        }

      

        private void bBMin_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            this.SetVisibleCore(false);
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
           // System.Diagnostics.Process.Start(@"PATH\NAME.EXE");
           DialogResult Q = MessageBox.Show("Muốn thoát chương trình ? nếu thoát chương trình sẽ không thể backup được !", "Xác nhận", MessageBoxButtons.YesNo);
            if (Q == DialogResult.Yes)
            {
                Application.ExitThread();
                Application.Exit();
            }


            //MessageBox.Show(Connection.DecrytedString("MQAwAC4AMAAuADAALgAzAA=="));
        }

       
     
        private void brBLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
           // Connection.m_admin = false;
           // Connection.createkey = false;
           // enableButtonS(false);
           //// brReport.Enabled = false;
        }

      

       

        private void alertControl1_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            form = e.AlertForm;
        }

        private void alertControl2_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            form1 = e.AlertForm;
        }

     
       
      
        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FrmAbout frm = new FrmAbout(false);
            //frm.ShowDialog();
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
        }

       

             
        private void brFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            //CloseAllFormOpened();
            //gridControl1.Visible = false;
            //frmexFile frm = new frmexFile();
            //frm.TopLevel = false;
            //frm.Parent = splitContainerControl.Panel2;
            //frm.Show();
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
        }

        private void brPhuchoi_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void btnIn_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        //userNameHsm = "ct406service";//txtusernamePH.Text.Trim();
        //        //userPassHsm = "123456aA@";//txtuserpassPH.Text.Trim();
        //        //string InvoiceToken = txtInvoiceToken.Text.Trim();
        //       string fkeyHsm = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, m_key).ToString();
        //        portalService.PortalService pl = new portalService.PortalService();
               
        //        //reSult = pl.downloadInvPDF(InvoiceToken, userNameHsm, userPassHsm);
        //        string reSult = pl.downloadInvPDFFkey(fkeyHsm, Connection.userNameHsm, Connection.userPassHsm);
        //        //string txtKQFkeyDownload = reSult.ToString();//.Replace('-', '+').Replace('_', '/');
        //        if (reSult.Substring(0,3)=="ERR:")
        //        {
        //            return;
        //        }
        //        SaveFileDialog dialog = new SaveFileDialog();
        //        if (!Directory.Exists("Download"))
        //        {
        //            Directory.CreateDirectory(Connection.path_excel + "Download");
        //        }
        //        dialog.InitialDirectory = Connection.path_excel + "Download";
        //        dialog.Filter = "PDF document (*.pdf)|*.pdf";
        //        DialogResult result = dialog.ShowDialog();
        //        string fileName = dialog.FileName;
        //        if (result == DialogResult.OK)
        //        {
        //            MemoryStream stream = new MemoryStream();
        //            byte[] fileBytes = Convert.FromBase64String(reSult);
        //            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
        //            fileStream.Write(fileBytes, 0, fileBytes.Length);
        //            fileStream.Flush();
        //            fileStream.Close();
        //            stream.Close();
        //            Process.Start(fileName);               
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi: " + ex.ToString());
        //    }
        //}

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmintheosodoc frm = new frmintheosodoc();
            ActivatForm(frm);
        }


        void ActivatForm(Form frm)
        {
            foreach (Document item in tabbedView1.Documents)
            {
                if (item.Caption.ToString() == frm.Text)
                {
                    tabbedView1.Controller.Activate(item);
                    return;
                }
            }
            frm.MdiParent = this;
            frm.Show();
        }

        private void brIntheosodoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmintheosodoc frm = new frmintheosodoc();
            ActivatForm(frm);
        }

        private void brBPhlai_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmphhdbiloiS frm = new frmphhdbiloiS();
            ActivatForm(frm);
        }

        private void brPHtheoso_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmphhdtheoso frm = new frmphhdtheoso();
            ActivatForm(frm);
        }

        private void brPhsot_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmphhdbisot frm = new frmphhdbisot();
            ActivatForm(frm);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmphW frm = new frmphW();
            frm.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmget_dskh frm = new frmget_dskh();
            frm.ShowDialog();
        }

        private void brBbiennhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            frminthongbao frm = new frminthongbao();
            ActivatForm(frm);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmmatkhau frm = new frmmatkhau();
            frm.ShowDialog();
        }

        private void brBzip_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmzipfile frm = new frmzipfile();
            ActivatForm(frm);
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmInLaiBN frm = new frmInLaiBN();
            ActivatForm(frm);
        }
    }
}