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
using HDDT_CTNS.bsWebservice;
using HDDT_CTNS.plWebservice;
using HDDT_CTNS.portalWebsevice;

namespace HDDT_CTNS
{
    public partial class frmphhdbiloiS : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        DataTable dt;
        int type = 2; // loại hóa đơn chỉnh sửa : 2=> tăng, 3=> giảm, 4=>thông tin
        //= new DataTable();
        public frmphhdbiloiS()
        {
            InitializeComponent();
            gridView2.ShowFindPanel();
            dateEdit1.EditValue = DateTime.Now;
            dateEdit2.EditValue = DateTime.Now;
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
            gridView1.RowClick += GridView1_RowClick;            
            //gridView2.RowClick += GridView2_RowClick;
            gridView1.KeyUp += GridView1_KeyUp;
            //gridView2.ShownEditor += gridView2_ShownEditor;
            //gridView2.Click += new RowEventHandler(gridView2_MouseDown);
            checkEdit1.Checked = false;
            //txtbd.Enabled = false;
            //txtkt.Enabled = false;

            //.KeyPress += GridView1_KeyPress;
        }

        //void gridView2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    var hitInfo = gridView2.CalcHitInfo(e.Location);
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}
        private void GridView1_KeyUp(object sender, KeyEventArgs e)
        {
            get_data();
        }

        private void GridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            get_data();
        }

        //private void GridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i,m_check)))
        //        {
        //            m_re = true;                    
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}


        public static void load_dl(GridControl gridControl1,int thang, int nam)
        {
            string SQL = "select b.so_doc, b.ten_so_doc from in_sodoc a, dmsodoc b where  a.so_doc=b.so_doc and a.thang=" + thang + " and a.nam=" + nam + " order by so_doc";
            GetData.dien_dl_vao_grid(SQL, gridControl1);
        }

        private void cmdview_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            load_dl(gridControl1, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
        }

        void get_data()
        {
            dt = new DataTable();
           // dt.Clear();
            txtSodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"so_doc").ToString().Trim();
            txtTensodoc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ten_so_doc").ToString();
            dt = GetData.Get_InvoiceList(txtSodoc.Text, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, "InvoiceListErr");
            if (checkEdit1.Checked==true)
            {
                txtbd.Enabled = true;
                txtkt.Enabled = true;
                txtbd.Text = dt.Rows[0]["stt_order"].ToString();
                txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
                txtbd.Enabled = false;
                txtkt.Enabled = false;
            }
            else
            {
                txtbd.Text = dt.Rows[0]["stt_order"].ToString();
                txtkt.Text = dt.Rows[dt.Rows.Count - 1]["stt_order"].ToString();
            }            
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
            
            if (!checkEdit1.Checked)
            {
                
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
                         string m_keyR = dt.Rows[j]["m_key"].ToString().Trim();
                         m_XML = PhatHanh.phat_hanh_hoa_don_thay_the(txtTensodoc.Text.ToString().Trim(), dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year,dateEdit2.Text);
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
                    }

                }
            }
            else
            {
                if (isCheck())
                {
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
                        {
                            string m_keyR = dt.Rows[i]["m_key"].ToString().Trim();
                            m_XML = PhatHanh.phat_hanh_hoa_don_thay_the(txtTensodoc.Text.ToString().Trim(), dt, i, i, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year,dateEdit2.Text);
                            // goi webservice phát hành hóa đơn
                            bsWebservice.BusinessService pl = new bsWebservice.BusinessService();
                            reSult = pl.ReplaceInvoie(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, m_keyR, "", 0);
                            if (reSult.Substring(0, 2) == "OK")
                            {
                                // cap nhat so lan ph tang len 1 log file vào table DaPH
                                Connection.ConnectSQLSERVER();
                                SQL = "UPDATE DaPH SET m_lan=m_lan+1,m_key='" + dt.Rows[i]["m_key"].ToString().Trim() + "' WHERE ma_kh='" + dt.Rows[i]["ma_kh"].ToString().Trim() + "' AND thang=" + Convert.ToInt16(dt.Rows[i]["thang"]) + " AND nam=" + Convert.ToInt16(dt.Rows[i]["nam"]);
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
                                MessageBox.Show("Chưa phát hành hóa đơn thứ " + i.ToString());
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Chưa check chọn hóa đơn nào");
            }
            get_data();
        }

        public bool isCheck()
        {
            bool m_return = false;
            for (int i = 0; i < gridView2.RowCount; i++)
                if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
                {
                    m_return = true;
                    break;
                }
            return m_return;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                txtbd.Enabled = false;
                txtkt.Enabled = false;
                gridView2.Columns["status"].Visible = true;           
            }
            else
            {
                txtbd.Enabled = true;
                txtkt.Enabled = true;
                gridView2.Columns["status"].Visible = false;
            }
        }

        private void btnDieuChinh_Click(object sender, EventArgs e)
        {
            int m_bd = Convert.ToInt16(txtbd.Text);
            int m_kt = Convert.ToInt16(txtkt.Text);
            int m_bd_indexdt = 0;
            int m_kt_indexdt = 0;
            string m_XML = "";
            string SQL;
            string reSult = "";
            // int[,] a;

            if (!checkEdit1.Checked)
            {

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
                        string m_keyR = dt.Rows[j]["m_key"].ToString().Trim();
                        m_XML = PhatHanh.phat_hanh_hoa_don_dieu_chinh(txtTensodoc.Text.ToString().Trim(), dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dateEdit2.Text,type);
                        // goi webservice phát hành hóa đơn
                        //string m_keyR = dt.Rows[j]["m_key"].ToString();

                        //string m_keyR = "1804660104$1";
                        bsWebservice.BusinessService pl = new bsWebservice.BusinessService();
                        pl.Timeout = Connection.pl_timeout;
                        reSult = pl.AdjustInvoie(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, m_keyR, "", 0);
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
                    }

                }
            }
            else
            {
                if (isCheck())
                {
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
                        {
                            string m_keyR = dt.Rows[i]["m_key"].ToString().Trim();
                            m_XML = PhatHanh.phat_hanh_hoa_don_dieu_chinh(txtTensodoc.Text.ToString().Trim(), dt, i, i, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dateEdit2.Text,type);
                            // goi webservice phát hành hóa đơn
                            bsWebservice.BusinessService pl = new bsWebservice.BusinessService();
                            reSult = pl.AdjustInvoie(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, m_keyR, "", 0);
                            if (reSult.Substring(0, 2) == "OK")
                            {
                                // cap nhat so lan ph tang len 1 log file vào table DaPH
                                Connection.ConnectSQLSERVER();
                                SQL = "UPDATE DaPH SET m_lan=m_lan+1,m_key='" + dt.Rows[i]["m_key"].ToString().Trim() + "' WHERE ma_kh='" + dt.Rows[i]["ma_kh"].ToString().Trim() + "' AND thang=" + Convert.ToInt16(dt.Rows[i]["thang"]) + " AND nam=" + Convert.ToInt16(dt.Rows[i]["nam"]);
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
                                MessageBox.Show("Chưa phát hành hóa đơn thứ " + i.ToString());
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Chưa check chọn hóa đơn nào");
            }
            get_data();


        }
        /// <summary>
        /// gán giá trị cho thẻ Type trong xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            if (radioGroup.SelectedIndex == 0)
            {
                type = 2;
                //MessageBox.Show("type=2");
            }
            if (radioGroup.SelectedIndex==1)
            {
                type = 3;
                //MessageBox.Show("type=3");
            }
            if (radioGroup.SelectedIndex == 2)
            {
                type = 4;
                //MessageBox.Show("type=4");
            }

        }

        private void btnPhatHanhHuy_Click(object sender, EventArgs e)
        {
            int m_bd = Convert.ToInt16(txtbd.Text);
            int m_kt = Convert.ToInt16(txtkt.Text);
            int m_bd_indexdt = 0;
            int m_kt_indexdt = 0;
            string m_XML = "";
            string SQL;
            string reSult = "";
            // int[,] a;

            if (!checkEdit1.Checked)
            {

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
                        string m_keyR = dt.Rows[j]["m_key"].ToString().Trim();
                        m_XML = PhatHanh.phat_hanh_hoa_don_huy(txtTensodoc.Text.ToString().Trim(), dt, j, j, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dateEdit2.Text);
                        // goi webservice phát hành hóa đơn
                        //string m_keyR = dt.Rows[j]["m_key"].ToString();
                        //ghi file xml vao thu muc chua file exe
                        if (m_XML != null)
                        {
                            string strStartupPath = Application.StartupPath;
                            string strXmlDataFilePath = strStartupPath + "\\" + "einv" + ".xml";
                            System.IO.StreamWriter file = new System.IO.StreamWriter(strXmlDataFilePath);
                            file.WriteLine(m_XML);
                            file.Close();
                        }

                        //string m_keyR = "1804660104$1";
                        plWebservice.PublishService pl = new plWebservice.PublishService();
                        pl.Timeout = Connection.pl_timeout;
                        reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1,0);
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
                            MessageBox.Show("Chưa phát hành hóa đơn thứ " + j.ToString()+ reSult.ToString()+ "\t"+ m_XML.ToString());
                        }
                    }

                }
            }
            else
            {
                if (isCheck())
                {
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
                        {
                            string m_keyR = dt.Rows[i]["m_key"].ToString().Trim();
                            m_XML = PhatHanh.phat_hanh_hoa_don_huy(txtTensodoc.Text.ToString().Trim(), dt, i, i, dateEdit1.DateTime.Month, dateEdit1.DateTime.Year, dateEdit2.Text);
                            // goi webservice phát hành hóa đơn
                            if (m_XML != null)
                            {
                                string strStartupPath = Application.StartupPath;
                                string strXmlDataFilePath = strStartupPath + "\\" + "einv" + ".xml";
                                System.IO.StreamWriter file = new System.IO.StreamWriter(strXmlDataFilePath);
                                file.WriteLine(m_XML);
                                file.Close();
                            }
                            plWebservice.PublishService pl = new plWebservice.PublishService();
                            reSult = pl.ImportAndPublishInv(Connection.accountHsm, Connection.accpassHsm, m_XML, Connection.userNameHsm, Connection.userPassHsm, Connection.patternHsm, Connection.serial1,0);
                            if (reSult.Substring(0, 2) == "OK")
                            {
                                // cap nhat so lan ph tang len 1 log file vào table DaPH
                                Connection.ConnectSQLSERVER();
                                SQL = "UPDATE DaPH SET m_lan=m_lan+1,m_key='" + dt.Rows[i]["m_key"].ToString().Trim() + "' WHERE ma_kh='" + dt.Rows[i]["ma_kh"].ToString().Trim() + "' AND thang=" + Convert.ToInt16(dt.Rows[i]["thang"]) + " AND nam=" + Convert.ToInt16(dt.Rows[i]["nam"]);
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
                                MessageBox.Show("Chưa phát hành hóa đơn thứ " + i.ToString());
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Chưa check chọn hóa đơn nào");
            }
            get_data();

        }



        //private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        //{
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}

        //void gridView2_ShownEditor(object sender, EventArgs e)
        //{
        //    //gridView2.ActiveEditor.EditValueChanged+= new MouseEventHandler(ActiveEditor_MouseUp);
        //    gridView2.CellValueChanged += GridView2_CellValueChanged;
        //}

        //private void GridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}

        //private void ActiveEditor_EditValueChanged(object sender, EventArgs e)
        //{
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}

        //void ActiveEditor_MouseUp(object sender, MouseEventArgs e)
        //{
        //    //MessageBox.Show(gridView2.GetRowCellValue((int)gridView2.FocusedRowHandle, "Ten_kh").ToString());
        //    bool m_re = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}

        //private void gridView2_MouseDown_1(object sender, MouseEventArgs e)
        //{
        //    bool m_re = false;
        //    MessageBox.Show(gridView2.FocusedValue.ToString());
        //    //MessageBox.Show(gridView2.GetRowCellValue(1, status).ToString());
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        if (Convert.ToBoolean(gridView2.GetRowCellValue(i, status)))
        //        {
        //            m_re = true;
        //            break;
        //        }
        //    }
        //    checkEdit1.Checked = m_re;
        //}
    }

}