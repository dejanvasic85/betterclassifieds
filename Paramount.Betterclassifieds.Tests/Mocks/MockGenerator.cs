
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

	internal partial class AddressMockBuilder : MockBuilder<AddressMockBuilder, Address>
	{	
		public AddressMockBuilder WithAddressId(Int64? val)
		{ 
			return WithBuildStep(p => p.AddressId = val);
		}
		public AddressMockBuilder WithStreetNumber(String val)
		{ 
			return WithBuildStep(p => p.StreetNumber = val);
		}
		public AddressMockBuilder WithStreetName(String val)
		{ 
			return WithBuildStep(p => p.StreetName = val);
		}
		public AddressMockBuilder WithSuburb(String val)
		{ 
			return WithBuildStep(p => p.Suburb = val);
		}
		public AddressMockBuilder WithState(String val)
		{ 
			return WithBuildStep(p => p.State = val);
		}
		public AddressMockBuilder WithPostcode(String val)
		{ 
			return WithBuildStep(p => p.Postcode = val);
		}
		public AddressMockBuilder WithCountry(String val)
		{ 
			return WithBuildStep(p => p.Country = val);
		}
	}

	internal partial class RegistrationModelMockBuilder : MockBuilder<RegistrationModelMockBuilder, RegistrationModel>
	{	
		public RegistrationModelMockBuilder WithRegistrationId(Int32? val)
		{ 
			return WithBuildStep(p => p.RegistrationId = val);
		}
		public RegistrationModelMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public RegistrationModelMockBuilder WithEncryptedPassword(String val)
		{ 
			return WithBuildStep(p => p.EncryptedPassword = val);
		}
		public RegistrationModelMockBuilder WithFirstName(String val)
		{ 
			return WithBuildStep(p => p.FirstName = val);
		}
		public RegistrationModelMockBuilder WithLastName(String val)
		{ 
			return WithBuildStep(p => p.LastName = val);
		}
		public RegistrationModelMockBuilder WithPostCode(String val)
		{ 
			return WithBuildStep(p => p.PostCode = val);
		}
		public RegistrationModelMockBuilder WithUsername(String val)
		{ 
			return WithBuildStep(p => p.Username = val);
		}
		public RegistrationModelMockBuilder WithToken(String val)
		{ 
			return WithBuildStep(p => p.Token = val);
		}
		public RegistrationModelMockBuilder WithExpirationDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.ExpirationDate = val);
		}
		public RegistrationModelMockBuilder WithExpirationDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.ExpirationDateUtc = val);
		}
		public RegistrationModelMockBuilder WithLastModifiedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.LastModifiedDate = val);
		}
		public RegistrationModelMockBuilder WithLastModifiedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.LastModifiedDateUtc = val);
		}
		public RegistrationModelMockBuilder WithConfirmationDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.ConfirmationDate = val);
		}
		public RegistrationModelMockBuilder WithConfirmationDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.ConfirmationDateUtc = val);
		}
		public RegistrationModelMockBuilder WithVersion(Byte[] val)
		{ 
			return WithBuildStep(p => p.Version = val);
		}
		public RegistrationModelMockBuilder WithHowYouFoundUs(String val)
		{ 
			return WithBuildStep(p => p.HowYouFoundUs = val);
		}
		public RegistrationModelMockBuilder WithPhone(String val)
		{ 
			return WithBuildStep(p => p.Phone = val);
		}
		public RegistrationModelMockBuilder WithConfirmationAttempts(Int32? val)
		{ 
			return WithBuildStep(p => p.ConfirmationAttempts = val);
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

	internal partial class EnquiryMockBuilder : MockBuilder<EnquiryMockBuilder, Enquiry>
	{	
		public EnquiryMockBuilder WithEnquiryId(Int32 val)
		{ 
			return WithBuildStep(p => p.EnquiryId = val);
		}
		public EnquiryMockBuilder WithFullName(String val)
		{ 
			return WithBuildStep(p => p.FullName = val);
		}
		public EnquiryMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public EnquiryMockBuilder WithEnquiryText(String val)
		{ 
			return WithBuildStep(p => p.EnquiryText = val);
		}
		public EnquiryMockBuilder WithCreatedDate(DateTime val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public EnquiryMockBuilder WithActive(Boolean val)
		{ 
			return WithBuildStep(p => p.Active = val);
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
		public ApplicationUserMockBuilder WithPreferredPaymentMethod(PaymentType val)
		{ 
			return WithBuildStep(p => p.PreferredPaymentMethod = val);
		}
		public ApplicationUserMockBuilder WithPayPalEmail(String val)
		{ 
			return WithBuildStep(p => p.PayPalEmail = val);
		}
		public ApplicationUserMockBuilder WithBankName(String val)
		{ 
			return WithBuildStep(p => p.BankName = val);
		}
		public ApplicationUserMockBuilder WithBankAccountName(String val)
		{ 
			return WithBuildStep(p => p.BankAccountName = val);
		}
		public ApplicationUserMockBuilder WithBankAccountNumber(String val)
		{ 
			return WithBuildStep(p => p.BankAccountNumber = val);
		}
		public ApplicationUserMockBuilder WithBankBsbNumber(String val)
		{ 
			return WithBuildStep(p => p.BankBsbNumber = val);
		}
		public ApplicationUserMockBuilder WithRequiresEventOrganiserConfirmation(Boolean? val)
		{ 
			return WithBuildStep(p => p.RequiresEventOrganiserConfirmation = val);
		}
	}

	internal partial class UserNetworkModelMockBuilder : MockBuilder<UserNetworkModelMockBuilder, UserNetworkModel>
	{	
		public UserNetworkModelMockBuilder WithLastModifiedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.LastModifiedDateUtc = val);
		}
	}

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
		public AdSearchResultMockBuilder WithCategoryFontIcon(String val)
		{ 
			return WithBuildStep(p => p.CategoryFontIcon = val);
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
		public AdBookingModelMockBuilder WithCategoryFontIcon(String val)
		{ 
			return WithBuildStep(p => p.CategoryFontIcon = val);
		}
		public AdBookingModelMockBuilder WithCategoryName(String val)
		{ 
			return WithBuildStep(p => p.CategoryName = val);
		}
		public AdBookingModelMockBuilder WithParentCategoryName(String val)
		{ 
			return WithBuildStep(p => p.ParentCategoryName = val);
		}
		public AdBookingModelMockBuilder WithLocationName(String val)
		{ 
			return WithBuildStep(p => p.LocationName = val);
		}
		public AdBookingModelMockBuilder WithLocationAreaName(String val)
		{ 
			return WithBuildStep(p => p.LocationAreaName = val);
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
		public EventModelMockBuilder WithTimeZoneId(String val)
		{ 
			return WithBuildStep(p => p.TimeZoneId = val);
		}
		public EventModelMockBuilder WithTimeZoneName(String val)
		{ 
			return WithBuildStep(p => p.TimeZoneName = val);
		}
		public EventModelMockBuilder WithTimeZoneDaylightSavingsOffsetSeconds(Int64? val)
		{ 
			return WithBuildStep(p => p.TimeZoneDaylightSavingsOffsetSeconds = val);
		}
		public EventModelMockBuilder WithTimeZoneUtcOffsetSeconds(Int64? val)
		{ 
			return WithBuildStep(p => p.TimeZoneUtcOffsetSeconds = val);
		}
		public EventModelMockBuilder WithEventStartDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventStartDate = val);
		}
		public EventModelMockBuilder WithEventStartDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventStartDateUtc = val);
		}
		public EventModelMockBuilder WithEventEndDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventEndDate = val);
		}
		public EventModelMockBuilder WithEventEndDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.EventEndDateUtc = val);
		}
		public EventModelMockBuilder WithTickets(IList<EventTicket> val)
		{ 
			return WithBuildStep(p => p.Tickets = val);
		}
		public EventModelMockBuilder WithEventBookings(IList<EventBooking> val)
		{ 
			return WithBuildStep(p => p.EventBookings = val);
		}
		public EventModelMockBuilder WithEventOrganisers(IList<EventOrganiser> val)
		{ 
			return WithBuildStep(p => p.EventOrganisers = val);
		}
		public EventModelMockBuilder WithPromoCodes(IList<EventPromoCode> val)
		{ 
			return WithBuildStep(p => p.PromoCodes = val);
		}
		public EventModelMockBuilder WithLocationFloorPlanDocumentId(String val)
		{ 
			return WithBuildStep(p => p.LocationFloorPlanDocumentId = val);
		}
		public EventModelMockBuilder WithLocationFloorPlanFilename(String val)
		{ 
			return WithBuildStep(p => p.LocationFloorPlanFilename = val);
		}
		public EventModelMockBuilder WithAddress(Address val)
		{ 
			return WithBuildStep(p => p.Address = val);
		}
		public EventModelMockBuilder WithAddressId(Int64? val)
		{ 
			return WithBuildStep(p => p.AddressId = val);
		}
		public EventModelMockBuilder WithIncludeTransactionFee(Boolean? val)
		{ 
			return WithBuildStep(p => p.IncludeTransactionFee = val);
		}
		public EventModelMockBuilder WithVenueName(String val)
		{ 
			return WithBuildStep(p => p.VenueName = val);
		}
		public EventModelMockBuilder WithGroupsRequired(Boolean? val)
		{ 
			return WithBuildStep(p => p.GroupsRequired = val);
		}
		public EventModelMockBuilder WithClosingDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.ClosingDate = val);
		}
		public EventModelMockBuilder WithClosingDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.ClosingDateUtc = val);
		}
		public EventModelMockBuilder WithOpeningDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.OpeningDate = val);
		}
		public EventModelMockBuilder WithOpeningDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.OpeningDateUtc = val);
		}
		public EventModelMockBuilder WithDisplayGuests(Boolean val)
		{ 
			return WithBuildStep(p => p.DisplayGuests = val);
		}
		public EventModelMockBuilder WithIsSeatedEvent(Boolean? val)
		{ 
			return WithBuildStep(p => p.IsSeatedEvent = val);
		}
		public EventModelMockBuilder WithHowYouHeardAboutEventOptions(String val)
		{ 
			return WithBuildStep(p => p.HowYouHeardAboutEventOptions = val);
		}
	}

	internal partial class EventSearchResultTicketDataMockBuilder : MockBuilder<EventSearchResultTicketDataMockBuilder, EventSearchResultTicketData>
	{	
		public EventSearchResultTicketDataMockBuilder WithCheapestTicket(Decimal? val)
		{ 
			return WithBuildStep(p => p.CheapestTicket = val);
		}
		public EventSearchResultTicketDataMockBuilder WithMostExpensiveTicket(Decimal? val)
		{ 
			return WithBuildStep(p => p.MostExpensiveTicket = val);
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
		public EventTicketMockBuilder WithIsActive(Boolean val)
		{ 
			return WithBuildStep(p => p.IsActive = val);
		}
		public EventTicketMockBuilder WithColourCode(String val)
		{ 
			return WithBuildStep(p => p.ColourCode = val);
		}
		public EventTicketMockBuilder WithEventTicketReservations(IList<EventTicketReservation> val)
		{ 
			return WithBuildStep(p => p.EventTicketReservations = val);
		}
		public EventTicketMockBuilder WithEventBookingTickets(IList<EventBookingTicket> val)
		{ 
			return WithBuildStep(p => p.EventBookingTickets = val);
		}
		public EventTicketMockBuilder WithEventTicketFields(IList<EventTicketField> val)
		{ 
			return WithBuildStep(p => p.EventTicketFields = val);
		}
		public EventTicketMockBuilder WithEventSeats(IList<EventSeat> val)
		{ 
			return WithBuildStep(p => p.EventSeats = val);
		}
		public EventTicketMockBuilder WithTicketImage(String val)
		{ 
			return WithBuildStep(p => p.TicketImage = val);
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
		public EventBookingTicketMockBuilder WithDiscountAmount(Decimal? val)
		{ 
			return WithBuildStep(p => p.DiscountAmount = val);
		}
		public EventBookingTicketMockBuilder WithDiscountPercent(Decimal? val)
		{ 
			return WithBuildStep(p => p.DiscountPercent = val);
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
		public EventBookingTicketMockBuilder WithTransactionFee(Decimal? val)
		{ 
			return WithBuildStep(p => p.TransactionFee = val);
		}
		public EventBookingTicketMockBuilder WithTotalPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalPrice = val);
		}
		public EventBookingTicketMockBuilder WithEventGroupId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventGroupId = val);
		}
		public EventBookingTicketMockBuilder WithEventGroup(EventGroup val)
		{ 
			return WithBuildStep(p => p.EventGroup = val);
		}
		public EventBookingTicketMockBuilder WithLastModifiedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.LastModifiedDate = val);
		}
		public EventBookingTicketMockBuilder WithLastModifiedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.LastModifiedDateUtc = val);
		}
		public EventBookingTicketMockBuilder WithLastModifiedBy(String val)
		{ 
			return WithBuildStep(p => p.LastModifiedBy = val);
		}
		public EventBookingTicketMockBuilder WithIsActive(Boolean val)
		{ 
			return WithBuildStep(p => p.IsActive = val);
		}
		public EventBookingTicketMockBuilder WithBarcodeImageDocumentId(Guid? val)
		{ 
			return WithBuildStep(p => p.BarcodeImageDocumentId = val);
		}
		public EventBookingTicketMockBuilder WithTicketDocumentId(Guid? val)
		{ 
			return WithBuildStep(p => p.TicketDocumentId = val);
		}
		public EventBookingTicketMockBuilder WithIsPublic(Boolean val)
		{ 
			return WithBuildStep(p => p.IsPublic = val);
		}
		public EventBookingTicketMockBuilder WithSeatNumber(String val)
		{ 
			return WithBuildStep(p => p.SeatNumber = val);
		}
		public EventBookingTicketMockBuilder WithTicketImage(String val)
		{ 
			return WithBuildStep(p => p.TicketImage = val);
		}
	}

	internal partial class EventBookingTicketFieldMockBuilder : MockBuilder<EventBookingTicketFieldMockBuilder, EventBookingTicketField>
	{	
		public EventBookingTicketFieldMockBuilder WithEventBookingTicketFieldId(Int64 val)
		{ 
			return WithBuildStep(p => p.EventBookingTicketFieldId = val);
		}
		public EventBookingTicketFieldMockBuilder WithEventBookingTicketId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventBookingTicketId = val);
		}
		public EventBookingTicketFieldMockBuilder WithFieldName(String val)
		{ 
			return WithBuildStep(p => p.FieldName = val);
		}
		public EventBookingTicketFieldMockBuilder WithFieldValue(String val)
		{ 
			return WithBuildStep(p => p.FieldValue = val);
		}
	}

	internal partial class EventBookingTicketValidationMockBuilder : MockBuilder<EventBookingTicketValidationMockBuilder, EventBookingTicketValidation>
	{	
		public EventBookingTicketValidationMockBuilder WithEventBookingTicketValidationId(Int64 val)
		{ 
			return WithBuildStep(p => p.EventBookingTicketValidationId = val);
		}
		public EventBookingTicketValidationMockBuilder WithEventBookingTicketId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventBookingTicketId = val);
		}
		public EventBookingTicketValidationMockBuilder WithValidationCount(Int32 val)
		{ 
			return WithBuildStep(p => p.ValidationCount = val);
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
		public EventBookingMockBuilder WithPromoCode(String val)
		{ 
			return WithBuildStep(p => p.PromoCode = val);
		}
		public EventBookingMockBuilder WithDiscountPercent(Decimal? val)
		{ 
			return WithBuildStep(p => p.DiscountPercent = val);
		}
		public EventBookingMockBuilder WithDiscountAmount(Decimal? val)
		{ 
			return WithBuildStep(p => p.DiscountAmount = val);
		}
		public EventBookingMockBuilder WithCreatedDateTime(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTime = val);
		}
		public EventBookingMockBuilder WithCreatedDateTimeUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTimeUtc = val);
		}
		public EventBookingMockBuilder WithHowYouHeardAboutEvent(String val)
		{ 
			return WithBuildStep(p => p.HowYouHeardAboutEvent = val);
		}
		public EventBookingMockBuilder WithStatusAsString(String val)
		{ 
			return WithBuildStep(p => p.StatusAsString = val);
		}
		public EventBookingMockBuilder WithPaymentMethodAsString(String val)
		{ 
			return WithBuildStep(p => p.PaymentMethodAsString = val);
		}
		public EventBookingMockBuilder WithCost(Decimal val)
		{ 
			return WithBuildStep(p => p.Cost = val);
		}
		public EventBookingMockBuilder WithTransactionFee(Decimal val)
		{ 
			return WithBuildStep(p => p.TransactionFee = val);
		}
		public EventBookingMockBuilder WithFeePercentage(Decimal? val)
		{ 
			return WithBuildStep(p => p.FeePercentage = val);
		}
		public EventBookingMockBuilder WithFeeCents(Decimal? val)
		{ 
			return WithBuildStep(p => p.FeeCents = val);
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
		public EventTicketReservationMockBuilder WithTransactionFee(Decimal? val)
		{ 
			return WithBuildStep(p => p.TransactionFee = val);
		}
		public EventTicketReservationMockBuilder WithEventGroupId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventGroupId = val);
		}
		public EventTicketReservationMockBuilder WithIsPublic(Boolean val)
		{ 
			return WithBuildStep(p => p.IsPublic = val);
		}
		public EventTicketReservationMockBuilder WithSeatNumber(String val)
		{ 
			return WithBuildStep(p => p.SeatNumber = val);
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
		public EventGuestDetailsMockBuilder WithTotalTicketPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalTicketPrice = val);
		}
		public EventGuestDetailsMockBuilder WithTicketPrice(Decimal? val)
		{ 
			return WithBuildStep(p => p.TicketPrice = val);
		}
		public EventGuestDetailsMockBuilder WithDateOfBooking(DateTime val)
		{ 
			return WithBuildStep(p => p.DateOfBooking = val);
		}
		public EventGuestDetailsMockBuilder WithDateOfBookingUtc(DateTime val)
		{ 
			return WithBuildStep(p => p.DateOfBookingUtc = val);
		}
		public EventGuestDetailsMockBuilder WithTicketId(Int32 val)
		{ 
			return WithBuildStep(p => p.TicketId = val);
		}
		public EventGuestDetailsMockBuilder WithGroupName(String val)
		{ 
			return WithBuildStep(p => p.GroupName = val);
		}
		public EventGuestDetailsMockBuilder WithIsPublic(Boolean val)
		{ 
			return WithBuildStep(p => p.IsPublic = val);
		}
		public EventGuestDetailsMockBuilder WithSeatNumber(String val)
		{ 
			return WithBuildStep(p => p.SeatNumber = val);
		}
		public EventGuestDetailsMockBuilder WithPromoCode(String val)
		{ 
			return WithBuildStep(p => p.PromoCode = val);
		}
	}

	internal partial class EventPaymentSummaryMockBuilder : MockBuilder<EventPaymentSummaryMockBuilder, EventPaymentSummary>
	{	
		public EventPaymentSummaryMockBuilder WithTotalTicketSalesAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.TotalTicketSalesAmount = val);
		}
		public EventPaymentSummaryMockBuilder WithEventOrganiserOwedAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.EventOrganiserOwedAmount = val);
		}
		public EventPaymentSummaryMockBuilder WithEventOrganiserFeesTotalFeesAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.EventOrganiserFeesTotalFeesAmount = val);
		}
	}

	internal partial class EventPaymentRequestMockBuilder : MockBuilder<EventPaymentRequestMockBuilder, EventPaymentRequest>
	{	
		public EventPaymentRequestMockBuilder WithEventPaymentRequestId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventPaymentRequestId = val);
		}
		public EventPaymentRequestMockBuilder WithEventId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventPaymentRequestMockBuilder WithRequestedAmount(Decimal val)
		{ 
			return WithBuildStep(p => p.RequestedAmount = val);
		}
		public EventPaymentRequestMockBuilder WithCreatedByUser(String val)
		{ 
			return WithBuildStep(p => p.CreatedByUser = val);
		}
		public EventPaymentRequestMockBuilder WithCreatedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public EventPaymentRequestMockBuilder WithCreatedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateUtc = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentMethod(PaymentType val)
		{ 
			return WithBuildStep(p => p.PaymentMethod = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentMethodAsString(String val)
		{ 
			return WithBuildStep(p => p.PaymentMethodAsString = val);
		}
		public EventPaymentRequestMockBuilder WithIsPaymentProcessed(Boolean? val)
		{ 
			return WithBuildStep(p => p.IsPaymentProcessed = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentProcessedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.PaymentProcessedDate = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentProcessedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.PaymentProcessedDateUtc = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentProcessedBy(String val)
		{ 
			return WithBuildStep(p => p.PaymentProcessedBy = val);
		}
		public EventPaymentRequestMockBuilder WithPaymentReference(String val)
		{ 
			return WithBuildStep(p => p.PaymentReference = val);
		}
	}

	internal partial class EventTicketFieldMockBuilder : MockBuilder<EventTicketFieldMockBuilder, EventTicketField>
	{	
		public EventTicketFieldMockBuilder WithEventTicketFieldId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventTicketFieldId = val);
		}
		public EventTicketFieldMockBuilder WithEventTicketId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventTicketFieldMockBuilder WithFieldName(String val)
		{ 
			return WithBuildStep(p => p.FieldName = val);
		}
		public EventTicketFieldMockBuilder WithIsRequired(Boolean val)
		{ 
			return WithBuildStep(p => p.IsRequired = val);
		}
	}

	internal partial class EventInvitationMockBuilder : MockBuilder<EventInvitationMockBuilder, EventInvitation>
	{	
		public EventInvitationMockBuilder WithEventInvitationId(Int64? val)
		{ 
			return WithBuildStep(p => p.EventInvitationId = val);
		}
		public EventInvitationMockBuilder WithUserNetworkId(Int32 val)
		{ 
			return WithBuildStep(p => p.UserNetworkId = val);
		}
		public EventInvitationMockBuilder WithEventId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventInvitationMockBuilder WithEventModel(EventModel val)
		{ 
			return WithBuildStep(p => p.EventModel = val);
		}
		public EventInvitationMockBuilder WithConfirmedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.ConfirmedDate = val);
		}
		public EventInvitationMockBuilder WithConfirmedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.ConfirmedDateUtc = val);
		}
		public EventInvitationMockBuilder WithCreatedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public EventInvitationMockBuilder WithCreatedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateUtc = val);
		}
	}

	internal partial class EventGroupMockBuilder : MockBuilder<EventGroupMockBuilder, EventGroup>
	{	
		public EventGroupMockBuilder WithEventGroupId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventGroupId = val);
		}
		public EventGroupMockBuilder WithEventId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventGroupMockBuilder WithGroupName(String val)
		{ 
			return WithBuildStep(p => p.GroupName = val);
		}
		public EventGroupMockBuilder WithMaxGuests(Int32? val)
		{ 
			return WithBuildStep(p => p.MaxGuests = val);
		}
		public EventGroupMockBuilder WithCreatedDateTime(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTime = val);
		}
		public EventGroupMockBuilder WithCreatedDateTimeUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateTimeUtc = val);
		}
		public EventGroupMockBuilder WithCreatedBy(String val)
		{ 
			return WithBuildStep(p => p.CreatedBy = val);
		}
		public EventGroupMockBuilder WithGuestCount(Int32 val)
		{ 
			return WithBuildStep(p => p.GuestCount = val);
		}
		public EventGroupMockBuilder WithAvailableToAllTickets(Boolean? val)
		{ 
			return WithBuildStep(p => p.AvailableToAllTickets = val);
		}
		public EventGroupMockBuilder WithIsDisabled(Boolean val)
		{ 
			return WithBuildStep(p => p.IsDisabled = val);
		}
	}

	internal partial class EventOrganiserMockBuilder : MockBuilder<EventOrganiserMockBuilder, EventOrganiser>
	{	
		public EventOrganiserMockBuilder WithEventOrganiserId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventOrganiserId = val);
		}
		public EventOrganiserMockBuilder WithEventId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventOrganiserMockBuilder WithEvent(EventModel val)
		{ 
			return WithBuildStep(p => p.Event = val);
		}
		public EventOrganiserMockBuilder WithUserId(String val)
		{ 
			return WithBuildStep(p => p.UserId = val);
		}
		public EventOrganiserMockBuilder WithEmail(String val)
		{ 
			return WithBuildStep(p => p.Email = val);
		}
		public EventOrganiserMockBuilder WithInviteToken(Guid? val)
		{ 
			return WithBuildStep(p => p.InviteToken = val);
		}
		public EventOrganiserMockBuilder WithLastModifiedBy(String val)
		{ 
			return WithBuildStep(p => p.LastModifiedBy = val);
		}
		public EventOrganiserMockBuilder WithLastModifiedDateUtc(DateTime val)
		{ 
			return WithBuildStep(p => p.LastModifiedDateUtc = val);
		}
		public EventOrganiserMockBuilder WithLastModifiedDate(DateTime val)
		{ 
			return WithBuildStep(p => p.LastModifiedDate = val);
		}
		public EventOrganiserMockBuilder WithIsActive(Boolean val)
		{ 
			return WithBuildStep(p => p.IsActive = val);
		}
		public EventOrganiserMockBuilder WithSubscribeToPurchaseNotifications(Boolean? val)
		{ 
			return WithBuildStep(p => p.SubscribeToPurchaseNotifications = val);
		}
		public EventOrganiserMockBuilder WithSubscribeToDailyNotifications(Boolean? val)
		{ 
			return WithBuildStep(p => p.SubscribeToDailyNotifications = val);
		}
	}

	internal partial class EventSeatMockBuilder : MockBuilder<EventSeatMockBuilder, EventSeat>
	{	
		public EventSeatMockBuilder WithEventSeatId(Int64 val)
		{ 
			return WithBuildStep(p => p.EventSeatId = val);
		}
		public EventSeatMockBuilder WithEventTicketId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventSeatMockBuilder WithSeatOrder(Int32 val)
		{ 
			return WithBuildStep(p => p.SeatOrder = val);
		}
		public EventSeatMockBuilder WithSeatNumber(String val)
		{ 
			return WithBuildStep(p => p.SeatNumber = val);
		}
		public EventSeatMockBuilder WithNotAvailableToPublic(Boolean? val)
		{ 
			return WithBuildStep(p => p.NotAvailableToPublic = val);
		}
		public EventSeatMockBuilder WithEventTicket(EventTicket val)
		{ 
			return WithBuildStep(p => p.EventTicket = val);
		}
		public EventSeatMockBuilder WithRowNumber(String val)
		{ 
			return WithBuildStep(p => p.RowNumber = val);
		}
		public EventSeatMockBuilder WithRowOrder(Int32 val)
		{ 
			return WithBuildStep(p => p.RowOrder = val);
		}
		public EventSeatMockBuilder WithIsBooked(Boolean val)
		{ 
			return WithBuildStep(p => p.IsBooked = val);
		}
	}

	internal partial class EventPromoCodeMockBuilder : MockBuilder<EventPromoCodeMockBuilder, EventPromoCode>
	{	
		public EventPromoCodeMockBuilder WithEventPromoCodeId(Int64 val)
		{ 
			return WithBuildStep(p => p.EventPromoCodeId = val);
		}
		public EventPromoCodeMockBuilder WithEventId(Int32 val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventPromoCodeMockBuilder WithEvent(EventModel val)
		{ 
			return WithBuildStep(p => p.Event = val);
		}
		public EventPromoCodeMockBuilder WithPromoCode(String val)
		{ 
			return WithBuildStep(p => p.PromoCode = val);
		}
		public EventPromoCodeMockBuilder WithDiscountPercent(Decimal? val)
		{ 
			return WithBuildStep(p => p.DiscountPercent = val);
		}
		public EventPromoCodeMockBuilder WithIsDisabled(Boolean? val)
		{ 
			return WithBuildStep(p => p.IsDisabled = val);
		}
		public EventPromoCodeMockBuilder WithCreatedDate(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDate = val);
		}
		public EventPromoCodeMockBuilder WithCreatedDateUtc(DateTime? val)
		{ 
			return WithBuildStep(p => p.CreatedDateUtc = val);
		}
	}
}


