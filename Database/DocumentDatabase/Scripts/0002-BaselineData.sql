SET IDENTITY_INSERT [dbo].[DocumentCategory] ON 

GO

IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'General')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (1, N'General', 1, 365, N'.jpg;.jpeg;.bmp;.gif;.tif;.tiff;.png', 6291456)
end

GO
IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'Logos')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (2, N'Logos', 2, 0, N'.gif;.jpeg;.jpg', 6291456)
end

GO
IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'Permanent')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (3, N'Permanent', 3, 0, N'.jpg;.jpeg;.bmp;.gif;.tif;.tiff;.png', 6291456)
end

GO
IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'OnlineAd')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (4, N'OnlineAd', 4, 0, N'.jpg;.jpeg;.bmp;.gif;.tif;.tiff;.png', 6291456)
end
GO

IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'LineAd')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (5, N'LineAd', 5, 0, N'.jpg;.jpeg;.bmp;.gif;.tif;.tiff;.png', 6291456)
end

GO

IF NOT EXISTS ( SELECT 1 FROM dbo.DocumentCategory WHERE Title = 'Banner')
begin
	INSERT [dbo].[DocumentCategory] ([DocumentCategoryId], [Title], [Code], [ExpiryPurgeDays], [AcceptedFileTypes], [MaximumFileSize]) VALUES (6, N'Banner', 6, 0, N'.jpg;.jpeg;.bmp;.gif;.tif;.tiff;.png', 6291456)
end
GO
SET IDENTITY_INSERT [dbo].[DocumentCategory] OFF
GO
