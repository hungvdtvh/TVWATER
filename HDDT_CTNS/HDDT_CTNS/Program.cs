using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Microsoft.Win32;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Globalization;

namespace HDDT_CTNS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //rkApp.SetValue("VNPT Envoice", '"' + Application.ExecutablePath + '"' + " -minimize");

            ////////////RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            ////////////rkey.SetValue("sShortDate", "dd/MM/yyyy");
            ////////////rkey.SetValue("sLongDate", "dd/MM/yyyy");
            ////////////rkey.SetValue("LocaleName", "vi-VN");
            //////////////rkey.SetValue("LocaleName", "vi-VN");
            ////////////// Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("vi-VN");
            ci.NumberFormat.NumberDecimalSeparator = ",";

            Thread.CurrentThread.CurrentCulture = ci;

            int p = Application.ExecutablePath.Length - (Assembly.GetExecutingAssembly().GetName().Name.Length + 4);
            Connection.p_path = Application.ExecutablePath.Substring(0, p);
            Connection.m_lan = 1;
            Connection.m_so_hoa_don = 50;
            Connection.pl_timeout = 500000;
            Connection.token = "VNPT Tra Vinh";
            //Connection.patternHsm = "01GTKT0/009";
            //Connection.serial1 = "KI/18E";


            //Connection.patternHsm = "01GTKT0/001";
            //Connection.serial1 = "DN/18E";
            Connection.ReadConnectFile();
            Connection.ConnectSQLSERVER();
            if (Connection.conn.State == ConnectionState.Closed)
            {
                MessageBox.Show("Không kết nối được SQL SERVER");
                return;
            }            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ////////////////NotifyIcon icon = new NotifyIcon();
            ////////////////icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            ////////////////icon.Text = "VNPT Envoice";
            //////////////////if (icon.Visible)
            ////////////////icon.Click += delegate { icon.Visible = false; show_mainForm(); };
            //////////////////else
            //////////////////    icon.Click += delegate { icon.Visible = true; show_mainForm(); };
            ////////////////////icon.Click += delegate { show_mainForm(); };
            ////////////////////icon.Visible = true;
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.Run(new frmlogin());            
            //Application.Run(new Form2());
        }
        static void show_mainForm()
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == "Form1")
                {
                    NotifyIcon icon = new NotifyIcon();
                    icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                    icon.Text = "VNPT Envoice";
                    icon.Click += delegate { icon.Visible = false; show_mainForm(); };
                    // icon.Click += delegate {show_mainForm(); };
                    icon.Visible = true;
                    frm.WindowState = FormWindowState.Maximized;
                    if (Connection.m_lan == 1)
                    {
                        frm.Show();
                        Connection.m_lan += 1;
                        return;
                    }
                    if (!frm.Visible)
                    {
                        frm.Show();
                        Connection.m_lan += 1;
                    }
                    else
                    {
                        frm.Hide();
                        icon.Visible = true;
                    }

                }
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process thisProc = System.Diagnostics.Process.GetCurrentProcess();
                thisProc.Kill();
            }
            catch { }
        }

    }
}