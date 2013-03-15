CREATE proc [dbo].[brn_GetNextBanner]

@ClientCode varchar(10),
@BannerGroupId uniqueidentifier,
@Params NameValueCollectionType  READONLY



as



select top 1 b.* 
from  
Banner b
--inner join dbo.BannerReference br on b.BannerId = br.BannerId
--inner join dbo.BannerReferenceType brt on br.BannerReferenceTypeId = brt.BannerReferenceTypeId
--inner join @Params p on brt.Title = p.Name and br.Value = p.Value
where b.BannerGroupId = @BannerGroupId 
	 -- and getdate() between b.StartDateTime and b.EndDateTime
	  --and b.IsDeleted = 0
	  --and b.ClientCode = @ClientCode
	  order by b.RequestCount asc
	  


