-- =============================================
-- Author:		Dejan Vasic
-- Create date: 19th January 2009
-- Modifications
-- Date			Author			Description
--
-- 20-3-2009	Dejan Vasic		Added ORDER BY Title clause
-- =============================================
CREATE PROCEDURE [dbo].[spGetMainParentCategories]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	SELECT * FROM MainCategory WHERE ParentId IS NULL
	ORDER BY Title
END
