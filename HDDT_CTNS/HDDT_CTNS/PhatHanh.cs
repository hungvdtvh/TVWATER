//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
//using NPOI.SS.Util;
//using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class PhatHanh
{
    public static bool checkNull=false;//biến dùng để làm cờ check null tên KH
    public static string key;
    public static string ky_ph;
    public static string ngay_ph;
    public static string so_hd;
    //public static string ngay_ph_nguoc;
    public static string ma_kh;
    public static string ten_kh;
    public static string ten_ct;
    public static string dia_chi;
    public static string dien_thoai;
    public static string pt_tt="CK/TM";
    public static string ms_thue;
    public static int chiso_moi;
    public static int chiso_cu;
    public static int so_luong;
    public static string so_doc;
    public static string ten_so_doc;
    public static int don_gia;
    public static string noi_dung; 
    public static string tieu_muc;
    public static long thanh_tien;
    public static long muc_thue_GTGT;
    public static long thue;
    public static long muc_thue_BVMT;
    public static long thue_BVMT;
    public static long tong_tien;
    //Hàm phân đoạn m_so với m_run đoạn trả về mãng 2 chiều tương ứng với các chỉ số
    //[1,i]=[1,i-1]+m_run
    public static int[,] phan_doan(Int32 m_so, int m_run)
    {
        int n = 0;
        int max_in_1 = m_so / m_run;
        if (m_so % m_run > 0)
            n += max_in_1 + 1;
        else
            n = max_in_1;
        int[,] a = new int[2, n];
        a[0, 0] = 0;
        if (max_in_1 > 0)
        {
            a[1, 0] = m_run-1;
            for (int i = 1; i < n; i++)
            {
                if (a[1, i - 1] + m_run < m_so)
                    a[1, i] = a[1, i - 1] + m_run;
                else
                    a[1, i] = m_so-1;
                    a[0, i] = a[1, i - 1] + 1;
                //m_run += m_run;
            }
        }
        else
            a[1, 0] = m_so-1;
        return a;
    }
    //End Hàm phân đoạn m_so với m_run đoạn trả về mãng 2 chiều tương ứng với các chỉ số

    public static string phat_hanh_hoa_don(string m_sodoc, DataTable dt, int m_bd, int m_kt, int iMonth, int iYear, string m_file, string ngay_ph_hd)
    {
        int n=0;
        StringBuilder sb = new StringBuilder("");
        sb.Capacity = 32 * 1024 * 1024;
        if (File.Exists(Connection.p_path + m_file + ".xml"))
        {
            File.Delete(Connection.p_path + m_file + ".xml");            
        }
        //StreamWriter outfile = new StreamWriter(Connection.p_path + m_file + ".xml", true);

        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            // thêm column để lưu lại key
            if (!IsColumnExist(dt,"m_key"))
            {
                dt.Columns.Add("m_key", typeof(System.String));
            } 
            
            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            sb.Append("<Invoices>");
            //sb.Append("<BillTime>" + iYear.ToString()+ (iMonth<10? "0"+iMonth.ToString():iMonth.ToString()) + "</BillTime>");
            n = 0;
            for (int i = m_bd; i <= m_kt; i++)
            {
                //if (m_file !="" && n % 10000 == 0) // tao file
                //{
                //    n++;                 
                //    outfile.Write(sb.ToString());
                //    sb.Length = 0;               
                //}else
                //{
                //    outfile.Close();
                //}

                ngay_ph = dt.Rows[i]["ngay_ct0"].ToString().Trim().Substring(0,10);                  
                string tempString = iYear.ToString().Trim().Substring(2,2)+ dt.Rows[i]["ma_kh"].ToString().Trim() + padstring(iMonth.ToString());
                if (!string.IsNullOrEmpty(tempString))
                {
                    key = tempString.Trim().Substring(0,10);
                }
                dt.Rows[i]["m_key"] = key;
                ky_ph = dt.Rows[i]["thang"].ToString().Trim() + "/" + dt.Rows[i]["nam"].ToString().Trim();
                ma_kh = dt.Rows[i]["ma_kh"].ToString().Trim();
                so_hd = dt.Rows[i]["So_hd"].ToString().Trim();
                so_doc = dt.Rows[i]["so_doc"].ToString().Trim();
                // lấy tên khách hàng
                ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                dia_chi = dt.Rows[i]["Dia_chi"].ToString().Trim();
                //pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
                ms_thue = dt.Rows[i]["Ma_so_thue"].ToString().Trim();
                chiso_moi = Convert.ToInt32(dt.Rows[i]["cs_kt"]);
                chiso_cu = Convert.ToInt32(dt.Rows[i]["cs_bd"]);
                so_luong = Convert.ToInt32(dt.Rows[i]["t_so_luong"]);
                thanh_tien = Convert.ToInt64(dt.Rows[i]["t_tien"]);
                don_gia = (int)(thanh_tien / so_luong);
                muc_thue_GTGT = Convert.ToInt64(dt.Rows[i]["thue_suat"]);
                thue = Convert.ToInt64(dt.Rows[i]["t_thue"]);
                muc_thue_BVMT = Convert.ToInt64(dt.Rows[i]["Phi_thai"]);
                thue_BVMT = Convert.ToInt64(dt.Rows[i]["t_phi_thai"]);
                tieu_muc = "";
                tong_tien = Convert.ToInt64(dt.Rows[i]["t_tt"]);
                string bang_chu = DocTienBangChu(tong_tien, " đồng");
                
                sb.Append("<Inv><key>");
                sb.Append(key);
                sb.Append("</key><Invoice>");
                sb.Append("<MA_SO_DOC>");
                sb.Append(so_doc);
                sb.Append("</MA_SO_DOC><CusCode>");
                sb.Append(ma_kh);
                sb.Append("</CusCode><CusName>");
                sb.Append(convertSpecialCharacter(ten_kh));
                //sb.Append("</CusName>");
                sb.Append("</CusName><Buyer>");
                //sb.Append(ten_kh);
                sb.Append("</Buyer>");
                sb.Append("<CusAddress>");
                sb.Append(convertSpecialCharacter(dia_chi));
                sb.Append("</CusAddress><CusPhone>");
                sb.Append(dien_thoai);
                sb.Append("</CusPhone><CusTaxCode>");
                sb.Append(ms_thue);
                sb.Append("</CusTaxCode><PaymentMethod>");
                sb.Append(pt_tt);
                sb.Append("</PaymentMethod>");
                sb.Append("<KindOfService>");
                sb.Append(convertSpecialCharacter(m_sodoc));
                sb.Append("</KindOfService><SO_HOP_DONG>");
                sb.Append(so_hd);
                sb.Append("</SO_HOP_DONG>");
                sb.Append("<Sumptions><Sumption><CHI_SO_DAU_KY>");
                sb.Append(chiso_cu);
                sb.Append("</CHI_SO_DAU_KY><CHI_SO_CUOI_KY>");
                sb.Append(chiso_moi);
                sb.Append("</CHI_SO_CUOI_KY><SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</SAN_LUONG>");
                sb.Append("</Sumption></Sumptions>");
                sb.Append("<Products><Product>");
                sb.Append("<ProdName>");
                sb.Append(convertSpecialCharacter(noi_dung));
                sb.Append("</ProdName>");                
                sb.Append("<ProdQuantity>");
                sb.Append(so_luong);
                sb.Append("</ProdQuantity><ProdPrice>");
                sb.Append(don_gia);
                sb.Append("</ProdPrice><Amount>");
                sb.Append(thanh_tien);
                sb.Append("</Amount></Product></Products>");
                sb.Append("<TONG_SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</TONG_SAN_LUONG><Total>");
                sb.Append(thanh_tien);
                sb.Append("</Total><PHI_BVMT>");
                sb.Append(thue_BVMT);
                sb.Append("</PHI_BVMT><VATAmount>");
                sb.Append(thue);
                sb.Append("</VATAmount><VATRate>");
                sb.Append(muc_thue_GTGT);
                sb.Append("</VATRate><Amount>");
                sb.Append(tong_tien);
                sb.Append("</Amount><AmountInWords>");
                sb.Append(bang_chu);
                sb.Append("</AmountInWords>");
                sb.Append("<Extra>");
                sb.Append(muc_thue_BVMT);
                sb.Append("</Extra><ArisingDate>");
                sb.Append(ngay_ph_hd);
                //sb.Append("26/07/2018");
                sb.Append("</ArisingDate>");
                sb.Append("<PaymentStatus>1</PaymentStatus>");
                sb.Append("</Invoice></Inv>");
            }
            sb.Append("</Invoices>");
            //if (m_file != "")
            //{
            //    outfile.Write(sb.ToString());
            //    outfile.Close();
            //    bool exists = Directory.Exists(Connection.p_path+"Temp");
            //    if (exists)
            //        Directory.Delete(Connection.p_path + "Temp",true);
            //    Directory.CreateDirectory(Connection.p_path + "Temp");
            //    File.Copy(Connection.p_path + m_file + ".xml", Connection.p_path + "Temp\\" + m_file + ".xml");
            //    // Tạo ra file zip bằng cách nén một thư mục.
            //    ZipFile.CreateFromDirectory(Connection.p_path + "Temp", Connection.p_path + m_file + ".zip");
            //}
        }
        catch (Exception ex)
        {         
            MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }

    public static string phat_hanh_hoa_don_zip(string m_sodoc, DataTable dt, int m_bd, int m_kt, int iMonth, int iYear,string ngay_ph_hd_zip)
    {
        int n = 0;
        StringBuilder sb = new StringBuilder("");
        sb.Capacity = 32 * 1024 * 1024;
        //if (File.Exists(Connection.p_path + m_file + ".xml"))
        //    File.Delete(Connection.p_path + m_file + ".xml");

        //StreamWriter outfile = new StreamWriter(Connection.p_path + m_file + ".xml", true);

        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            // thêm column để lưu lại key
            if (!IsColumnExist(dt, "m_key"))
            {
                dt.Columns.Add("m_key", typeof(System.String));
            }

            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            sb.Append("<Invoices>");
            sb.AppendLine("<BillTime>" + iYear.ToString() + (iMonth < 10 ? "0" + iMonth.ToString() : iMonth.ToString()) + "</BillTime>");
            n = 0;
            for (int i = m_bd; i <= m_kt; i++)
            {
                //if (m_file != "" && n % 10000 == 0) // tao file
                //{
                //    n++;
                //    outfile.Write(sb.ToString());
                //    sb.Length = 0;
                //}

                ngay_ph = dt.Rows[i]["ngay_ct0"].ToString().Trim().Substring(0, 10);
                string tempString = iYear.ToString().Trim().Substring(2, 2) + dt.Rows[i]["ma_kh"].ToString().Trim() + padstring(iMonth.ToString());
                if (!string.IsNullOrEmpty(tempString))
                {
                    key = tempString.Trim().Substring(0, 10);
                }
                dt.Rows[i]["m_key"] = key;
                ky_ph = dt.Rows[i]["thang"].ToString().Trim() + "/" + dt.Rows[i]["nam"].ToString().Trim();
                ma_kh = dt.Rows[i]["ma_kh"].ToString().Trim();
                so_hd = dt.Rows[i]["So_hd"].ToString().Trim();
                so_doc = dt.Rows[i]["so_doc"].ToString().Trim();
                ten_so_doc = dt.Rows[i]["ten_so_doc"].ToString().Trim();
                // lấy tên khách hàng
                //ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                if (IsCheckNullCusName(dt.Rows[i]["Ten_kh"].ToString().Trim(), ma_kh))
                {
                    break;
                    //checkNull = false;
                }
                else
                {
                    ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                    
                }
                dia_chi = dt.Rows[i]["Dia_chi"].ToString().Trim();
                //pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
                ms_thue = dt.Rows[i]["Ma_so_thue"].ToString().Trim();
                chiso_moi = Convert.ToInt32(dt.Rows[i]["cs_kt"]);
                chiso_cu = Convert.ToInt32(dt.Rows[i]["cs_bd"]);
                so_luong = Convert.ToInt32(dt.Rows[i]["t_so_luong"]);
                thanh_tien = Convert.ToInt64(dt.Rows[i]["t_tien"]);
                don_gia = (int)(thanh_tien / so_luong);
                muc_thue_GTGT = Convert.ToInt64(dt.Rows[i]["thue_suat"]);
                thue = Convert.ToInt64(dt.Rows[i]["t_thue"]);
                muc_thue_BVMT = Convert.ToInt64(dt.Rows[i]["Phi_thai"]);
                thue_BVMT = Convert.ToInt64(dt.Rows[i]["t_phi_thai"]);
                tieu_muc = "";
                tong_tien = Convert.ToInt64(dt.Rows[i]["t_tt"]);
                string bang_chu = DocTienBangChu(tong_tien, " đồng");

                sb.Append("<Inv><key>");
                sb.Append(key);
                sb.Append("</key><Invoice>");
                sb.Append("<MA_SO_DOC>");
                sb.Append(so_doc);
                sb.Append("</MA_SO_DOC><CusCode>");
                sb.Append(ma_kh);
                sb.Append("</CusCode><CusName>");
                sb.Append(convertSpecialCharacter(ten_kh));
                //sb.Append("</CusName>");
                sb.Append("</CusName><Buyer>");
                //sb.Append(ten_kh);
                sb.Append("</Buyer>");
                sb.Append("<CusAddress>");
                sb.Append(convertSpecialCharacter(dia_chi));
                sb.Append("</CusAddress><CusPhone>");
                sb.Append(dien_thoai);
                sb.Append("</CusPhone><CusTaxCode>");
                sb.Append(ms_thue);
                sb.Append("</CusTaxCode><PaymentMethod>");
                sb.Append(pt_tt);
                sb.Append("</PaymentMethod>");
                sb.Append("<KindOfService>");
                sb.Append(convertSpecialCharacter(ten_so_doc));
                sb.Append("</KindOfService><SO_HOP_DONG>");
                sb.Append(so_hd);
                sb.Append("</SO_HOP_DONG>");
                sb.Append("<Sumptions><Sumption><CHI_SO_DAU_KY>");
                sb.Append(chiso_cu);
                sb.Append("</CHI_SO_DAU_KY><CHI_SO_CUOI_KY>");
                sb.Append(chiso_moi);
                sb.Append("</CHI_SO_CUOI_KY><SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</SAN_LUONG>");
                sb.Append("</Sumption></Sumptions>");
                sb.Append("<Products><Product>");
                sb.Append("<ProdName>");
                sb.Append(convertSpecialCharacter(noi_dung));
                sb.Append("</ProdName>");
                sb.Append("<ProdQuantity>");
                sb.Append(so_luong);
                sb.Append("</ProdQuantity><ProdPrice>");
                sb.Append(don_gia);
                sb.Append("</ProdPrice><Amount>");
                sb.Append(thanh_tien);
                sb.Append("</Amount></Product></Products>");
                sb.Append("<TONG_SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</TONG_SAN_LUONG><Total>");
                sb.Append(thanh_tien);
                sb.Append("</Total><PHI_BVMT>");
                sb.Append(thue_BVMT);
                sb.Append("</PHI_BVMT><VATAmount>");
                sb.Append(thue);
                sb.Append("</VATAmount><VATRate>");
                sb.Append(muc_thue_GTGT);
                sb.Append("</VATRate><Amount>");
                sb.Append(tong_tien);
                sb.Append("</Amount><AmountInWords>");
                sb.Append(bang_chu);
                sb.Append("</AmountInWords>");
                sb.Append("<Extra>");
                sb.Append(muc_thue_BVMT);
                sb.Append("</Extra><ArisingDate>");
                sb.Append(ngay_ph_hd_zip);
                //sb.Append("26/07/2018");
                sb.Append("</ArisingDate>");
                sb.Append("<PaymentStatus>1</PaymentStatus>");
                sb.AppendLine("</Invoice></Inv>");
            }
            sb.Append("</Invoices>");            
            //if (m_file != "")
            //{
            //    outfile.Write(sb.ToString());
            //    outfile.Close();
            //    bool exists = Directory.Exists(Connection.p_path + "Temp");
            //    if (exists)
            //        Directory.Delete(Connection.p_path + "Temp", true);
            //    Directory.CreateDirectory(Connection.p_path + "Temp");
            //    File.Copy(Connection.p_path + m_file + ".xml", Connection.p_path + "Temp\\" + m_file + ".xml");
            //    // Tạo ra file zip bằng cách nén một thư mục.
            //    if (File.Exists(Connection.p_path + m_file + ".zip"))
            //    {
            //        File.Delete(Connection.p_path + m_file + ".zip");
            //    }
            //    ZipFile.CreateFromDirectory(Connection.p_path + "Temp", Connection.p_path + m_file + ".zip");
            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }
    private static string convertSpecialCharacter(string xmlData)
    {
        return "<![CDATA[" + xmlData + "]]>";
    }

    public static string phat_hanh_hoa_don_thay_the(string m_sodoc, DataTable dt, int m_bd, int m_kt, int iMonth, int iYear, string ngay_ph_hd_thaythe)
    {
        StringBuilder sb = new StringBuilder("");
        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            // thêm column để lưu lại key
            if (!IsColumnExist(dt, "m_key"))
            {
                dt.Columns.Add("m_key", typeof(System.String));
            }

            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            sb.Append("<ReplaceInv>");
            for (int i = m_bd; i <= m_kt; i++)
            {
                ngay_ph = dt.Rows[i]["ngay_ct0"].ToString().Trim().Substring(0, 10);
                string tempString = iYear.ToString().Trim().Substring(2, 2) + dt.Rows[i]["ma_kh"].ToString().Trim() + padstring(iMonth.ToString());
                if (!string.IsNullOrEmpty(tempString))
                {
                    key = tempString.Trim().Substring(0, 10);
                }
                int m_lanR = Convert.ToInt16(dt.Rows[i]["m_lan"]);
                if (m_lanR >= 1)
                {
                    key = key + "$" + m_lanR.ToString().Trim();
                }                
                dt.Rows[i]["m_key"] = key;
                ky_ph = dt.Rows[i]["thang"].ToString().Trim() + "/" + dt.Rows[i]["nam"].ToString().Trim();
                ma_kh = dt.Rows[i]["ma_kh"].ToString().Trim();
                so_hd = dt.Rows[i]["So_hd"].ToString().Trim();
                so_doc = dt.Rows[i]["so_doc"].ToString().Trim();
                // lấy tên khách hàng
                ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                dia_chi = dt.Rows[i]["Dia_chi"].ToString().Trim();
                //pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
                ms_thue = dt.Rows[i]["Ma_so_thue"].ToString().Trim();
                chiso_moi = Convert.ToInt32(dt.Rows[i]["cs_kt"]);
                chiso_cu = Convert.ToInt32(dt.Rows[i]["cs_bd"]);
                so_luong = Convert.ToInt32(dt.Rows[i]["t_so_luong"]);
                thanh_tien = Convert.ToInt64(dt.Rows[i]["t_tien"]);
                don_gia = (int)(thanh_tien / so_luong);
                muc_thue_GTGT = Convert.ToInt64(dt.Rows[i]["thue_suat"]);
                thue = Convert.ToInt64(dt.Rows[i]["t_thue"]);
                muc_thue_BVMT = Convert.ToInt64(dt.Rows[i]["Phi_thai"]);
                thue_BVMT = Convert.ToInt64(dt.Rows[i]["t_phi_thai"]);
                tieu_muc = "";
                tong_tien = Convert.ToInt64(dt.Rows[i]["t_tt"]);
                string bang_chu = DocTienBangChu(tong_tien, " đồng");

                sb.Append("<key>");
                sb.Append(key);
                sb.Append("</key>");
                sb.Append("<ArisingDate>");
                sb.Append(ngay_ph_hd_thaythe);
                sb.Append("</ArisingDate>");
                sb.Append("<MA_SO_DOC>");
                sb.Append(so_doc);
                sb.Append("</MA_SO_DOC><CusName>");
                sb.Append(convertSpecialCharacter(ten_kh));
                sb.Append("</CusName><CusAddress>");
                sb.Append(convertSpecialCharacter(dia_chi));
                //sb.Append("</CusName>");
                sb.Append("</CusAddress>");
                //sb.Append(ten_kh);

                sb.Append("<CusTaxCode>");
                sb.Append(ms_thue);
                sb.Append("</CusTaxCode><SO_HOP_DONG>");
                sb.Append(so_hd);
                sb.Append("</SO_HOP_DONG><CusBankNo>");
                sb.Append("</CusBankNo><CusCode>");
                sb.Append(ma_kh);
                sb.Append("</CusCode>");
                sb.Append("<CusPhone>");
                sb.Append("</CusPhone>");
                sb.Append("<PaymentMethod>");
                sb.Append(pt_tt);
                sb.Append("</PaymentMethod>");
                sb.Append("<KindOfService>");
                sb.Append(convertSpecialCharacter(m_sodoc));
                sb.Append("</KindOfService>");
                sb.Append("<Sumptions><Sumption><CHI_SO_DAU_KY>");
                sb.Append(chiso_cu);
                sb.Append("</CHI_SO_DAU_KY><CHI_SO_CUOI_KY>");
                sb.Append(chiso_moi);
                sb.Append("</CHI_SO_CUOI_KY><SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</SAN_LUONG>");
                sb.Append("</Sumption></Sumptions>");
                sb.Append("<Products><Product>");
                sb.Append("<ProdName>");
                sb.Append(convertSpecialCharacter(noi_dung));
                sb.Append("</ProdName>");
                sb.Append("<ProdQuantity>");
                sb.Append(so_luong);
                sb.Append("</ProdQuantity><ProdPrice>");
                sb.Append(don_gia);
                sb.Append("</ProdPrice><Amount>");
                sb.Append(thanh_tien);
                sb.Append("</Amount></Product></Products>");
                sb.Append("<TONG_SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</TONG_SAN_LUONG><Total>");
                sb.Append(thanh_tien);
                sb.Append("</Total><PHI_BVMT>");
                sb.Append(thue_BVMT);
                sb.Append("</PHI_BVMT><VATAmount>");
                sb.Append(thue);
                sb.Append("</VATAmount><VATRate>");
                sb.Append(muc_thue_GTGT);
                sb.Append("</VATRate><Amount>");
                sb.Append(tong_tien);
                sb.Append("</Amount><AmountInWords>");
                sb.Append(bang_chu);
                sb.Append("</AmountInWords>");
                sb.Append("<Extra>");
                sb.Append(muc_thue_BVMT);
                sb.Append("</Extra>");
            }
            sb.Append("</ReplaceInv>");
        }
        catch (Exception ex)
        {          
            MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }

    public static string phat_hanh_hoa_don_huy(string m_sodoc, DataTable dt, int m_bd, int m_kt, int iMonth, int iYear, string ngay_ph_hd_huy)
    {
        StringBuilder sb = new StringBuilder("");
        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            // thêm column để lưu lại key
            if (!IsColumnExist(dt, "m_key"))
            {
                dt.Columns.Add("m_key", typeof(System.String));
            }

            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            sb.Append("<Invoices>");
            for (int i = m_bd; i <= m_kt; i++)
            {
                ngay_ph = dt.Rows[i]["ngay_ct0"].ToString().Trim().Substring(0, 10);
                string tempString = iYear.ToString().Trim().Substring(2, 2) + dt.Rows[i]["ma_kh"].ToString().Trim() + padstring(iMonth.ToString());
                if (!string.IsNullOrEmpty(tempString))
                {
                    key = tempString.Trim().Substring(0, 10);
                }
                int m_lanR = Convert.ToInt16(dt.Rows[i]["m_lan"]);
                if (m_lanR >= 1)
                {
                    key = key + m_lanR.ToString().Trim();
                }
                dt.Rows[i]["m_key"] = key;
                ky_ph = dt.Rows[i]["thang"].ToString().Trim() + "/" + dt.Rows[i]["nam"].ToString().Trim();
                ma_kh = dt.Rows[i]["ma_kh"].ToString().Trim();
                so_hd = dt.Rows[i]["So_hd"].ToString().Trim();
                so_doc = dt.Rows[i]["so_doc"].ToString().Trim();
                // lấy tên khách hàng
                ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                dia_chi = dt.Rows[i]["Dia_chi"].ToString().Trim();
                //pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
                ms_thue = dt.Rows[i]["Ma_so_thue"].ToString().Trim();
                chiso_moi = Convert.ToInt32(dt.Rows[i]["cs_kt"]);
                chiso_cu = Convert.ToInt32(dt.Rows[i]["cs_bd"]);
                so_luong = Convert.ToInt32(dt.Rows[i]["t_so_luong"]);
                thanh_tien = Convert.ToInt64(dt.Rows[i]["t_tien"]);
                don_gia = (int)(thanh_tien / so_luong);
                muc_thue_GTGT = Convert.ToInt64(dt.Rows[i]["thue_suat"]);
                thue = Convert.ToInt64(dt.Rows[i]["t_thue"]);
                muc_thue_BVMT = Convert.ToInt64(dt.Rows[i]["Phi_thai"]);
                thue_BVMT = Convert.ToInt64(dt.Rows[i]["t_phi_thai"]);
                tieu_muc = "";
                tong_tien = Convert.ToInt64(dt.Rows[i]["t_tt"]);
                string bang_chu = DocTienBangChu(tong_tien, " đồng");

                sb.Append("<Inv><key>");
                sb.Append(key);
                sb.Append("</key><Invoice>");
                sb.Append("<ArisingDate>");
                sb.Append(ngay_ph_hd_huy);
                sb.Append("</ArisingDate>");
                sb.Append("<MA_SO_DOC>");
                sb.Append(so_doc);
                sb.Append("</MA_SO_DOC><CusName>");
                sb.Append(convertSpecialCharacter(ten_kh));
                sb.Append("</CusName><CusAddress>");
                sb.Append(convertSpecialCharacter(dia_chi));
                //sb.Append("</CusName>");
                sb.Append("</CusAddress>");
                //sb.Append(ten_kh);
                sb.Append("<CusTaxCode>");
                sb.Append(ms_thue);
                sb.Append("</CusTaxCode><SO_HOP_DONG>");
                sb.Append(so_hd);
                sb.Append("</SO_HOP_DONG><CusBankNo>");
                sb.Append("</CusBankNo><CusCode>");
                sb.Append(ma_kh);
                sb.Append("</CusCode>");
                sb.Append("<CusPhone>");
                sb.Append("</CusPhone>");
                sb.Append("<PaymentMethod>");
                sb.Append(pt_tt);
                sb.Append("</PaymentMethod>");
                sb.Append("<KindOfService>");
                sb.Append(convertSpecialCharacter(m_sodoc));
                sb.Append("</KindOfService>");
                sb.Append("<Sumptions><Sumption><CHI_SO_DAU_KY>");
                sb.Append(chiso_cu);
                sb.Append("</CHI_SO_DAU_KY><CHI_SO_CUOI_KY>");
                sb.Append(chiso_moi);
                sb.Append("</CHI_SO_CUOI_KY><SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</SAN_LUONG>");
                sb.Append("</Sumption></Sumptions>");
                sb.Append("<Products><Product>");
                sb.Append("<ProdName>");
                sb.Append(convertSpecialCharacter(noi_dung));
                sb.Append("</ProdName>");
                sb.Append("<ProdQuantity>");
                sb.Append(so_luong);
                sb.Append("</ProdQuantity><ProdPrice>");
                sb.Append(don_gia);
                sb.Append("</ProdPrice><Amount>");
                sb.Append(thanh_tien);
                sb.Append("</Amount></Product></Products>");
                sb.Append("<TONG_SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</TONG_SAN_LUONG><Total>");
                sb.Append(thanh_tien);
                sb.Append("</Total><PHI_BVMT>");
                sb.Append(thue_BVMT);
                sb.Append("</PHI_BVMT><VATAmount>");
                sb.Append(thue);
                sb.Append("</VATAmount><VATRate>");
                sb.Append(muc_thue_GTGT);
                sb.Append("</VATRate><Amount>");
                sb.Append(tong_tien);
                sb.Append("</Amount><AmountInWords>");
                sb.Append(bang_chu);
                sb.Append("</AmountInWords>");
                sb.Append("<Extra>");
                sb.Append(muc_thue_BVMT);
                sb.Append("</Extra>");
                sb.Append("<PaymentStatus>1</PaymentStatus>");
                sb.Append("</Invoice></Inv>");
            }
            sb.Append("</Invoices>");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }

    public static string phat_hanh_hoa_don_dieu_chinh(string m_sodoc, DataTable dt, int m_bd, int m_kt, int iMonth, int iYear, string ngay_ph_hd_thaythe, int type)
    {
        StringBuilder sb = new StringBuilder("");
        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            // thêm column để lưu lại key
            if (!IsColumnExist(dt, "m_key"))
            {
                dt.Columns.Add("m_key", typeof(System.String));
            }

            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            sb.Append("<AdjustInv>");
            for (int i = m_bd; i <= m_kt; i++)
            {
                ngay_ph = dt.Rows[i]["ngay_ct0"].ToString().Trim().Substring(0, 10);
                string tempString = iYear.ToString().Trim().Substring(2, 2) + dt.Rows[i]["ma_kh"].ToString().Trim() + padstring(iMonth.ToString());
                if (!string.IsNullOrEmpty(tempString))
                {
                    key = tempString.Trim().Substring(0, 10);
                }
                int m_lanR = Convert.ToInt16(dt.Rows[i]["m_lan"]);
                if (m_lanR >= 1)
                {
                    key = key + "$" + m_lanR.ToString().Trim();
                }
                dt.Rows[i]["m_key"] = key;
                ky_ph = dt.Rows[i]["thang"].ToString().Trim() + "/" + dt.Rows[i]["nam"].ToString().Trim();
                ma_kh = dt.Rows[i]["ma_kh"].ToString().Trim();
                so_hd = dt.Rows[i]["So_hd"].ToString().Trim();
                so_doc = dt.Rows[i]["so_doc"].ToString().Trim();
                // lấy tên khách hàng
                ten_kh = dt.Rows[i]["Ten_kh"].ToString().Trim();
                dia_chi = dt.Rows[i]["Dia_chi"].ToString().Trim();
                //pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
                ms_thue = dt.Rows[i]["Ma_so_thue"].ToString().Trim();
                chiso_moi = Convert.ToInt32(dt.Rows[i]["cs_kt"]);
                chiso_cu = Convert.ToInt32(dt.Rows[i]["cs_bd"]);
                so_luong = Convert.ToInt32(dt.Rows[i]["t_so_luong"]);
                thanh_tien = Convert.ToInt64(dt.Rows[i]["t_tien"]);
                don_gia = (int)(thanh_tien / so_luong);
                muc_thue_GTGT = Convert.ToInt64(dt.Rows[i]["thue_suat"]);
                thue = Convert.ToInt64(dt.Rows[i]["t_thue"]);
                muc_thue_BVMT = Convert.ToInt64(dt.Rows[i]["Phi_thai"]);
                thue_BVMT = Convert.ToInt64(dt.Rows[i]["t_phi_thai"]);
                tieu_muc = "";
                tong_tien = Convert.ToInt64(dt.Rows[i]["t_tt"]);
                string bang_chu = DocTienBangChu(tong_tien, " đồng");

                sb.Append("<key>");
                sb.Append(key);
                sb.Append("</key>");
                sb.Append("<ArisingDate>");
                sb.Append(ngay_ph_hd_thaythe);
                sb.Append("</ArisingDate>");
                sb.Append("<MA_SO_DOC>");
                sb.Append(so_doc);
                sb.Append("</MA_SO_DOC><CusName>");
                sb.Append(convertSpecialCharacter(ten_kh));
                sb.Append("</CusName><CusAddress>");
                sb.Append(convertSpecialCharacter(dia_chi));
                //sb.Append("</CusName>");
                sb.Append("</CusAddress>");
                //sb.Append(ten_kh);

                sb.Append("<CusTaxCode>");
                sb.Append(ms_thue);
                sb.Append("</CusTaxCode><SO_HOP_DONG>");
                sb.Append(so_hd);
                sb.Append("</SO_HOP_DONG><CusBankNo>");
                sb.Append("</CusBankNo><CusCode>");
                sb.Append(ma_kh);
                sb.Append("</CusCode>");
                sb.Append("<CusPhone>");
                sb.Append("</CusPhone>");
                sb.Append("<PaymentMethod>");
                sb.Append(pt_tt);
                sb.Append("</PaymentMethod>");
                sb.Append("<Type>");
                sb.Append(type);
                sb.Append("</Type>");
                sb.Append("<KindOfService>");
                sb.Append(convertSpecialCharacter(m_sodoc));
                sb.Append("</KindOfService>");
                sb.Append("<Sumptions><Sumption><CHI_SO_DAU_KY>");
                sb.Append(chiso_cu);
                sb.Append("</CHI_SO_DAU_KY><CHI_SO_CUOI_KY>");
                sb.Append(chiso_moi);
                sb.Append("</CHI_SO_CUOI_KY><SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</SAN_LUONG>");
                sb.Append("</Sumption></Sumptions>");
                sb.Append("<Products><Product>");
                sb.Append("<ProdName>");
                sb.Append(convertSpecialCharacter(noi_dung));
                sb.Append("</ProdName>");
                sb.Append("<ProdQuantity>");
                sb.Append(so_luong);
                sb.Append("</ProdQuantity><ProdPrice>");
                sb.Append(don_gia);
                sb.Append("</ProdPrice><Amount>");
                sb.Append(thanh_tien);
                sb.Append("</Amount></Product></Products>");
                sb.Append("<TONG_SAN_LUONG>");
                sb.Append(so_luong);
                sb.Append("</TONG_SAN_LUONG><Total>");
                sb.Append(thanh_tien);
                sb.Append("</Total><PHI_BVMT>");
                sb.Append(thue_BVMT);
                sb.Append("</PHI_BVMT><VATAmount>");
                sb.Append(thue);
                sb.Append("</VATAmount><VATRate>");
                sb.Append(muc_thue_GTGT);
                sb.Append("</VATRate><Amount>");
                sb.Append(tong_tien);
                sb.Append("</Amount><AmountInWords>");
                sb.Append(bang_chu);
                sb.Append("</AmountInWords>");
                sb.Append("<Extra>");
                sb.Append(muc_thue_BVMT);
                sb.Append("</Extra>");
            }
            sb.Append("</AdjustInv>");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }
    public static string padstring(string stri)
    {
        string MyString = "";
        MyString = "00" + stri;
        MyString = MyString.Substring(MyString.Length - 2, 2);
        return MyString;
    }


    public static string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
    public static string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
    // Hàm đọc số thành chữ
    public static string DocTienBangChu(long SoTien, string strTail)
    {
        int lan, i;
        long so;
        string KetQua = "", tmp = "";
        int[] ViTri = new int[6];
        if (SoTien < 0) return "Số tiền âm !";
        if (SoTien == 0) return "Không đồng !";
        if (SoTien > 0)
        {
            so = SoTien;
        }
        else
        {
            so = -SoTien;
        }
        //Kiểm tra số quá lớn
        if (SoTien > 8999999999999999)
        {
            SoTien = 0;
            return "";
        }
        ViTri[5] = (int)(so / 1000000000000000);
        so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
        ViTri[4] = (int)(so / 1000000000000);
        so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
        ViTri[3] = (int)(so / 1000000000);
        so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
        ViTri[2] = (int)(so / 1000000);
        ViTri[1] = (int)((so % 1000000) / 1000);
        ViTri[0] = (int)(so % 1000);
        if (ViTri[5] > 0)
        {
            lan = 5;
        }
        else if (ViTri[4] > 0)
        {
            lan = 4;
        }
        else if (ViTri[3] > 0)
        {
            lan = 3;
        }
        else if (ViTri[2] > 0)
        {
            lan = 2;
        }
        else if (ViTri[1] > 0)
        {
            lan = 1;
        }
        else
        {
            lan = 0;
        }
        for (i = lan; i >= 0; i--)
        {
            tmp = DocSo3ChuSo(ViTri[i]);
            KetQua += tmp;
            if (ViTri[i] != 0) KetQua += Tien[i];
            if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
        }
        if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
        KetQua = KetQua.Trim() + strTail;
        return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
    }

    // Hàm đọc số có 3 chữ số
    private static string DocSo3ChuSo(int baso)
    {
        int tram, chuc, donvi;
        string KetQua = "";
        tram = (int)(baso / 100);
        chuc = (int)((baso % 100) / 10);
        donvi = baso % 10;
        if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
        if (tram != 0)
        {
            KetQua += ChuSo[tram] + " trăm";
            if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
        }
        if ((chuc != 0) && (chuc != 1))
        {
            KetQua += ChuSo[chuc] + " mươi";
            if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
        }
        if (chuc == 1) KetQua += " mười";
        switch (donvi)
        {
            case 1:
                if ((chuc != 0) && (chuc != 1))
                {
                    KetQua += " mốt";
                }
                else
                {
                    KetQua += ChuSo[donvi];
                }
                break;
            case 5:
                if (chuc == 0)
                {
                    KetQua += ChuSo[donvi];
                }
                else
                {
                    KetQua += " lăm";
                }
                break;
            default:
                if (donvi != 0)
                {
                    KetQua += ChuSo[donvi];
                }
                break;
        }
        return KetQua;
    }
    //ham kiem tra xem column co ton tai hay k
    private static bool IsColumnExist(DataTable tableNameToCheck, string columnName)
    {
        bool iscolumnExist = true;
        try
        {
            if (null != tableNameToCheck && tableNameToCheck.Columns != null)
            {
                if (!tableNameToCheck.Columns.Contains(columnName))
                    iscolumnExist = false;
            }
            else
            {
                iscolumnExist = false;
            }
        }
        catch (Exception ex)
        {

        }
        return iscolumnExist;
    }
    /// <summary>
    /// Check thẻ CusName có null hay không
    /// </summary>
    /// <param name="Cusname"> tham số là CusName</param>
    /// <returns></returns>
    private static bool IsCheckNullCusName(string Cusname, string MaKH)
    {
        if (Cusname=="" || Cusname==null)
        {
            checkNull = true;
            MessageBox.Show("Khách hàng có mã "+ MaKH.ToString()+ " không có tên", "Lổi", MessageBoxButtons.OK,MessageBoxIcon.Error);
            return checkNull;   
            //có null =true
        }
        else
        {
            checkNull = false;
            return checkNull;
            //không null
        }
        
    }
}
