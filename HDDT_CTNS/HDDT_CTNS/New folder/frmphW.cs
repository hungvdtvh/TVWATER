using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace HDDT_CTNS
{
    public partial class frmintheosodoc : DevExpress.XtraEditors.XtraForm
    {
        public bool m_update;
        public frmintheosodoc()
        {
            InitializeComponent();
            load_dl(gridControl1);
            //Connection.EnableControl(this, false);
            EnableControl(false);
            gridView1.ShowFindPanel();
        }

        public static void load_dl(GridControl gridControl1)
        {
            Connection.dien_dl_vao_grid("SELECT * FROM dt_chi ORDER BY hieu_luc", gridControl1);
        }    

     

        private void cmdThem_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            txtma.Text = Connection.get_max_ma("DTC");
            txtma.ReadOnly = true;
            txtten.Text = "";
            txtdc.Text = "";
            txtdt.Text = "";
            txtmsthue.Text = "";
            txttk.Text = "";
            txtnh.Text = "";
            txtma.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtma.Text == "" || txtten.Text == "" || txtdc.Text == "")
            {
                MessageBox.Show("Nhập chưa đủ thông tin !");
                return;
            }
            if (!m_update)
            {
                if (Connection.CheckExits(txtma.Text.Trim(), "dt_thu", "ma_dt", false))
                {
                    MessageBox.Show("Mã đối tượng này đã tồn tại !");
                    return;
                }
                try
                {
                    Connection.ConnectData();
                    string SQL = "INSERT INTO dt_chi(ma_dt,ten_dt,dc_dt,so_dt,ms_thue,tk,ngan_hang,hieu_luc) VALUES('" + txtma.Text + "','" + txtten.Text + "','" + txtdc.Text + "','" + txtdt.Text + "','" + txtmsthue.Text + "','" + txttk.Text.Replace("'", "") + "','" + txtnh.Text + "'," + chkhieuluc.EditValue + ");";
                    using (OleDbCommand b = new OleDbCommand(SQL, Connection.conn))
                    {
                        b.ExecuteNonQuery();
                        b.Dispose();
                    }
                    txtma.Text = Connection.get_max_ma("CT");
                    txtma.ReadOnly = true;
                    txtten.Text = "";
                    txtdc.Text = "";
                    txtdt.Text = "";
                    txtmsthue.Text = "";
                    txttk.Text = "";
                    txtnh.Text = "";
                    chkhieuluc.EditValue = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    Connection.DisconnectData();
                    load_dl(gridControl1);
                    // frmnhacc.load_dl(frmnhacc. gridControl1);
                }
            }
            else
            {
                try
                {
                    Connection.ConnectData();
                    string SQL = "UPDATE dt_chi SET ma_dt='" + txtma.Text + "',ten_dt='" + txtten.Text + "',dc_dt='" + txtdc.Text + "',so_dt='" + txtdt.Text + "',ms_thue='" + txtmsthue.Text + "',tk='" + txttk.Text.Replace("'", "") + "',ngan_hang='" + txtnh.Text + "',hieu_luc=" + chkhieuluc.EditValue + " WHERE ma_dt='" + txtma.Text.Trim() + "';";
                    using (OleDbCommand b = new OleDbCommand(SQL, Connection.conn))
                    {
                        b.ExecuteNonQuery();
                        b.Dispose();
                    }
                    txtma.Text = "";
                    txtten.Text = "";
                    txtdc.Text = "";
                    txtdt.Text = "";
                    txtmsthue.Text = "";
                    txttk.Text = "";
                    txtnh.Text = "";
                    chkhieuluc.EditValue = 0;
                    EnableControl(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    m_update = false;
                    Connection.DisconnectData();
                    load_dl(gridControl1);
                    // frmnhacc.load_dl(frmnhacc. gridControl1);
                }
            }

            txtma.ReadOnly = false;
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            string SQL = "DELETE FROM dt_chi WHERE ma_dt='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, ma_dt).ToString() +"';";
            try
            {
                Connection.ConnectData();
                using (OleDbCommand b = new OleDbCommand(SQL, Connection.conn))
                {
                    b.ExecuteNonQuery();
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                load_dl(gridControl1);
                MessageBox.Show("Đã xóa xong !");                   
                Connection.DisconnectData();                
            }
        }

        private void EnableControl(bool logic)
        {
            txtma.Enabled = logic;
            txtten.Enabled = logic;
            txtdc.Enabled = logic;
            txtdt.Enabled = logic;
            txtmsthue.Enabled = logic;
            txttk.Enabled = logic;
            txtnh.Enabled = logic;
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            m_update = true;
            EnableControl(true);
            txtma.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, ma_dt).ToString();
            txtten.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, ten_dt).ToString();
            txtdc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, dc_dt).ToString();
            txtdt.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, so_dt).ToString();
            txtmsthue.Text=gridView1.GetRowCellValue(gridView1.FocusedRowHandle, ms_thue).ToString();
            txttk.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, tk).ToString().Replace("'","");
            txtnh.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, Ngan_hang).ToString();
            chkhieuluc.Checked = (bool)(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, hieu_luc) == null ? false : gridView1.GetRowCellValue(gridView1.FocusedRowHandle, hieu_luc));
            txtma.Focus();
            txtma.ReadOnly = true;
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // ten_dt.OptionsFilter.
        }
    }
}