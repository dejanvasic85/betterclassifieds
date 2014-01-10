using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Paramount.Betterclassifieds.DataService
{
    public class BannerDataProvider: IDisposable
    {
        private BannerDatabaseModelDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";

        public BannerDataProvider(string clientCode)
        {
            _context = new BannerDatabaseModelDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public IQueryable<Banner> AsRawQueryable()
        {
            return _context.Banners.Where(a => a.ClientCode == ClientCode);
        }
        public IEnumerable<BannerEntity> AsQueryable()
        {
            return AsRawQueryable().ToList().Select(a => a.Convert());
        }

        public BannerEntity GetNextBanner(Guid groupId, Collection<CodeDescription> attributes)
        {
            var table = new DataTable("attributes");
            var nameCol = new DataColumn("Name", typeof(string));
            var valueCol = new DataColumn("Value", typeof(string));
            table.Columns.AddRange(new[] {nameCol, valueCol});

            var parameters = new Collection<Parameter>();

            foreach (var item in attributes)
            {
                var row = table.NewRow();
                row.SetField("Name", item.Description);
                row.SetField("Value", item.Code);
                table.Rows.Add(row);
            }

            //var attributes
            parameters.Add(new Parameter(Proc.GetNextBanner.Params.Attributes, table));
            parameters.Add(new Parameter(Proc.GetNextBanner.Params.ClientCode, ClientCode, StringType.VarChar));
            parameters.Add(new Parameter(Proc.GetNextBanner.Params.BannerGroupId, groupId));
            
            var database = new DatabaseProxy(Proc.GetNextBanner.Name,_context.Connection.ConnectionString,parameters);
            var ds = database.ExecuteQuery().Tables[0];

            //var banners = new Collection<BannerEntity>();
            //foreach (DataRow item in ds.Rows)
            //{
            if (ds.Rows.Count == 0)
            {
                return null;
            }
                var row1 = new BannerRow(ds.Rows[0]);
                //banners.Add(new BannerEntity {BannerId = row.BannerId, Title = row.Title, ImageId = row.ImageId});
            //}
            return new BannerEntity
                       {
                           BannerId = row1.BannerId, 
                           Title = row1.Title, 
                           ImageId = row1.ImageId,
                           EndDate = row1.EndDate,
                           ClickCount = row1.ClickCount,
                           ClientCode = row1.ClientCode, 
                           CreatedBy =  row1.CreatedBy,
                           RequestCount = row1.RequestCount,
                           NavigateUrl = row1.NavigateUrl,
                           GroupId = row1.GroupId,
                           IsDeleted = row1.IsDeleted,
                           StartDate = row1.StartDate,

                       };
           
        }

        public IEnumerable<BannerEntity> GetAll()
        {
            return AsQueryable().AsEnumerable();
        }

        public IEnumerable<BannerEntity> Find(Expression<Func<Banner, bool>> where)
        {
            return AsRawQueryable().Where(where).ToList().Select( a=>a.Convert() );
        }

        public BannerEntity Single(Expression<Func<Banner, bool>> where)
        {
            return AsRawQueryable().SingleOrDefault(where).Convert();
        }

        public BannerEntity First(Expression<Func<Banner, bool>> where)
        {
            return AsRawQueryable().FirstOrDefault(where).Convert();
        }

        public void Delete(BannerEntity entity)
        {
            var t = GetRawById(entity.BannerId);

            if (t.StartDateTime > DateTime.Today)
            {
                _context.Banners.DeleteOnSubmit(t);
            }
            else
            {
                if (t.EndDateTime > DateTime.Today)
                {
                    t.EndDateTime = Yesterday;
                }
            }
        }

        private static DateTime Yesterday
        {
           get
            {
                return  DateTime.Today.AddSeconds( -1);
            }
        }

        public void Add(BannerEntity entity)
        {
            var banner = entity.Convert();
            banner.CreatedDate = DateTime.Now;
            banner.ModifiedDate = DateTime.Now;
            _context.Banners.InsertOnSubmit(banner);
        }

        public void Update(BannerEntity entity)
        {
            throw new NotImplementedException();
        }

        public void ReBookBanner(BannerRebookDetailEntity entity)
        {
            var raw = GetRawById(entity.BannerId);
            raw.StartDateTime = entity.StartDateTime;
            raw.EndDateTime = entity.EndDateTime;
            Update(entity);
        }

        public void Update(BannerModifyEntity entity)
        {
            var raw = GetRawById(entity.BannerId);
            raw.DocumentID = entity.ImageId;
            raw.Title = entity.Title;
            raw.NavigateUrl = entity.NavigateUrl;
            raw.BannerReferences.Clear();
            raw.ModifiedDate = DateTime.Now;
            raw.BannerReferences.AddRange(entity.Attributes.Select( a=>a.GetAttributes()));
        }

        public void UpdateBannerClickCount(Guid bannerId)
        {
            var raw = GetRawById(bannerId);
            raw.ClickCount += 1;
        }

        public void UpdateBannerRequestCount(Guid bannerId)
        {
            var raw = GetRawById(bannerId);
            raw.RequestCount += 1;
        }

        public string ClientCode { get; private set; }

        public void Commit()
        {
            _context.SubmitChanges();
        }

        private Banner GetRawById(Guid bannerId)
        {
            return _context.Banners.Single(a => a.BannerId.Equals(bannerId));
        }

        public void AddAudit( BannerAuditEntity audit)
        {
            _context.BannerAudits.InsertOnSubmit(audit.Convert());
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}