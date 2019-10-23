using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HDDT_CTNS
{
    public partial class frmget_dskh : DevExpress.XtraEditors.XtraForm
    {
        public frmget_dskh()
        {
            InitializeComponent();
            dateEdit1.EditValue = DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // progressPanel1.Visible = true;
            //System.Threading.Thread.SpinWait(100);
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                //int iMonth = dateEdit1.DateTime.Month;
                //int iYear = dateEdit1.DateTime.Year;

                Connection.ConnectSQLSERVER();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Get_DSKH";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
                //cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
                cmd.Connection = Connection.conn;
                cmd.CommandTimeout = 300;
                // System.Threading.Thread.Sleep(500);
                cmd.ExecuteNonQuery();
                //table D_dmkh luu gia tri thang va nam lay du lieu khach hang
                string SQL1 = "DELETE FROM D_dmkh WHERE thang=" + dateEdit1.DateTime.Month + " AND nam=" + dateEdit1.DateTime.Year;
                bool t1 = GetData.Excute_SQL_Command(SQL1);
                string SQL = "INSERT INTO D_dmkh(thang,nam) VALUES(" + dateEdit1.DateTime.Month + "," + dateEdit1.DateTime.Year + ")";
               bool t= GetData.Excute_SQL_Command(SQL);
                if (t)
                {
                    MessageBox.Show("Đã lấy dữ liệu xong khách hàng xong !");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
            finally
            {
                // progressPanel1.Visible = false;
                Cursor.Current = Cursors.Default;
                Connection.DisconnectData();
            }
        }
    }
}