using System;
using System.Reflection;
using System.Collections;
using System.Security.AccessControl;
using System.Net;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

using System.Diagnostics;


public partial class Connection
{
    public static string p_path { get; set; }
    public static string token { get; set; }
    public static string accountHsm { get; set; }
    public static string accpassHsm { get; set; }
    public static string userNameHsm { get; set; }
    public static string userPassHsm { get; set; }
    public static string patternHsm { get; set; }
    public static string serial1 { get; set; }
    public static string m_SERVER { get; set; }
    public static string m_username { get; set; }
    public static string m_password { get; set; }
    public static bool m_admin { get; set; }
    public static string path_excel { get; set; }
    public static decimal m_lan { get; set; }
    public static DataSet rs { get; set; }
    public static DataView ObjView { get; set; }
    public static IDataAdapter adpt { get; set; }
    public static SqlConnection conn { get; set; }
    public static int m_so_hoa_don { get; set; }  // phan moi doan 100 hd de chuyen thanh XML
    public static int pl_timeout { get; set; }  // phan moi doan 100 hd de chuyen thanh XML
    public static string ngayTempHeThong = DateTime.Now.ToString(" MM/dd/yyyy");
    public static DateTime ngayTrongFile { get; set; }
    public static string ngayTempTrongFile { get; set; }
    public static int  soluongHD { get; set; }

    //public static void ConnectToData()
    //{
    //    conn = new OleDbConnection();
    //    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @Connection.p_path + "db.mdb;Jet OLEDB:Database Password=bvtv@2018;";
    //    try
    //    {
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //    }
    //    catch
    //    {
    //        if (m_lan == 1)
    //            MessageBox.Show("Failed to connect to data source");
    //    }
    //    finally
    //    {
    //       // MessageBox.Show(conn.State.ToString());
    //    }
    //}

    public static void ConnectSQLSERVER()
    {
        conn = new SqlConnection();
        conn.ConnectionString = "Data Source=" + m_SERVER + ";Initial Catalog=TVWater;User ID=nuoctvh;Password=123@123;MultipleActiveResultSets=True; Integrated Security=True";
        //conn.ConnectionString = "Data Source="+m_SERVER+";Initial Catalog=TVWater;User ID=tvhWater;Password=123@123;MultipleActiveResultSets=True";
        //conn.ConnectionString = "Data Source=" + m_SERVER + ";Initial Catalog=TVWater;User ID=tvhWater;Password=123@123;MultipleActiveResultSets=True;Integrated Security=True";
        //kết nối nội bộ, sử dụng trong mạng Lan bị lổi Integrated Security=True
        //conn.ConnectionString = "Data Source=" + m_SERVER + ";Initial Catalog=TVWater;User ID=nuoctvh;Password=123@123;MultipleActiveResultSets=True; Integrated Security=True";
        //conn.ConnectionString = "Data Source=10.90.99.167;Network Library=DBMSSOCN;Initial Catalog=tvWater;User ID=tvhWater;Password=123@123;MultipleActiveResultSets = true;";
        //conn.ConnectionString = "Data Source=DESKTOP-C5AHIT8,1433;Initial Catalog=TVWater;User ID=tvhWater;Password=123@123;MultipleActiveResultSets = true;";
        // MessageBox.Show(conn.ConnectionString);
        try
        {
            // MessageBox.Show(t);
            if (conn.State == ConnectionState.Closed)
            {
              // int t= conn.ConnectionTimeout;
                conn.Open();          
               // MessageBox.Show("connect tu xa ok !");
            }
        }
        catch(Exception ex)
        { MessageBox.Show(ex.Message); }     
    }

    public static void DisconnectData()
    {
        try
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        catch
        {
            // MessageBox.Show("Failed to disconnect to data source");
        }
        //finally
        //{
        //    Connection.conn.Close();
        //}
    }


    public static void ReadConnectFile()
    {
        if (!System.IO.File.Exists(@p_path + "setting.ini"))
        {
            MessageBox.Show("File initial chưa được tạo !");
            return;
        }
        StreamReader Re = System.IO.File.OpenText(@p_path + "setting.ini");
        string input = null;
        string[] s = new string[8];
        int i = 0;
        while ((input = Re.ReadLine()) != null)
        {
            if (input != "")
            {
                s[i] = input;
                i++;
            }
        }
        Re.Close();
        if (i >= 7)
        {
            accountHsm =Decrypt(s[0].Trim(),true);
            accpassHsm = Decrypt(s[1].Trim(), true);
            userNameHsm = Decrypt(s[2].Trim(), true);
            userPassHsm = Decrypt(s[3].Trim(), true);
            patternHsm = s[4].Trim();
            serial1 = s[5].Trim();
            m_SERVER = s[6].Trim();
            soluongHD = Convert.ToInt32(Decrypt(s[7].Trim(), true));
            ////ngayTempTrongFile= Decrypt(s[7].Trim(), true);
            ////ngayTrongFile = Convert.ToDateTime(ngayTempTrongFile);
            ////DateTime ngayHeThong = Convert.ToDateTime(ngayTempHeThong);
            ////int compare = DateTime.Compare(ngayHeThong, ngayTrongFile);
            int soTemp = GetData.Get_InvoiceCount("GetInvoiceCount");
            if (soluongHD-soTemp<=500)
            {
                MessageBox.Show("Số lượng hóa đơn của bạn < 500! Vui lòng liên hệ VNPT để mua thêm hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (soluongHD == soTemp)
            {
                MessageBox.Show("Vui lòng liên hệ TTCNTT để được sử dụng tiếp. Số lượng hóa đơn đã hết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Nếu hết hóa đơn tắt luôn chương trình không cho xài nữa
                System.Diagnostics.Process thisProc = System.Diagnostics.Process.GetCurrentProcess();
                thisProc.Kill();
            }
        }
        else
        {
            MessageBox.Show("File initial chưa đúng !");
        }
    }


    public static string Encrypt(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        // System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        // Get the key from config file

        string key = token;
        //System.Windows.Forms.MessageBox.Show(key);
        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        //get the byte code of the string

        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        //System.Configuration.AppSettingsReader settingsReader =
        //                                    new AppSettingsReader();
        ////Get your key from config file to open the lock!
        string key = token;

        if (useHashing)
        {
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();
        }
        else
        {
            //if hashing was not implemented get the byte code of the key
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
        }

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. 
        //We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
                             toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor                
        tdes.Clear();
        //return the Clear decrypted TEXT
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
    public static string convert_date(string date)
    {
        string s1 = date.Substring(0, 2);
        string s2 = date.Substring(3, 2);
        string s3 = date.Substring(6, 4);
        date = s2 + "/" + s1 + "/" + s3;
        return date;
        

    }
}