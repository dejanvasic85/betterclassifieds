using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests
{
	///
	/// Auto-Generated code. See MockGenerator.tt. All classes should be partial, so you can extend the methods for each!
	///

	internal partial class AdBookingModelMockBuilder : MockBuilder<AdBookingModelMockBuilder, AdBookingModel>
	{	
		public AdBookingModelMockBuilder WithAdBookingId(Int32 val)
		{ 
			return WithBuildStep(p => p.AdBookingId = val);
		}
		public AdBookingModelMockBuilder WithStartDate(DateTime val)
		{ 
			return WithBuildStep(p => p.StartDate = val);
		}
		public AdBookingModelMockBuilder WithEndDate(DateTime val)
		{ 
			return WithBuildStep(p => p.EndDate = val);
		}
		public AdBookingModelMockBuilder WithSubCategoryId(Int32? val)
		{ 
			return WithBuildStep(p => p.SubCategoryId = val);
		}
		public AdBookingModelMockBuilder WithCategoryId(Int32? val)
		{ 
			return WithBuildStep(p => p.CategoryId = val);
		}
		public AdBookingModelMockBuilder WithInsertions(Int32 val)
		{ 
			return WithBuildStep(p => p.Insertions = val);
		}
		public AdBookingModelMockBuilder WithBookingType(BookingType val)
		{ 
			return WithBuildStep(p => p.BookingType = val);
		}
		public AdBookingModelMockBuilder WithTotalPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalPrice = val);
		}
		public AdBookingModelMockBuilder WithUserId(String val)
		{ 
			return WithBuildStep(p => p.UserId = val);
		}
		public AdBookingModelMockBuilder WithBookingStatus(BookingStatusType val)
		{ 
			return WithBuildStep(p => p.BookingStatus = val);
		}
		public AdBookingModelMockBuilder WithBookReference(String val)
		{ 
			return WithBuildStep(p => p.BookReference = val);
		}
		public AdBookingModelMockBuilder WithAds(List<IAd> val)
		{ 
			return WithBuildStep(p => p.Ads = val);
		}
		public AdBookingModelMockBuilder WithEnquiries(List<Enquiry> val)
		{ 
			return WithBuildStep(p => p.Enquiries = val);
		}
		public AdBookingModelMockBuilder WithPublications(Int32[] val)
		{ 
			return WithBuildStep(p => p.Publications = val);
		}
	}

	internal partial class OnlineAdRateMockBuilder : MockBuilder<OnlineAdRateMockBuilder, OnlineAdRate>
	{	
		public OnlineAdRateMockBuilder WithOnlineAdRateId(Int32 val)
		{ 
			return WithBuildStep(p => p.OnlineAdRateId = val);
		}
		public OnlineAdRateMockBuilder WithCategoryId(Int32? val)
		{ 
			return WithBuildStep(p => p.CategoryId = val);
		}
		public OnlineAdRateMockBuilder WithMinimumCharge(Decimal val)
		{ 
			return WithBuildStep(p => p.MinimumCharge = val);
		}
	}

	internal partial class RateModelMockBuilder : MockBuilder<RateModelMockBuilder, RateModel>
	{	
		public RateModelMockBuilder WithBaseRateId(Int32 val)
		{ 
			return WithBuildStep(p => p.BaseRateId = val);
		}
		public RateModelMockBuilder WithRatecardId(Int32 val)
		{ 
			return WithBuildStep(p => p.RatecardId = val);
		}
		public RateModelMockBuilder WithMinCharge(Decimal? val)
		{ 
			return WithBuildStep(p => p.MinCharge = val);
		}
		public RateModelMockBuilder WithMaxCharge(Decimal? val)
		{ 
			return WithBuildStep(p => p.MaxCharge = val);
		}
		public RateModelMockBuilder WithRatePerWord(Decimal? val)
		{ 
			return WithBuildStep(p => p.RatePerWord = val);
		}
		public RateModelMockBuilder WithFreeWords(Int32 val)
		{ 
			return WithBuildStep(p => p.FreeWords = val);
		}
		public RateModelMockBuilder WithPhotoCharge(Decimal? val)
		{ 
			return WithBuildStep(p => p.PhotoCharge = val);
		}
		public RateModelMockBuilder WithBoldHeading(Decimal? val)
		{ 
			return WithBuildStep(p => p.BoldHeading = val);
		}
		public RateModelMockBuilder WithOnlineEditionBundle(Decimal? val)
		{ 
			return WithBuildStep(p => p.OnlineEditionBundle = val);
		}
		public RateModelMockBuilder WithLineAdSuperBoldHeading(Decimal? val)
		{ 
			return WithBuildStep(p => p.LineAdSuperBoldHeading = val);
		}
		public RateModelMockBuilder WithLineAdColourHeading(Decimal? val)
		{ 
			return WithBuildStep(p => p.LineAdColourHeading = val);
		}
		public RateModelMockBuilder WithLineAdColourBorder(Decimal? val)
		{ 
			return WithBuildStep(p => p.LineAdColourBorder = val);
		}
		public RateModelMockBuilder WithLineAdColourBackground(Decimal? val)
		{ 
			return WithBuildStep(p => p.LineAdColourBackground = val);
		}
		public RateModelMockBuilder WithCreatedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public RateModelMockBuilder WithCreatedByUser(String val)
		{ 
			return WithBuildStep(p => p.CreatedByUser = val);
		}
		public RateModelMockBuilder WithPublicationId(Int32 val)
		{ 
			return WithBuildStep(p => p.PublicationId = val);
		}
	}

	internal partial class AdEnquiryMockBuilder : MockBuilder<AdEnquiryMockBuilder, AdEnquiry>
	{	
		public AdEnquiryMockBuilder WithEnquiryId(Int32? val)
		{ 
			return WithBuildStep(p => p.EnquiryId = val);
		}
		public AdEnquiryMockBuilder WithAdId(Int32 val)
		{ 
			return WithBuildStep(p => p.AdId = val);
		}
		public AdEnquiryMockBuilder WithFullName(String val)
		{ 
			return WithBuildStep(p => p.FullName = val);
		}
		public AdEnquiryMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public AdEnquiryMockBuilder WithPhone(String val)
		{ 
			return WithBuildStep(p => p.Phone = val);
		}
		public AdEnquiryMockBuilder WithQuestion(String val)
		{ 
			return WithBuildStep(p => p.Question = val);
		}
	}

	internal partial class ApplicationUserMockBuilder : MockBuilder<ApplicationUserMockBuilder, ApplicationUser>
	{	
		public ApplicationUserMockBuilder WithUsername(String val)
		{ 
			return WithBuildStep(p => p.Username = val);
		}
		public ApplicationUserMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public ApplicationUserMockBuilder WithFirstName(String val)
		{ 
			return WithBuildStep(p => p.FirstName = val);
		}
		public ApplicationUserMockBuilder WithLastName(String val)
		{ 
			return WithBuildStep(p => p.LastName = val);
		}
		public ApplicationUserMockBuilder WithAddressLine1(String val)
		{ 
			return WithBuildStep(p => p.AddressLine1 = val);
		}
		public ApplicationUserMockBuilder WithAddressLine2(String val)
		{ 
			return WithBuildStep(p => p.AddressLine2 = val);
		}
		public ApplicationUserMockBuilder WithCity(String val)
		{ 
			return WithBuildStep(p => p.City = val);
		}
		public ApplicationUserMockBuilder WithState(String val)
		{ 
			return WithBuildStep(p => p.State = val);
		}
		public ApplicationUserMockBuilder WithPostcode(String val)
		{ 
			return WithBuildStep(p => p.Postcode = val);
		}
		public ApplicationUserMockBuilder WithPhone(String val)
		{ 
			return WithBuildStep(p => p.Phone = val);
		}
	}

	internal partial class EventTicketMockBuilder : MockBuilder<EventTicketMockBuilder, EventTicket>
	{	
		public EventTicketMockBuilder WithEventTicketId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventTicketMockBuilder WithEventId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventTicketMockBuilder WithTicketName(String val)
		{ 
			return WithBuildStep(p => p.TicketName = val);
		}
		public EventTicketMockBuilder WithAvailableQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.AvailableQuantity = val);
		}
		public EventTicketMockBuilder WithRemainingQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.RemainingQuantity = val);
		}
		public EventTicketMockBuilder WithPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.Price = val);
		}
		public EventTicketMockBuilder WithEventTicketReservations(IList<EventTicketReservation> val)
		{ 
			return WithBuildStep(p => p.EventTicketReservations = val);
		}
		public EventTicketMockBuilder WithEventBookingTickets(IList<EventBookingTicket> val)
		{ 
			return WithBuildStep(p => p.EventBookingTickets = val);
		}
	}

	internal partial class EventTicketReservationMockBuilder : MockBuilder<EventTicketReservationMockBuilder, EventTicketReservation>
	{	
		public EventTicketReservationMockBuilder WithEventTicketReservationId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketReservationId = val);
		}
		public EventTicketReservationMockBuilder WithEventTicketId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventTicketReservationMockBuilder WithEventTicket(EventTicket val)
		{ 
			return WithBuildStep(p => p.EventTicket = val);
		}
		public EventTicketReservationMockBuilder WithQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.Quantity = val);
		}
		public EventTicketReservationMockBuilder WithPrice(Decimal? val)
		{ 
			return WithBuildStep(p => p.Price = val);
		}
		public EventTicketReservationMockBuilder WithSessionId(String val)
		{ 
			return WithBuildStep(p => p.SessionId = val);
		}
		public EventTicketReservationMockBuilder WithStatus(EventTicketReservationStatus val)
		{ 
			return WithBuildStep(p => p.Status = val);
		}
		public EventTicketReservationMockBuilder WithExpiryDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.ExpiryDate = val);
		}
		public EventTicketReservationMockBuilder WithExpiryDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.ExpiryDateUtc = val);
		}
		public EventTicketReservationMockBuilder WithCreatedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public EventTicketReservationMockBuilder WithCreatedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateUtc = val);
		}
		public EventTicketReservationMockBuilder WithStatusAsString(String val)
		{ 
			return WithBuildStep(p => p.StatusAsString = val);
		}
	}

	internal partial class EventTicketReservationRequestMockBuilder : MockBuilder<EventTicketReservationRequestMockBuilder, EventTicketReservationRequest>
	{	
		public EventTicketReservationRequestMockBuilder WithQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.Quantity = val);
		}
		public EventTicketReservationRequestMockBuilder WithEventTicket(EventTicket val)
		{ 
			return WithBuildStep(p => p.EventTicket = val);
		}
	}
}


