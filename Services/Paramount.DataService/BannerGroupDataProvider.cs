using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Paramount.ApplicationBlock.Data;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.DataService.LinqObjects;

namespace Paramount.DataService
{
    public class BannerGroupDataProvider : IDisposable
    {
        private BannerDatabaseModelDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";
       

        public BannerGroupDataProvider (string clientCode)
        {
            _context = new BannerDatabaseModelDataContext(ConnectionString);
            ClientCode = clientCode ;
        }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public IEnumerable<BannerGroupEntity> AsQueryable()
        {
            return AsRawQueryable().ToList().Select(a => a.Convert());
        }

        public IQueryable<BannerGroup> AsRawQueryable()
        {
            return _context.BannerGroups.Where(a => a.ClientCode == ClientCode);
        }

        public IEnumerable<BannerGroupEntity> GetAll()
        {
           return  AsQueryable();
        }

        public IEnumerable<BannerGroupEntity> Find(Expression<Func<BannerGroup, bool>> where)
        {
            return  AsRawQueryable().Where(where).ToList().Select(a=>a.Convert());
        }

        public BannerGroupEntity Single(Expression<Func<BannerGroup, bool>> where)
        {
            return AsRawQueryable().SingleOrDefault(where).Convert();
        }

        public BannerGroupEntity First(Expression<Func<BannerGroup, bool>> where)
        {
            return AsRawQueryable().FirstOrDefault(where).Convert();
        }

        public void Delete(BannerGroupEntity entity)
        {
            var t = GetRawById(entity.GroupId);
            if (t.Banners.Count == 0)
            {
                _context.BannerGroups.DeleteOnSubmit(t);
            }
            else
            {
                t.IsActive = false;
            }
        }

        public List<BannerFileType> GetAllFileTypes()
        {
            return _context.BannerFileTypes.ToList();
        }

        public void Add(BannerGroupEntity entity)
        {
            var group = entity.Convert();
            group.ClientCode = ClientCode;
            _context.BannerGroups.InsertOnSubmit(group);
        }

        public void Update(BannerGroupEntity entity)
        {
            var t = GetRawById(entity.GroupId);
            t.AcceptedFileType = entity.AcceptedFileType;
            t.Title = entity.Name;
            t.Height = entity.Height;
            t.Width = entity.Width;
            t.IsTimerEnabled = entity.UseTimer;
            t.IsActive = entity.IsActive;
        }

        public string ClientCode { get; private set; }

        private BannerGroup GetRawById(Guid groupId)
        {
            return _context.BannerGroups.Single(a => a.BannerGroupId.Equals(groupId));
        }

        public void Commit()
        {
            _context.SubmitChanges();
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