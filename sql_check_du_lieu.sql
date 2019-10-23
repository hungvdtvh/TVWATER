select * from daph
delete from DaPH
select * from in_sodoc
select * from phw
select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from 
	(select rtrim(a.ten_so_doc) as ten_so_doc, b.* 
		from dmsodoc a 
		inner join 
			(select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien 
				from phW 
				where 
				thang=06 and nam=2018 group by so_doc) b on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c order by so_doc
---------------------------------------------------------
select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from 
	(select rtrim(a.ten_so_doc) as ten_so_doc, b.* 
	from dmsodoc a 
		inner join 
			(select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien 
				from phW where thang=06 and nam=2018 group by so_doc) b 
					on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c 
					where c.so_doc not in (select so_doc from in_sodoc d 
					where d.thang=06 and d.nam=2018 ) order by so_doc
--------------------------------------------------------
select c.*,CAST(COALESCE(NULL,0) AS BIT) as status from 
	(select rtrim(a.ten_so_doc) as ten_so_doc, b.* 
	from dmsodoc a 
		inner join 
			(select so_doc,count(*) as sl,sum(t_tien) as tien,sum(t_thue) as thue,sum(t_khac) as t_khac,sum(t_phi_thai) as t_phi_thai,sum(t_tt) as tong_tien 
				from phW where thang=06 and nam=2018 group by so_doc) b 
					on Rtrim(Ltrim(a.so_doc))=Rtrim(Ltrim(b.so_doc))) c 
					where c.so_doc not in (select so_doc from in_sodoc d 
					where d.thang=06 and d.nam=2018 ) order by so_doc