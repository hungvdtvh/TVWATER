using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
public partial class GetData
{
    
    public static void dien_dl_vao_grid(string SQL, GridControl grid)
    {
        Connection.ConnectSQLSERVER();
        // string SQL = "SELECT * FROM nha_cc ORDER BY hieu_luc";
        grid.DataSource = null;
        try
        {
            System.Threading.Thread.Sleep(500);
            using (SqlDataAdapter m = new SqlDataAdapter(SQL, Connection.conn))
            {
                DataTable mc = new DataTable();
                mc.Clear();
                m.Fill(mc);
                m.Dispose();
                grid.DataSource = mc;
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
    }

    public static DataTable Get_InvoiceList(string sSo_doc,int iMonth, int iYear,string In_voice)
    {        
        DataTable mc = new DataTable();
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = In_voice;
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.Add(new SqlParameter("@sSo_doc",SqlDbType.NVarChar));
           // cmd.Parameters["@sSo_doc"].Value = sSo_doc;
            cmd.Parameters.Add(new SqlParameter("@sSo_doc", sSo_doc));
            cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
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

    public static DataTable Get_InvoiceListBN(string sSo_doc, int iMonth, int iYear, Int32 stt_bd, Int32 stt_kt, string In_voice)
    {
        DataTable mc = new DataTable();
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50000;
            cmd.CommandText = In_voice;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@sSo_doc", sSo_doc));
            cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
            cmd.Parameters.Add(new SqlParameter("@isttbd", stt_bd));
            cmd.Parameters.Add(new SqlParameter("@isttkt", stt_kt));
            cmd.Connection = Connection.conn;
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
    public static DataTable Get_InvoiceListBNErr(int iMonth, int iYear, Int32 stt_bd, Int32 stt_kt, string In_voice)
    {
        DataTable mc = new DataTable();
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50000;
            cmd.CommandText = In_voice;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@sSo_doc", sSo_doc));
            cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
            cmd.Parameters.Add(new SqlParameter("@isttbd", stt_bd));
            cmd.Parameters.Add(new SqlParameter("@isttkt", stt_kt));
            cmd.Connection = Connection.conn;
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
    public static DataTable Get_InvoiceListMiss(int iMonth, int iYear, string In_voice)
    {
        DataTable mc = new DataTable();
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = In_voice;
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
            cmd.Connection = Connection.conn;
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
    /// <summary>
    /// Hàm lấy số lượng hóa đơn đã phát hành
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="cmb"></param>
    /// <param name="m_field"></param>
    /// <param name="m_values"></param>
    public static int Get_InvoiceCount(string In_voice)
    {
        //DataTable mc = new DataTable();
        int soLuongHD = 0;
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = In_voice;
            cmd.CommandType = CommandType.StoredProcedure;            
            //cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            //cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
            cmd.Connection = Connection.conn;
            soLuongHD = Convert.ToInt32(cmd.ExecuteScalar());
            // System.Threading.Thread.Sleep(500);
            //using (SqlDataAdapter m = new SqlDataAdapter(cmd))
            //{
            //    //mc.Clear();
            //    //m.Fill(soLuongHD);
            //    m.Dispose();
            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi:" + ex.Message);
        }
        finally
        {
            Connection.DisconnectData();
        }
        return soLuongHD;
    }

    public static int Get_CheckPrint(int iMonth, int iYear, string So_doc)
    {
        //DataTable mc = new DataTable();
        int m_lan_in = 0;
        try
        {
            Connection.ConnectSQLSERVER();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 m_lan_in from PrintBN where thang = @iMonth and nam = @iYear and so_doc = @sSo_doc  order by m_lan_in desc";
            
            cmd.Parameters.Add(new SqlParameter("@iMonth", iMonth));
            cmd.Parameters.Add(new SqlParameter("@iYear", iYear));
            cmd.Parameters.Add(new SqlParameter("@sSo_doc", So_doc));
            cmd.Connection = Connection.conn;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if (sqlDataReader.Read())
            {
                m_lan_in = sqlDataReader.GetInt32(0);
            }
            //soLuongHD = Convert.ToInt32(cmd.ExecuteScalar());
            // System.Threading.Thread.Sleep(500);
            //using (SqlDataAdapter m = new SqlDataAdapter(cmd))
            //{
            //    //mc.Clear();
            //    //m.Fill(soLuongHD);
            //    m.Dispose();
            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi:" + ex.Message);
        }
        finally
        {
            Connection.DisconnectData();
        }
        return m_lan_in;
    }


    public static void dien_dl_vao_SearchLookup(string SQL, SearchLookUpEdit cmb, string m_field, string m_values)
    {
        Connection.ConnectSQLSERVER();
        // string SQL = "SELECT * FROM nha_cc ORDER BY hieu_luc";
        try
        {
            using (SqlDataAdapter m = new SqlDataAdapter(SQL, Connection.conn))
            {
                DataTable mc = new DataTable();
                mc.Clear();
                m.Fill(mc);
                m.Dispose();
                cmb.Properties.DataSource = mc;
                cmb.Properties.DisplayMember = m_field;
                cmb.Properties.ValueMember = m_values;
                //  cmb.Properties.Columns

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
    }

    public static bool Excute_SQL_Command(string SQL)
    {
        bool m_return;
        try
        {
            Connection.ConnectSQLSERVER();
            using (SqlCommand b = new SqlCommand(SQL, Connection.conn))
            {
                b.ExecuteNonQuery();                
                b.Dispose();
                m_return = true;
                
            }
        }
        catch
        {
            m_return = false;
        }
        finally
        {
            Connection.DisconnectData();
        }
        return m_return;
    }

}
