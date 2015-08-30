SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE dbo.MainCategory ADD
	CategoryAdType nvarchar(50) NULL
Go

ALTER TABLE dbo.MainCategory
DROP COLUMN ViewMap