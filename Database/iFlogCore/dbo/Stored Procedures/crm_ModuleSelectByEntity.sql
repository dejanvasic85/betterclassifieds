create PROC [dbo].[crm_ModuleSelectByEntity] 
    @EntityCode INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT m.*
	FROM   [dbo].[Module]  m
	inner join dbo.EntityModule em
	on em.ModuleId = m.ModuleId
	WHERE  em.[EntityCode] = @EntityCode  

	COMMIT
