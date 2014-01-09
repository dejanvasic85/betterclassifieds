﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BetterclassifiedsCore.DataModel;
using Paramount;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
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

        public TutorAdModel GetTutorAd(int onlineAdId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return this.Map<TutorAd, TutorAdModel>(context.TutorAds.FirstOrDefault(onlinead => onlinead.OnlineAdId == onlineAdId));
            }
        }

        public void UpdateTutor(TutorAdModel tutorAdModel)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                // Fetch original
                var tutorAd = context.TutorAds.Single(t => t.TutorAdId == tutorAdModel.TutorAdId);
                // Map new property changes
                this.Map(tutorAdModel, tutorAd);
                // Commit the changes
                context.SubmitChanges();
            }
        }

        public List<OnlineAdModel> GetLatestAds(int takeLast = 10)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                // Get the latest online ads
                var ads = context.OnlineAds.OrderByDescending(o => o.OnlineAdId).Take(10).ToList();

                // Map to the models
                return this.MapList<OnlineAd, OnlineAdModel>(ads);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From Db
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<TutorAd, TutorAdModel>();

            // To Db
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(member => member.AdDesignId, options => options.Ignore());
            configuration.CreateMap<TutorAdModel, TutorAd>().ForMember(member => member.OnlineAd, options => options.Ignore());
        }
    }
}