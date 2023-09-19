
/*

	select * 
	from [dbo].[MEMBER_ADDRESS] 
	where MEMBER_ID = 1451

	select MEMBER_ID, [LABEL]
	from [dbo].[MEMBER_ADDRESS]
	group by MEMBER_ID, [LABEL]
	having count(MEMBER_ID) > 1

	insert [dbo].[MEMBER_ADDRESS](MEMBER_ID, LABEL, STREET_1, STREET_2, CITY, STATE, ZIP, CREATED_ON, MODIFIED_ON)
	select 1451, 'Work', '123 Nowhere St', '', 'Some City',	'OH', 43068, getdate(), getdate()

*/

with cte as 
(
	select *
	from (
		select RANK() over(partition by MEMBER_ID, [LABEL] order by MODIFIED_ON desc, [LABEL]) LabelRank, *
		from [dbo].[MEMBER_ADDRESS]
	) tbl
	where LabelRank > 1
)

delete from cte
output deleted.MEMBER_ID, deleted.MEMBER_ADDRESS_ID;

--==================================================================
ALTER  TABLE  [dbo].[MEMBER_ADDRESS] WITH CHECK 
   ADD CONSTRAINT UQ_MEMBER_ADDRESS_Label UNIQUE (MEMBER_ID, [Label])
