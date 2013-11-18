namespace Paramount.Betterclassifieds.DataAccess.Classifieds
{
    using Domain;
    using Mapping;
    using System.Data.Entity;

    public class BetterclassifiedsDbContext : DbContext
    {
        static BetterclassifiedsDbContext()
        {
            Database.SetInitializer<BetterclassifiedsDbContext>(null);
        }

        public BetterclassifiedsDbContext()
            : base("Name=BetterclassifiedsDbContext")
        {
            //this.Configuration.ProxyCreationEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdBooking> AdBookings { get; set; }
        public DbSet<AdBookingExtension> AdBookingExtensions { get; set; }
        public DbSet<AdDesign> AdDesigns { get; set; }
        public DbSet<AdGraphic> AdGraphics { get; set; }
        public DbSet<AdType> AdTypes { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<BaseRate> BaseRates { get; set; }
        public DbSet<BookEntry> BookEntries { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<EnquiryDocument> EnquiryDocuments { get; set; }
        public DbSet<EnquiryType> EnquiryTypes { get; set; }
        public DbSet<LineAd> LineAds { get; set; }
        public DbSet<LineAdColourCode> LineAdColourCodes { get; set; }
        public DbSet<LineAdTheme> LineAdThemes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationArea> LocationAreas { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<NonPublicationDate> NonPublicationDates { get; set; }
        public DbSet<OnlineAd> OnlineAds { get; set; }
        public DbSet<OnlineAdEnquiry> OnlineAdEnquiries { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationAdType> PublicationAdTypes { get; set; }
        public DbSet<PublicationCategory> PublicationCategories { get; set; }
        public DbSet<PublicationRate> PublicationRates { get; set; }
        public DbSet<PublicationType> PublicationTypes { get; set; }
        public DbSet<Ratecard> Ratecards { get; set; }
        public DbSet<SupportEnquiry> SupportEnquiries { get; set; }
        public DbSet<TempBookingRecord> TempBookingRecords { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TutorAd> TutorAds { get; set; }
        public DbSet<WebAdSpace> WebAdSpaces { get; set; }
        public DbSet<WebAdSpaceSetting> WebAdSpaceSettings { get; set; }
        public DbSet<WebContent> WebContents { get; set; }
        public DbSet<RssFeed> RssFeeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdMap());
            modelBuilder.Configurations.Add(new AdBookingMap());
            modelBuilder.Configurations.Add(new AdBookingExtensionMap());
            modelBuilder.Configurations.Add(new AdDesignMap());
            modelBuilder.Configurations.Add(new AdGraphicMap());
            modelBuilder.Configurations.Add(new AdTypeMap());
            modelBuilder.Configurations.Add(new AppSettingMap());
            modelBuilder.Configurations.Add(new BaseRateMap());
            modelBuilder.Configurations.Add(new BookEntryMap());
            modelBuilder.Configurations.Add(new EditionMap());
            modelBuilder.Configurations.Add(new EnquiryDocumentMap());
            modelBuilder.Configurations.Add(new EnquiryTypeMap());
            modelBuilder.Configurations.Add(new LineAdMap());
            modelBuilder.Configurations.Add(new LineAdColourCodeMap());
            modelBuilder.Configurations.Add(new LineAdThemeMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new LocationAreaMap());
            modelBuilder.Configurations.Add(new LookupMap());
            modelBuilder.Configurations.Add(new MainCategoryMap());
            modelBuilder.Configurations.Add(new NonPublicationDateMap());
            modelBuilder.Configurations.Add(new OnlineAdMap());
            modelBuilder.Configurations.Add(new OnlineAdEnquiryMap());
            modelBuilder.Configurations.Add(new PublicationMap());
            modelBuilder.Configurations.Add(new PublicationAdTypeMap());
            modelBuilder.Configurations.Add(new PublicationCategoryMap());
            modelBuilder.Configurations.Add(new PublicationRateMap());
            modelBuilder.Configurations.Add(new PublicationTypeMap());
            modelBuilder.Configurations.Add(new RatecardMap());
            modelBuilder.Configurations.Add(new SupportEnquiryMap());
            modelBuilder.Configurations.Add(new TempBookingRecordMap());
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new TutorAdMap());
            modelBuilder.Configurations.Add(new WebAdSpaceMap());
            modelBuilder.Configurations.Add(new WebAdSpaceSettingMap());
            modelBuilder.Configurations.Add(new WebContentMap());
            modelBuilder.Configurations.Add(new RssFeedMap());
        }
    }
}
