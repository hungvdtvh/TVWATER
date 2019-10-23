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
using DevExpress.XtraEditors.Controls;

namespace HDDT_CTNS
{
    public partial class frmphW : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        public frmphW()
        {
            InitializeComponent();
            // progressPanel1.Visible = false;
            dateEdit1.EditValue = DateTime.Now;
            //lay_ds_so_doc();
            create_radio();
        }

        void create_radio()
        {
            object[] itemValues = new object[] { 1, 2 };
            string[] itemDescriptions = new string[] { "Lấy toàn bộ dữ liệu tháng", "Lấy theo sổ đọc và số thứ tự" };
            for (int i = 0; i < itemValues.Length; i++)
            {
                radioGroup3.Properties.Items.Add(new RadioGroupItem(itemValues[i], itemDescriptions[i]));
            }
            //Select the Rectangle item.
            radioGroup3.EditValue = 1;
            set_enable();
        }

        void  lay_ds_so_doc()
        {
            // String SQL = "select c.* from (select rtrim(a.ten_so_doc) as ten_so_doc, b.* from dmsodoc a inner join (select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien from phW where thang=" + dateEdit1.DateTime.Month + " and nam=" + dateEdit1.DateTime.Year + " group by so_doc) b on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c where c.so_doc not in (select so_doc from in_sodoc d where d.thang=" + dateEdit1.DateTime.Month + " and d.nam=" + dateEdit1.DateTime.Year + " ) order by so_doc";
            String SQL = "select rtrim(ten_so_doc) as ten_so_doc, so_doc from dmsodoc";
            GetData.dien_dl_vao_SearchLookup(SQL, lookUpEdit3, "ten_so_doc", "so_doc");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int iMonth = dateEdit1.DateTime.Month;
            int iYear = dateEdit1.DateTime.Year;
            if (!check_exits_cus(iMonth,iYear))
            {
                MessageBox.Show("Chưa lấy dữ liệu khách hàng cho tháng phát hành hóa đơn này !");
                return;
            }

            // progressPanel1.Visible = true;
            //System.Threading.Thread.SpinWait(100);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                
                

                Connection.ConnectSQLSERVER();
                SqlCommand cmd = new SqlCommand();               
                if ((int)radioGroup3.EditValue == 1) // truong hop lay toan bo table phW theo thang nam
                {
                    cmd.CommandText = "Get_PhW";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
                    cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
                }
                else
                {
                    // truong hop lay 1 vai record table phW theo so doc va thang nam
                    cmd.CommandText = "Get_PhWS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
                    cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
                    cmd.Parameters.Add(new SqlParameter("@sSo_doc", lookUpEdit3.EditValue.ToString().Trim()));
                }
                              
                cmd.Connection = Connection.conn;
                // System.Threading.Thread.Sleep(500);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lấy dữ liệu xong");
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

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            lay_ds_so_doc();
        }

        private void radioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_enable();
        }

        void set_enable()
        {
            if ((int)radioGroup3.EditValue == 1)           
                lookUpEdit3.Enabled = false;          
            else           
                lookUpEdit3.Enabled = true; 
        }

        bool check_exits_cus(int iMonth, int iYear)
        {
            bool m_re = false;
            Connection.ConnectSQLSERVER();
            try
            {               
                string SQL = "SELECT * FROM D_dmkh WHERE thang=" + iMonth + " AND nam=" + iYear;
                using (SqlDataAdapter m = new SqlDataAdapter(SQL, Connection.conn))
                {
                    DataTable mc = new DataTable();
                    mc.Clear();
                    m.Fill(mc);
                    m.Dispose();
                    if (mc.Rows.Count >= 1)
                    {
                        m_re = true;
                    }
                    else
                        m_re = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return m_re;
           
        }
    }
}