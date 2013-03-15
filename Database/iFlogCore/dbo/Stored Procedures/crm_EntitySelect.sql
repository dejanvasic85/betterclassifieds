
CREATE PROC [dbo].[crm_EntitySelect] 
    @EntityCode NVARCHAR(10) = null,
    @PageSize int = -1,
	@PageIndex int = 0,
	@TotalPopulationSize int output
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON 
	 
    BEGIN TRAN
    
    select @TotalPopulationSize = count(*)
    FROM   [dbo].[Entity] 
	WHERE  ([EntityCode] = @EntityCode OR @EntityCode IS NULL) 
	
		-- Setup paging
	if (@pageSize <= 0)
	begin
		set @pageSize = @totalPopulationSize
		set @pageIndex = 0
	end
	
	
	;with [Entities] as
	(
		select e.*, ROW_NUMBER() OVER(ORDER by e.EntityCode) as RowNumber
	 from  dbo.[Entity] e
	 WHERE  ([EntityCode] = @EntityCode OR @EntityCode IS NULL)
	 )
	 
	Select * from [Entities] where RowNumber between ((@pageSize * @pageIndex) + 1) and (@pageSize * (@pageIndex + 1))

	COMMIT

