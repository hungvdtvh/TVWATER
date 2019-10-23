insert into [phW] select  * from [TVWATERSERVER].[TVWater].[dbo].[phW] where thang=6 and nam=2017
delete  from [Dmkh]
insert into [Dmkh] select  * from [TVWATERSERVER].[TVWater].[dbo].[Dmkh]
delete  from [Dmsodoc]
insert into [Dmsodoc] select  * from [TVWATERSERVER].[TVWater].[dbo].[Dmsodoc]
delete  from [dmnvbh]
insert into [dmnvbh] select  * from [TVWATERSERVER].[TVWater].[dbo].[dmnvbh]
  

delete  from [phW] where thang=6 and nam=2017