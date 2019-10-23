
USE [TVWater]
GO

/****** Object:  Table [dbo].[D_dmkh]    Script Date: 9/25/2018 5:23:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[D_dmkh](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[thang] [smallint] NULL,
	[nam] [int] NULL,
	[m_lan] [int] NULL,
 CONSTRAINT [PK_D_dmkh] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [TVWater]
GO

/****** Object:  StoredProcedure [dbo].[InvoiceList_SD]    Script Date: 9/25/2018 5:22:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Bang tong hop thang toan Hoa don cua nhan vien thu ngan>
-- =============================================
CREATE PROCEDURE [dbo].[InvoiceList_SD]
	@sSo_doc as Nvarchar(2000),
	@iMonth AS Int,
	@iYear AS Int
AS
BEGIN

DECLARE @SQL as Nvarchar(3500)


set @sSo_doc=rtrim(replace(@sSo_doc,',',''','''))

set @SQL='SELECT 0 AS chkprint, a.*,d.ten_nvbh as ten_nv, b.stt_order, b.Ten_kh,b.Dia_chi,b.Ma_so_thue, b.Ma_hd,b.So_hd
INTO  #InvoiceList 
	FROM PhW a
  inner Join Dmkh b on b.Ma_kh = a.Ma_kh 
  inner Join Dmsodoc e on e.so_doc = a.so_doc 
  LEFT join dmnvbh d on d.Ma_nvbh = e.Ma_nv_tt 	
	WHERE a.nam = '+str(@iYear)+' AND a.thang = '+str(@iMonth)+' AND Rtrim(Ltrim(a.so_doc)) in (N'+@sSo_doc+') Order by a.so_doc, b.stt_order ' +


-- SELECT   @iTotlQuality = Sum(t_so_luong), @iTotlMoney = ROUND(sum(t_tien),0) , @iTotlVat = ROUND(sum(t_thue),0),@iTotlOther  = ROUND(sum(t_khac),0) ,@iTotlTT = ROUND(sum(t_tt),0) FROM #InvoiceList Group by thang,nam,Ma_kh


 ' SELECT * FROM #InvoiceList order by so_doc, stt_order '
-- select @SQL
 exec sp_executesql @SQL
END
GO
-------------
select * from D_dmkh
