﻿using System.Linq;
using AutoMapper;
using BetterClassified.UI.Models;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UI.Repository
{
    public interface IAdRepository
    {
        OnlineAdModel GetOnlineAdByBooking(int bookingId);
    }

    public class AdRepository : IAdRepository, IMappingBehaviour
    {
        public OnlineAdModel GetOnlineAdByBooking(int bookingId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var onlineAd = context.OnlineAds.FirstOrDefault(ad => ad.AdDesign.Ad.AdBookings.First().AdBookingId == bookingId);
                if (onlineAd == null)
                    return null;
                return this.Map<OnlineAd, OnlineAdModel>(onlineAd);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From Db
            configuration.CreateMap<OnlineAd, OnlineAdModel>();

            // To Db
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(member => member.AdDesignId, options => options.Ignore());
        }
    }
}