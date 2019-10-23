using System.Drawing;
using DevExpress.XtraReports.UI;
namespace HDDT_CTNS
{
    partial class XtraReportBNN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtongtien = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtienphimt = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtienthue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbbangchu = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtien = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Cellkl = new DevExpress.XtraReports.UI.XRTableCell();
            this.Celldongia = new DevExpress.XtraReports.UI.XRTableCell();
            this.Cellthanhtien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtieuthu = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.LbCSM = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.LbCSC = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.LbMKH = new DevExpress.XtraReports.UI.XRLabel();
            this.LbMST = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbdiachi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtenkh = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbtram = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbngay = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.dataSet11 = new HDDT_CTNS.DataSet1();
            this.str_InvoiceTableAdapter1 = new HDDT_CTNS.DataSet1TableAdapters.str_InvoiceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel26,
            this.Lbtongtien,
            this.Lbtienphimt,
            this.Lbtienthue,
            this.xrLabel20,
            this.xrLabel18,
            this.xrLabel13,
            this.Lbbangchu,
            this.Lbtien,
            this.xrLabel25,
            this.xrTable2,
            this.xrLabel35,
            this.xrLabel33,
            this.xrLabel34,
            this.xrLabel32,
            this.xrLabel31,
            this.Lbtieuthu,
            this.xrLabel17,
            this.LbCSM,
            this.xrLabel19,
            this.LbCSC,
            this.xrLabel21,
            this.xrLabel14,
            this.LbMKH,
            this.LbMST,
            this.xrLabel12,
            this.Lbdiachi,
            this.xrLabel11,
            this.Lbtenkh,
            this.xrLabel10,
            this.Lbtram,
            this.Lbngay});
            this.Detail.HeightF = 458.9171F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.Phi_Thai]")});
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(139.021F, 351F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(22.12488F, 16.74997F);
            this.xrLabel26.StylePriority.UseBorders = false;
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel26.TextFormatString = "{0:n0}";
            // 
            // Lbtongtien
            // 
            this.Lbtongtien.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Lbtongtien.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_tt]")});
            this.Lbtongtien.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtongtien.LocationFloat = new DevExpress.Utils.PointFloat(165.3543F, 367.75F);
            this.Lbtongtien.Name = "Lbtongtien";
            this.Lbtongtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtongtien.SizeF = new System.Drawing.SizeF(104.4374F, 16.74997F);
            this.Lbtongtien.StylePriority.UseBorders = false;
            this.Lbtongtien.StylePriority.UseFont = false;
            this.Lbtongtien.StylePriority.UseTextAlignment = false;
            this.Lbtongtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Lbtongtien.TextFormatString = "{0:#,#}";
            // 
            // Lbtienphimt
            // 
            this.Lbtienphimt.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Lbtienphimt.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_phi_thai]")});
            this.Lbtienphimt.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtienphimt.LocationFloat = new DevExpress.Utils.PointFloat(176.8127F, 351.0001F);
            this.Lbtienphimt.Name = "Lbtienphimt";
            this.Lbtienphimt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtienphimt.SizeF = new System.Drawing.SizeF(92.97903F, 16.74997F);
            this.Lbtienphimt.StylePriority.UseBorders = false;
            this.Lbtienphimt.StylePriority.UseFont = false;
            this.Lbtienphimt.StylePriority.UseTextAlignment = false;
            this.Lbtienphimt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Lbtienphimt.TextFormatString = "{0:#,#}";
            // 
            // Lbtienthue
            // 
            this.Lbtienthue.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Lbtienthue.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_thue]")});
            this.Lbtienthue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtienthue.LocationFloat = new DevExpress.Utils.PointFloat(176.8127F, 334.2501F);
            this.Lbtienthue.Name = "Lbtienthue";
            this.Lbtienthue.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtienthue.SizeF = new System.Drawing.SizeF(92.97897F, 16.74997F);
            this.Lbtienthue.StylePriority.UseBorders = false;
            this.Lbtienthue.StylePriority.UseFont = false;
            this.Lbtienthue.StylePriority.UseTextAlignment = false;
            this.Lbtienthue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Lbtienthue.TextFormatString = "{0:#,#}";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel20.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(9.020966F, 384.5F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(63.9164F, 16.74997F);
            this.xrLabel20.StylePriority.UseBorders = false;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseForeColor = false;
            this.xrLabel20.Text = "Bằng chữ:";
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel18.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(9.020966F, 367.75F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(145.1249F, 16.74997F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseForeColor = false;
            this.xrLabel18.Text = "Tổng tiền thanh toán:";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel13.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(9.020966F, 317.5001F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(145.1249F, 16.74997F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseForeColor = false;
            this.xrLabel13.Text = "Thành tiền trước thuế:";
            // 
            // Lbbangchu
            // 
            this.Lbbangchu.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Lbbangchu.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbbangchu.LocationFloat = new DevExpress.Utils.PointFloat(72.93736F, 384.5F);
            this.Lbbangchu.Name = "Lbbangchu";
            this.Lbbangchu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbbangchu.SizeF = new System.Drawing.SizeF(311.0414F, 47.79221F);
            this.Lbbangchu.StylePriority.UseBorders = false;
            this.Lbbangchu.StylePriority.UseFont = false;
            this.Lbbangchu.Text = "Lbbangchu";
            // 
            // Lbtien
            // 
            this.Lbtien.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Lbtien.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_tien]")});
            this.Lbtien.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtien.LocationFloat = new DevExpress.Utils.PointFloat(176.8127F, 317.5001F);
            this.Lbtien.Name = "Lbtien";
            this.Lbtien.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtien.SizeF = new System.Drawing.SizeF(92.979F, 16.74997F);
            this.Lbtien.StylePriority.UseBorders = false;
            this.Lbtien.StylePriority.UseFont = false;
            this.Lbtien.StylePriority.UseTextAlignment = false;
            this.Lbtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Lbtien.TextFormatString = "{0:#,#}";
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel25.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.thue_suat]")});
            this.xrLabel25.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(119.7153F, 334.2501F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(28.43045F, 16.74997F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel25.TextFormatString = "{0:n0}";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10.00012F, 226.9167F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(259.7915F, 24.125F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Cellkl,
            this.Celldongia,
            this.Cellthanhtien});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // Cellkl
            // 
            this.Cellkl.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_so_luong]")});
            this.Cellkl.Name = "Cellkl";
            this.Cellkl.StylePriority.UseTextAlignment = false;
            this.Cellkl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.Cellkl.TextFormatString = "{0:#,#}";
            this.Cellkl.Weight = 0.45217439807988136D;
            // 
            // Celldongia
            // 
            this.Celldongia.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.don_gia]")});
            this.Celldongia.Name = "Celldongia";
            this.Celldongia.StylePriority.UseTextAlignment = false;
            this.Celldongia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Celldongia.TextFormatString = "{0:#,#}";
            this.Celldongia.Weight = 0.49845341561278639D;
            // 
            // Cellthanhtien
            // 
            this.Cellthanhtien.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_tien]")});
            this.Cellthanhtien.Name = "Cellthanhtien";
            this.Cellthanhtien.StylePriority.UseTextAlignment = false;
            this.Cellthanhtien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Cellthanhtien.TextFormatString = "{0:#,#}";
            this.Cellthanhtien.Weight = 0.88260999785089689D;
            // 
            // xrLabel35
            // 
            this.xrLabel35.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.stt_order]")});
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(321.7917F, 30F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(65.45831F, 16.74998F);
            this.xrLabel35.StylePriority.UseFont = false;
            // 
            // xrLabel33
            // 
            this.xrLabel33.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xrLabel33.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel33.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(214.7917F, 48.7083F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(55F, 16.74998F);
            this.xrLabel33.StylePriority.UseBorderColor = false;
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.StylePriority.UseForeColor = false;
            this.xrLabel33.Text = "Sổ đọc: ";
            // 
            // xrLabel34
            // 
            this.xrLabel34.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.so_doc]")});
            this.xrLabel34.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(269.7917F, 49.2083F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(114.1871F, 16.74999F);
            this.xrLabel34.StylePriority.UseFont = false;
            // 
            // xrLabel32
            // 
            this.xrLabel32.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel32.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(10.00012F, 46.75F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(55.00008F, 16.74998F);
            this.xrLabel32.StylePriority.UseBorderColor = false;
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseForeColor = false;
            this.xrLabel32.Text = "Trạm: ";
            // 
            // xrLabel31
            // 
            this.xrLabel31.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel31.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(10.00012F, 30F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(55.00008F, 16.74998F);
            this.xrLabel31.StylePriority.UseBorderColor = false;
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseForeColor = false;
            this.xrLabel31.Text = "Ngày: ";
            // 
            // Lbtieuthu
            // 
            this.Lbtieuthu.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.t_so_luong]")});
            this.Lbtieuthu.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtieuthu.LocationFloat = new DevExpress.Utils.PointFloat(328.8332F, 184.375F);
            this.Lbtieuthu.Name = "Lbtieuthu";
            this.Lbtieuthu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtieuthu.SizeF = new System.Drawing.SizeF(55.14569F, 16.74997F);
            this.Lbtieuthu.StylePriority.UseFont = false;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel17.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 184.3749F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(55.00019F, 16.74998F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseForeColor = false;
            this.xrLabel17.Text = "CS mới:";
            // 
            // LbCSM
            // 
            this.LbCSM.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.cs_bd]")});
            this.LbCSM.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.LbCSM.LocationFloat = new DevExpress.Utils.PointFloat(69.75F, 184.3749F);
            this.LbCSM.Name = "LbCSM";
            this.LbCSM.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbCSM.SizeF = new System.Drawing.SizeF(66.36111F, 16.74998F);
            this.LbCSM.StylePriority.UseFont = false;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel19.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(140.6041F, 184.3749F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(47.39584F, 16.74998F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseForeColor = false;
            this.xrLabel19.Text = "CS cũ:";
            // 
            // LbCSC
            // 
            this.LbCSC.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.cs_kt]")});
            this.LbCSC.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.LbCSC.LocationFloat = new DevExpress.Utils.PointFloat(194.4374F, 184.3749F);
            this.LbCSC.Name = "LbCSC";
            this.LbCSC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbCSC.SizeF = new System.Drawing.SizeF(72.99977F, 16.74998F);
            this.LbCSC.StylePriority.UseFont = false;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel21.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(269.8332F, 184.375F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(56.1456F, 16.74997F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseForeColor = false;
            this.xrLabel21.Text = "TT(m3):";
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel14.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(224.6042F, 167.625F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(49.25005F, 16.74997F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseForeColor = false;
            this.xrLabel14.Text = "Mã KH:";
            // 
            // LbMKH
            // 
            this.LbMKH.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.ma_kh]")});
            this.LbMKH.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.LbMKH.LocationFloat = new DevExpress.Utils.PointFloat(273.8542F, 167.625F);
            this.LbMKH.Name = "LbMKH";
            this.LbMKH.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbMKH.SizeF = new System.Drawing.SizeF(110.1245F, 16.74997F);
            this.LbMKH.StylePriority.UseFont = false;
            // 
            // LbMST
            // 
            this.LbMST.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.Ma_so_thue]")});
            this.LbMST.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.LbMST.LocationFloat = new DevExpress.Utils.PointFloat(46.25014F, 167.625F);
            this.LbMST.Name = "LbMST";
            this.LbMST.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbMST.SizeF = new System.Drawing.SizeF(167.7915F, 16.74997F);
            this.LbMST.StylePriority.UseFont = false;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel12.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(10.00005F, 167.625F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(36.25008F, 16.74997F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.Text = "MST: ";
            // 
            // Lbdiachi
            // 
            this.Lbdiachi.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.Dia_chi]")});
            this.Lbdiachi.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbdiachi.LocationFloat = new DevExpress.Utils.PointFloat(65.00009F, 121.2917F);
            this.Lbdiachi.Name = "Lbdiachi";
            this.Lbdiachi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbdiachi.SizeF = new System.Drawing.SizeF(318.9787F, 46.33331F);
            this.Lbdiachi.StylePriority.UseFont = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel11.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 121.2917F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(55.00008F, 16.74998F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseForeColor = false;
            this.xrLabel11.Text = "Địa chỉ: ";
            // 
            // Lbtenkh
            // 
            this.Lbtenkh.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.Ten_kh]")});
            this.Lbtenkh.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtenkh.LocationFloat = new DevExpress.Utils.PointFloat(65.00011F, 67.95829F);
            this.Lbtenkh.Name = "Lbtenkh";
            this.Lbtenkh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtenkh.SizeF = new System.Drawing.SizeF(318.9788F, 53.3334F);
            this.Lbtenkh.StylePriority.UseFont = false;
            // 
            // xrLabel10
            // 
            this.xrLabel10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLabel10.ForeColor = System.Drawing.Color.Navy;
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 65.4583F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(55.00008F, 16.74998F);
            this.xrLabel10.StylePriority.UseBorderColor = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseForeColor = false;
            this.xrLabel10.Text = "Tên KH: ";
            // 
            // Lbtram
            // 
            this.Lbtram.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbtram.LocationFloat = new DevExpress.Utils.PointFloat(65.00011F, 48.7083F);
            this.Lbtram.Name = "Lbtram";
            this.Lbtram.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbtram.SizeF = new System.Drawing.SizeF(134.9998F, 16.74998F);
            this.Lbtram.StylePriority.UseFont = false;
            this.Lbtram.Text = "Trạm: ";
            // 
            // Lbngay
            // 
            this.Lbngay.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[str_Invoice.ngay_ct0]")});
            this.Lbngay.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Lbngay.LocationFloat = new DevExpress.Utils.PointFloat(65.00011F, 30F);
            this.Lbngay.Name = "Lbngay";
            this.Lbngay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbngay.SizeF = new System.Drawing.SizeF(105.2915F, 16.74998F);
            this.Lbngay.StylePriority.UseFont = false;
            this.Lbngay.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 66F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 18F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // str_InvoiceTableAdapter1
            // 
            this.str_InvoiceTableAdapter1.ClearBeforeFill = true;
            // 
            // XtraReportBN
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(17, 0, 66, 18);
            this.PageHeight = 550;
            this.PageWidth = 710;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }






        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel Lbdiachi;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel Lbtenkh;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel Lbtram;
        private DevExpress.XtraReports.UI.XRLabel Lbngay;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel LbMKH;
        private DevExpress.XtraReports.UI.XRLabel LbMST;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel LbCSM;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel LbCSC;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel Lbtieuthu;
        private HDDT_CTNS.DataSet1 dataSet11;
        private HDDT_CTNS.DataSet1TableAdapters.str_InvoiceTableAdapter str_InvoiceTableAdapter1;
        private XRLabel xrLabel33;
        private XRLabel xrLabel34;
        private XRLabel xrLabel32;
        private XRLabel xrLabel31;
        private XRLabel xrLabel35;
        private XRLabel xrLabel26;
        private XRLabel Lbtongtien;
        private XRLabel Lbtienphimt;
        private XRLabel Lbtienthue;
        private XRLabel xrLabel20;
        private XRLabel xrLabel18;
        private XRLabel xrLabel13;
        private XRLabel Lbbangchu;
        private XRLabel Lbtien;
        private XRLabel xrLabel25;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell Cellkl;
        private XRTableCell Celldongia;
        private XRTableCell Cellthanhtien;
    }
}
