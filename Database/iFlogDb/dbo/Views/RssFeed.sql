CREATE VIEW [dbo].[RssFeed]
AS
SELECT     TOP 100  dbo.OnlineAd.OnlineAdId, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.BookReference, dbo.AdBooking.UserId, 
                      dbo.AdBooking.MainCategoryId, dbo.Ad.AdId, dbo.Ad.Title, dbo.OnlineAd.Heading, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Description, dbo.OnlineAd.Price, 
                      dbo.OnlineAd.ContactName, dbo.MainCategory.ParentId, dbo.AdBooking.BookingDate, dbo.MainCategory.Title as CategoryTitle
FROM         dbo.OnlineAd INNER JOIN
                      dbo.AdDesign ON dbo.OnlineAd.AdDesignId = dbo.AdDesign.AdDesignId INNER JOIN
                      dbo.Ad ON dbo.AdDesign.AdId = dbo.Ad.AdId INNER JOIN
                      dbo.AdBooking ON dbo.Ad.AdId = dbo.AdBooking.AdId INNER JOIN
                      dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId
WHERE     (dbo.AdBooking.BookingStatus = 1)
ORDER BY dbo.AdBooking.BookingDate DESC

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[52] 4[9] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "OnlineAd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "AdDesign"
            Begin Extent = 
               Top = 16
               Left = 318
               Bottom = 135
               Right = 483
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdBooking"
            Begin Extent = 
               Top = 145
               Left = 51
               Bottom = 264
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Ad"
            Begin Extent = 
               Top = 111
               Left = 527
               Bottom = 230
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RssFeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RssFeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RssFeed';

