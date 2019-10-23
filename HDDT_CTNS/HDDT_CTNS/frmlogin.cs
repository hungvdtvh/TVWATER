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
    public partial class frmlogin : DevExpress.XtraEditors.XtraForm
    {
        int m_count = 0;
        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {           
           progressPanel1.Visible = true;
           if (m_count==4)
             this.Close();
           bool rs= checkLogin(Connection.Encrypt(txtusername.Text,true), Connection.Encrypt(txtpassword.Text,true));
           if (rs)
           {
               this.Hide();
               Form1 frm = new Form1();
               frm.ShowDialog();
               this.Close();
           }
           else
           {
               m_count += 1;
               progressPanel1.Visible = false;
               MessageBox.Show("Tên đăng nhập hoặc mât khẩu không đúng !");
               return;
           }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
          // txtusername.Text=Connection.EncrytedString("123");
           this.Close();
        }

        private bool checkLogin(string usr, string pass)
        {
            bool result;
            string SQL = "SELECT * FROM authen WHERE m_username='" + usr + "' and m_password='" + pass + "';";
            try
            {
                Connection.ConnectSQLSERVER();               
                SqlDataAdapter c = new SqlDataAdapter(SQL, Connection.conn);
                DataTable tb = new DataTable();
                c.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    result = true;
                    Connection.m_username = txtusername.Text;
                    Connection.m_password = txtpassword.Text;
                    Connection.m_admin = (bool)tb.Rows[0]["admin"];
                }

                else
                    result = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: "+e.Message);
                result = false;
            }
            finally
            {
                Connection.DisconnectData();
            }

            return result;
        }
    }
}