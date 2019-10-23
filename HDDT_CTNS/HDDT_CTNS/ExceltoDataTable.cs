
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class ExceltoDataTable
{
 
    public static string strc = "1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21";
    public static int index_ct_bd, index_ct_kt;
    public static int max_index_tieude = 10;
    public static int max_column = 25;
    public static string[] listHD = new string[] { "ma_kh", "ngay_ph", "ten_kh","ten_ct", "dia_chi", "dien_thoai", "ms_thue", "pt_tt", "noi_dung", "tieu_muc", "tong_nop", "tong_tien", "bang_chu" };

    public static string key;
    public static string ngay_ph;
    public static string ngay_ph_nguoc;
    public static string ma_kh;
    public static string ten_kh;       
    public static string ten_ct;
    public static string dia_chi;
    public static string dien_thoai;
    public static string pt_tt;
    public static string ms_thue;
    public static string tieu_muc;
    public static decimal tong_tien;   
public static string ConvertExcelToDatatable(string m_path)
    {
        DataTable dt = new DataTable();
        DataTable dthd = new DataTable();
        StringBuilder sb = new StringBuilder("");
        //StringBuilder sb = new StringBuilder("<Invoices>");
        try
        {
            dthd.Columns.Clear();
            // Lấy stream file
            FileStream fs = new FileStream(@m_path, FileMode.Open);

            // Khởi tạo workbook để đọc
            HSSFWorkbook wb = new HSSFWorkbook(fs);

            // Lấy sheet đầu tiên
            ISheet sheet = wb.GetSheetAt(0);

            // tao header của table rỗng gồm 25 column để đọc dữ liệu file hóa đơn vào

            for (int i = 0; i < max_column; i++)
            {
                DataRow dr = dt.NewRow();
                String val = "Column" + i.ToString();
                dt.Columns.Add(val.Trim().Replace(" ", "_"));
            }

            //// tao header của table rỗng gồm cac column để chứa dữ liệu chi tiết file hóa đơn
            for (int i = 0; i < listHD.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                String val = listHD[i].ToString();
                dthd.Columns.Add(val.Trim().Replace(" ", "_"));
            }

            //int rowIndex = 0;
            foreach (IRow row in sheet)
            {
                // skip header row
                //if (rowIndex++ == 0) continue;
                DataRow dataRow = dt.NewRow();
                dataRow.ItemArray = row.Cells.Select(c => c.ToString()).ToArray();
                dt.Rows.Add(dataRow);
            }
            // xac dinh doan chi tiet cua hoa don
            int dem = 0;
            for (int i = max_index_tieude; i < dt.Rows.Count - 2; i++)
            {
                if (strc.Contains(dt.Rows[i]["Column0"].ToString().Trim()) && dt.Rows[i]["Column0"].ToString().Trim() != "")
                {
                    dem++;
                    dt.Rows[i]["Column0"] = "";
                    index_ct_kt = i + 1;
                    index_ct_bd = index_ct_kt - dem;
                }
            }


            // lấy header hóa đơn
            // lấy ngày phát hành
            //decimal tong_nop =
            Random rd = new Random();
            
            ngay_ph = lay_thong_tin_ngay_thang(dt, 1,0, dt.Columns.Count); // dòng 2 trong file excel chứa thông tin ngày phát hành           
            ngay_ph_nguoc = lay_thong_tin_dong_nguoc(dt, 1);
            string tempString = ngay_ph_nguoc + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + rd.Next(1, 10000).ToString();
            if (!string.IsNullOrEmpty(tempString))
            {
                key = tempString.Trim();
            }
            ma_kh = key;          
                                                              // lấy tên khách hàng
            ten_kh = lay_thong_tin_dong_xuoi(dt, 4, 0, dt.Columns.Count); // dòng 4 trong file excel chứa thông tin ten khách hàng           
            ten_kh = ten_kh+ "      "+ lay_thong_tin_dong_xuoi(dt, 5, 0, dt.Columns.Count);
            ten_ct = lay_thong_tin_dong_xuoi(dt, 6, 0, dt.Columns.Count);
            dia_chi = lay_thong_tin_dong_xuoi(dt, 7, 0, dt.Columns.Count);
            dien_thoai = "";
            pt_tt = lay_thong_tin_dong_xuoi(dt, 8, 0, dt.Columns.Count);
            ms_thue = lay_thong_tin_dong_xuoi(dt, 9, 0, dt.Columns.Count);            
            tieu_muc = "";
            tong_tien = Convert.ToDecimal(lay_thong_tin_dong_xuoi(dt, index_ct_kt + 2, 0, dt.Columns.Count));
            string bang_chu = lay_thong_tin_dong_xuoi(dt, index_ct_kt + 2 + 1, 0, dt.Columns.Count);
           
            sb.Append("<Invoices><Inv><key>");
            sb.Append(key);
            sb.Append("</key><Invoice><CusCode>");
            sb.Append(ma_kh);
            sb.Append("</CusCode><CusName>");
            sb.Append(ten_ct);
            sb.Append("</CusName><Buyer>");
            sb.Append(ten_kh);
            sb.Append("</Buyer><CusAddress>");
            sb.Append(dia_chi);
            sb.Append("</CusAddress><CusPhone>");
            sb.Append(dien_thoai);
            sb.Append("</CusPhone><CusTaxCode>");
            sb.Append(ms_thue);
            sb.Append("</CusTaxCode><PaymentMethod>");
            sb.Append(pt_tt);
            sb.Append("</PaymentMethod>");
            sb.Append("<KindOfService></KindOfService>");
            sb.Append("<Products>");
            for (int i = index_ct_bd; i < index_ct_kt; i++)
            {
                sb.Append("<Product>");
                string noi_dung = lay_thong_tin_dong_xuoi(dt, i, 0, 10);
                decimal tong_nop = Convert.ToDecimal(lay_thong_tin_dong_xuoi(dt, i, 11, dt.Columns.Count));
                DataRow dr = dthd.NewRow();
                dr["ma_kh"] = ma_kh;
                dr["ngay_ph"] = ngay_ph;
                dr["ten_kh"] = ten_kh;
                dr["ten_ct"] = ten_ct;
                dr["dia_chi"] = dia_chi;
                dr["dien_thoai"] = dien_thoai;
                dr["ms_thue"] = ms_thue;
                dr["pt_tt"] = pt_tt;
                dr["noi_dung"] = noi_dung;
                dr["tieu_muc"] = tieu_muc;
                dr["tong_nop"] = tong_nop;
                dr["tong_tien"] = tong_tien;
                dr["bang_chu"] = bang_chu;
                dthd.Rows.Add(dr);
                sb.Append("<ProdName>");
                sb.Append(noi_dung);
                sb.Append("</ProdName>");
                sb.Append("<ProdUnit>");
                sb.Append(tieu_muc);
                sb.Append("</ProdUnit>");
                sb.Append("<Amount>");
                sb.Append(tong_nop);
                sb.Append("</Amount>");
                sb.Append("</Product>");
            }
            sb.Append("</Products>");
            sb.Append("<Total>");
            sb.Append(tong_tien);
            sb.Append("</Total>");
            sb.Append("<Amount>");
            sb.Append(tong_tien);
            sb.Append("</Amount>");
            sb.Append("<AmountInWords>");
            sb.Append(bang_chu);
            sb.Append("</AmountInWords>");
            sb.Append("<ArisingDate>");
            sb.Append(ngay_ph);
            sb.Append("</ArisingDate><PaymentStatus>1</PaymentStatus>");
            sb.Append("</Invoice></Inv>");
            sb.Append("</Invoices>");
        }
        catch (Exception ex)
        {
           // MessageBox.Show(ex.Message);
        }
        return sb.ToString();
    }
    bool isNullDataTable(DataTable tbl, int i)
    {
        bool m_return = true;
        try
        {
            for (int j = 0; j < tbl.Columns.Count; j++)
            {
                string t = tbl.Rows[i]["Column" + j.ToString()].ToString().Trim();
                if (tbl.Rows[i]["Column" + j.ToString()].ToString().Trim() != "")
                {
                    m_return = false;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return m_return;
    }

    public static string lay_thong_tin_dong_nguoc(DataTable dt, int i)
    {
        string m_return = "";
        try
        {
            for (int j = dt.Columns.Count - 1; j >= 0; j--)
            {
                if (dt.Rows[i]["Column" + j.ToString()].ToString().Trim() != "")
                    m_return = m_return + dt.Rows[i]["Column" + j.ToString()].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return m_return;
    }

    public static string lay_thong_tin_dong_xuoi(DataTable dt, int i, int m_bd, int m_kt) // cong thong tin tu vi tri bat dau den vi tri m_kt ở dòng thứ i
    {
        string m_return = "";
        try
        {
            for (int j = m_bd; j < m_kt; j++)
            {
                if (dt.Rows[i]["Column" + j.ToString()].ToString().Trim() != "")
                    m_return = m_return + dt.Rows[i]["Column" + j.ToString()].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return m_return;
    }

    public static string lay_thong_tin_ngay_thang(DataTable dt, int i, int m_bd, int m_kt)
    {
        string m_return = "";
        try
        {
            for (int j = m_bd; j < m_kt; j++)
            {
                if (dt.Rows[i]["Column" + j.ToString()].ToString().Trim() != "")
                    m_return = m_return+"/" + dt.Rows[i]["Column" + j.ToString()].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return m_return.Substring(1,m_return.Length-1);
    }

    public static bool IsInteger(string str)
    {
        Regex regex = new Regex(@"^[0-9]+$");
        try
        {
            str = str.Replace(',', '9');
            if (String.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (!regex.IsMatch(str))
            {
                return false;
            }

            return true;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;

    }

    //public class Hoadon
    //{
    //    public string ma_kh;
    //    public string ngay_ph;
    //    public string ten_kh;
    //    public string dia_chi;
    //    public string dien_thoai;
    //    public string ms_thue;
    //    public string pt_tt;
    //    public string noi_dung;
    //    public string tieu_muc;
    //    public decimal tong_nop;
    //    public decimal tong_tien;
    //    public string bang_chu;

    //}
}
//}
