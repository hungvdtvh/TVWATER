using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HDDT_CTNS
{
    public partial class frmmatkhau : DevExpress.XtraEditors.XtraForm
    {
        public frmmatkhau()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text !=Connection.m_password)
            {
                MessageBox.Show("Mật khẩu cũ không đúng");
                return;
            }
            if (textEdit2.Text != textEdit3.Text)
            {
                MessageBox.Show("Mật khẩu mới không giống nhau");
                return;
            }
            bool m_re= GetData.Excute_SQL_Command("UPDATE authen SET m_password='" + Connection.Encrypt(textEdit2.Text,true)+"' WHERE m_username='"+Connection.m_username+"';");
            if (m_re)
            {
                MessageBox.Show("Đổi mật khẩu thành công !");
                Connection.m_password = textEdit2.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra !");
            }
        }
    }
}