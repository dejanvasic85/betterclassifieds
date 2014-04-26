IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSearchOnlineAdFREETEXT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSearchOnlineAdFREETEXT]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSearchOnlineAdFREETEXT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:           Uche Njoku
-- Create date: 1/02/2014
-- Description:      Search for ad using FREETEXT 
-- =============================================
--  order rule:
-- most recent ad = 0
-- oldest ads = 1
-- lowest price = 2
-- highest price = 3
-- order by search rankong = 4


create PROCEDURE [dbo].[spSearchOnlineAdFREETEXT]
       @searchTerm nvarchar(50) = ''~'',
       @categoryIds varchar(20) = null,
       @locationIds varchar(20) = null,
       @areaIds varchar(20) = null,
       @order int = 0,
       @pageIndex int = 0,
       @pageSize int = -1

AS
BEGIN
       SET NOCOUNT ON;


       SET @searchTerm=isnull(@searchTerm,''~'')

       -- Setup paging
       if (@pageSize <= 0)
       begin
              set @pageIndex = 0
              set @pageSize = 20
       end
       
       ;with results
       as
       (
       select ROW_NUMBER() over ( order by
       case when (@order = 0)
       then [AdBookingId]
       end desc,

       case when (@order= 1)
       then [AdBookingId]
       end asc,

       case when (@order= 2)
       then [Price]
       end asc,

       case when (@order= 3)
       then [Price]
       end desc,

	   case when (@order= 4)
	   then Rank
	   end desc
       ) as RowNumber,
       Count(*) over() as TotalCount,
       t1.*
              from   
              (select o.*, KEY_TBL.Rank from [dbo].[OnlineAdview] o  
              INNER JOIN 
                     FREETEXTTABLE([dbo].[OnlineAdview],
              (Description, Heading), @searchTerm) AS KEY_TBL
              ON  (o.OnlineAdId = KEY_TBL.[KEY]) 
              --and KEY_TBL.Rank >2
              --ORDER BY RANK desc
              union ( select *, OnlineAdId as Rank from [dbo].[OnlineAdview] 
               where @searchTerm = ''~'')  
              ) as t1
       where
       (@categoryIds is null or t1.CategoryId in (select [data] from dbo.[SplitStringToInt](@categoryIds,'','')) or t1.CategoryParentId in (select data from dbo.[SplitStringToInt](@categoryIds,'','')))
       and (@locationIds is null or t1.LocationId in (select data from dbo.[SplitStringToInt](@locationIds,'','')))
       and (@areaIds is null or t1.LocationAreaId in (select data from dbo.[SplitStringToInt](@areaIds,'','')))   
       )
       
       Select * from results where RowNumber between ((@pageSize * @pageIndex) + 1) and (@pageSize * (@pageIndex + 1))

   end
' 
END
GO
