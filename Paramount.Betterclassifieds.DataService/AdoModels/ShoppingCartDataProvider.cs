using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Common.DataTransferObjects.Billing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.DataService
{
    public class ShoppingCartDataProvider : IDisposable
    {
        private BillingDatabaseModelDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";

        public ShoppingCartDataProvider(string clientCode)
        {
            _context = new BillingDatabaseModelDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public string ClientCode { get; private set; }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public IQueryable<ShoppingCart> AsRawQueryable()
        {
            return _context.ShoppingCarts.Where(a => a.ClientCode == ClientCode);
        }

        public IQueryable<ShoppingCartItem> ItemsAsRawQueryable(Guid shoppingCartId)
        {
            //return _context.ShoppingCartItems.Join().Where(a => a.ClientCode == ClientCode & a.ShoppingCartId == shoppingCartId);
            return from item in _context.ShoppingCartItems
                   join cart in _context.ShoppingCarts.Where(a => a.ClientCode == ClientCode && a.ShoppingCartId == shoppingCartId) on new { item.ShoppingCartId } equals new { cart.ShoppingCartId }
                   select item;
        }

        public IEnumerable<ShoppingCartEntity> AsQueryable()
        {
            return AsRawQueryable().ToList().Select(a => a.Convert());
        }

        public ShoppingCartEntity GetShoppingCart(Guid shoppingCartId)
        {
            return AsRawQueryable().FirstOrDefault(item=>item.ShoppingCartId == shoppingCartId).Convert();
        }

        public IEnumerable<ShoppingCartItemEntity> ItemsAsQueryable(Guid shoppingCartId)
        {
            return ItemsAsRawQueryable(shoppingCartId).ToList().Select(a => a.Convert());
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

        public  Guid Create(ShoppingCartEntity shoppingCartEntity)
        {
            var id = Guid.NewGuid();
            shoppingCartEntity.ShoppingCartId = id;
            shoppingCartEntity.ClientCode = ClientCode;
            _context.ShoppingCarts.InsertOnSubmit(shoppingCartEntity.Convert());
            Commit();
            return id;
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }

        public Guid AddItem(ShoppingCartItemEntity item)
        {
            var id = Guid.NewGuid();
            item.ShoppingCartItemId = id;
            _context.ShoppingCartItems.InsertOnSubmit(item.Convert());
            Commit();
            return id;
        }

     
    }
}
