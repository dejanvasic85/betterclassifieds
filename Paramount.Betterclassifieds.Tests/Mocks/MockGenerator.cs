
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests
{
	///
	/// Auto-Generated code. See MockGenerator.tt. All classes should be partial, so you can extend the methods for each!
	///

	internal partial class AdSearchResultMockBuilder : MockBuilder<AdSearchResultMockBuilder, AdSearchResult>
	{	
		public AdSearchResultMockBuilder WithAdId(Int32 val)
		{ 
			return WithBuildStep(p => p.AdId = val);
		}
		public AdSearchResultMockBuilder WithOnlineAdId(Int32 val)
		{ 
			return WithBuildStep(p => p.OnlineAdId = val);
		}
		public AdSearchResultMockBuilder WithHeading(String val)
		{ 
			return WithBuildStep(p => p.Heading = val);
		}
		public AdSearchResultMockBuilder WithDescription(String val)
		{ 
			return WithBuildStep(p => p.Description = val);
		}
		public AdSearchResultMockBuilder WithHtmlText(String val)
		{ 
			return WithBuildStep(p => p.HtmlText = val);
		}
		public AdSearchResultMockBuilder WithPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.Price = val);
		}
		public AdSearchResultMockBuilder WithBookingDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.BookingDate = val);
		}
		public AdSearchResultMockBuilder WithNumOfViews(Int32 val)
		{ 
			return WithBuildStep(p => p.NumOfViews = val);
		}
		public AdSearchResultMockBuilder WithContactName(String val)
		{ 
			return WithBuildStep(p => p.ContactName = val);
		}
		public AdSearchResultMockBuilder WithContactPhone(String val)
		{ 
			return WithBuildStep(p => p.ContactPhone = val);
		}
		public AdSearchResultMockBuilder WithContactEmail(String val)
		{ 
			return WithBuildStep(p => p.ContactEmail = val);
		}
		public AdSearchResultMockBuilder WithUsername(String val)
		{ 
			return WithBuildStep(p => p.Username = val);
		}
		public AdSearchResultMockBuilder WithImageUrls(String[] val)
		{ 
			return WithBuildStep(p => p.ImageUrls = val);
		}
		public AdSearchResultMockBuilder WithPublications(String[] val)
		{ 
			return WithBuildStep(p => p.Publications = val);
		}
		public AdSearchResultMockBuilder WithCategoryId(Int32 val)
		{ 
			return WithBuildStep(p => p.CategoryId = val);
		}
		public AdSearchResultMockBuilder WithCategoryName(String val)
		{ 
			return WithBuildStep(p => p.CategoryName = val);
		}
		public AdSearchResultMockBuilder WithParentCategoryId(Int32? val)
		{ 
			return WithBuildStep(p => p.ParentCategoryId = val);
		}
		public AdSearchResultMockBuilder WithLocationId(Int32 val)
		{ 
			return WithBuildStep(p => p.LocationId = val);
		}
		public AdSearchResultMockBuilder WithLocationName(String val)
		{ 
			return WithBuildStep(p => p.LocationName = val);
		}
		public AdSearchResultMockBuilder WithLocationAreaName(String val)
		{ 
			return WithBuildStep(p => p.LocationAreaName = val);
		}
		public AdSearchResultMockBuilder WithLocationAreaId(Int32 val)
		{ 
			return WithBuildStep(p => p.LocationAreaId = val);
		}
		public AdSearchResultMockBuilder WithTotalCount(Int32 val)
		{ 
			return WithBuildStep(p => p.TotalCount = val);
		}
		public AdSearchResultMockBuilder WithParentCategoryName(String val)
		{ 
			return WithBuildStep(p => p.ParentCategoryName = val);
		}
		public AdSearchResultMockBuilder WithStartDate(DateTime val)
		{ 
			return WithBuildStep(p => p.StartDate = val);
		}
		public AdSearchResultMockBuilder WithEndDate(DateTime val)
		{ 
			return WithBuildStep(p => p.EndDate = val);
		}
		public AdSearchResultMockBuilder WithCategoryAdType(String val)
		{ 
			return WithBuildStep(p => p.CategoryAdType = val);
		}
	}

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
		public AdBookingModelMockBuilder WithCategoryAdType(String val)
		{ 
			return WithBuildStep(p => p.CategoryAdType = val);
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

	internal partial class EventModelMockBuilder : MockBuilder<EventModelMockBuilder, EventModel>
	{	
		public EventModelMockBuilder WithEventId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventModelMockBuilder WithOnlineAdId(Int32 val)
		{ 
			return WithBuildStep(p => p.OnlineAdId = val);
		}
		public EventModelMockBuilder WithLocation(String val)
		{ 
			return WithBuildStep(p => p.Location = val);
		}
		public EventModelMockBuilder WithLocationLatitude(Decimal? val)
		{ 
			return WithBuildStep(p => p.LocationLatitude = val);
		}
		public EventModelMockBuilder WithLocationLongitude(Decimal? val)
		{ 
			return WithBuildStep(p => p.LocationLongitude = val);
		}
		public EventModelMockBuilder WithEventStartDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventStartDate = val);
		}
		public EventModelMockBuilder WithEventEndDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventEndDate = val);
		}
		public EventModelMockBuilder WithTickets(IList<EventTicket> val)
		{ 
			return WithBuildStep(p => p.Tickets = val);
		}
		public EventModelMockBuilder WithTicketFields(IList<EventTicketField> val)
		{ 
			return WithBuildStep(p => p.TicketFields = val);
		}
		public EventModelMockBuilder WithEventBookings(IList<EventBooking> val)
		{ 
			return WithBuildStep(p => p.EventBookings = val);
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

	internal partial class EventBookingTicketMockBuilder : MockBuilder<EventBookingTicketMockBuilder, EventBookingTicket>
	{	
		public EventBookingTicketMockBuilder WithEventBookingTicketId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventBookingTicketId = val);
		}
		public EventBookingTicketMockBuilder WithEventBookingId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventBookingId = val);
		}
		public EventBookingTicketMockBuilder WithEventBooking(EventBooking val)
		{ 
			return WithBuildStep(p => p.EventBooking = val);
		}
		public EventBookingTicketMockBuilder WithEventTicketId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventBookingTicketMockBuilder WithEventTicket(EventTicket val)
		{ 
			return WithBuildStep(p => p.EventTicket = val);
		}
		public EventBookingTicketMockBuilder WithTicketName(String val)
		{ 
			return WithBuildStep(p => p.TicketName = val);
		}
		public EventBookingTicketMockBuilder WithPrice(Decimal? val)
		{ 
			return WithBuildStep(p => p.Price = val);
		}
		public EventBookingTicketMockBuilder WithGuestFullName(String val)
		{ 
			return WithBuildStep(p => p.GuestFullName = val);
		}
		public EventBookingTicketMockBuilder WithGuestEmail(String val)
		{ 
			return WithBuildStep(p => p.GuestEmail = val);
		}
		public EventBookingTicketMockBuilder WithCreatedDateTime(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTime = val);
		}
		public EventBookingTicketMockBuilder WithCreatedDateTimeUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTimeUtc = val);
		}
		public EventBookingTicketMockBuilder WithTicketFieldValues(List<EventBookingTicketField> val)
		{ 
			return WithBuildStep(p => p.TicketFieldValues = val);
		}
	}

	internal partial class EventBookingMockBuilder : MockBuilder<EventBookingMockBuilder, EventBooking>
	{	
		public EventBookingMockBuilder WithEventBookingId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventBookingId = val);
		}
		public EventBookingMockBuilder WithEventId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventBookingMockBuilder WithEvent(EventModel val)
		{ 
			return WithBuildStep(p => p.Event = val);
		}
		public EventBookingMockBuilder WithEventBookingTickets(IList<EventBookingTicket> val)
		{ 
			return WithBuildStep(p => p.EventBookingTickets = val);
		}
		public EventBookingMockBuilder WithUserId(String val)
		{ 
			return WithBuildStep(p => p.UserId = val);
		}
		public EventBookingMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public EventBookingMockBuilder WithFirstName(String val)
		{ 
			return WithBuildStep(p => p.FirstName = val);
		}
		public EventBookingMockBuilder WithLastName(String val)
		{ 
			return WithBuildStep(p => p.LastName = val);
		}
		public EventBookingMockBuilder WithPostCode(String val)
		{ 
			return WithBuildStep(p => p.PostCode = val);
		}
		public EventBookingMockBuilder WithPhone(String val)
		{ 
			return WithBuildStep(p => p.Phone = val);
		}
		public EventBookingMockBuilder WithTotalCost(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalCost = val);
		}
		public EventBookingMockBuilder WithPaymentMethod(PaymentType val)
		{ 
			return WithBuildStep(p => p.PaymentMethod = val);
		}
		public EventBookingMockBuilder WithPaymentReference(String val)
		{ 
			return WithBuildStep(p => p.PaymentReference = val);
		}
		public EventBookingMockBuilder WithStatus(EventBookingStatus val)
		{ 
			return WithBuildStep(p => p.Status = val);
		}
		public EventBookingMockBuilder WithCreatedDateTime(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTime = val);
		}
		public EventBookingMockBuilder WithCreatedDateTimeUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTimeUtc = val);
		}
		public EventBookingMockBuilder WithStatusAsString(String val)
		{ 
			return WithBuildStep(p => p.StatusAsString = val);
		}
		public EventBookingMockBuilder WithPaymentMethodAsString(String val)
		{ 
			return WithBuildStep(p => p.PaymentMethodAsString = val);
		}
		public EventBookingMockBuilder WithTicketsDocumentId(Guid? val)
		{ 
			return WithBuildStep(p => p.TicketsDocumentId = val);
		}
		public EventBookingMockBuilder WithTicketsSentDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.TicketsSentDate = val);
		}
		public EventBookingMockBuilder WithTicketsSentDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.TicketsSentDateUtc = val);
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
		public EventTicketReservationMockBuilder WithGuestFullName(String val)
		{ 
			return WithBuildStep(p => p.GuestFullName = val);
		}
		public EventTicketReservationMockBuilder WithGuestEmail(String val)
		{ 
			return WithBuildStep(p => p.GuestEmail = val);
		}
		public EventTicketReservationMockBuilder WithTicketFields(List<EventBookingTicketField> val)
		{ 
			return WithBuildStep(p => p.TicketFields = val);
		}
	}

	internal partial class EventGuestDetailsMockBuilder : MockBuilder<EventGuestDetailsMockBuilder, EventGuestDetails>
	{	
		public EventGuestDetailsMockBuilder WithGuestFullName(String val)
		{ 
			return WithBuildStep(p => p.GuestFullName = val);
		}
		public EventGuestDetailsMockBuilder WithGuestEmail(String val)
		{ 
			return WithBuildStep(p => p.GuestEmail = val);
		}
		public EventGuestDetailsMockBuilder WithDynamicFields(IList<EventBookingTicketField> val)
		{ 
			return WithBuildStep(p => p.DynamicFields = val);
		}
		public EventGuestDetailsMockBuilder WithBarcodeData(String val)
		{ 
			return WithBuildStep(p => p.BarcodeData = val);
		}
		public EventGuestDetailsMockBuilder WithTicketName(String val)
		{ 
			return WithBuildStep(p => p.TicketName = val);
		}
		public EventGuestDetailsMockBuilder WithTicketNumber(Int32 val)
		{ 
			return WithBuildStep(p => p.TicketNumber = val);
		}
	}

	internal partial class EventPaymentSummaryMockBuilder : MockBuilder<EventPaymentSummaryMockBuilder, EventPaymentSummary>
	{	
		public EventPaymentSummaryMockBuilder WithTotalTicketSalesAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalTicketSalesAmount = val);
		}
		public EventPaymentSummaryMockBuilder WithSystemTicketFee(Decimal val)
		{ 
			return WithBuildStep(p => p.SystemTicketFee = val);
		}
		public EventPaymentSummaryMockBuilder WithEventOrganiserOwedAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.EventOrganiserOwedAmount = val);
		}
	}
}


