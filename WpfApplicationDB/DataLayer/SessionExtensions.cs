using Models;
using NHibernate;
using System.Collections.Generic;

namespace DataLayer
{
    public static class SessionExtensions
    {
        public static IEnumerable<Store> GetStores(this ISession session)
        {
            //var productTableName = NHDataContext.GetTableName<Product>();
            //var stores = session.CreateCriteria(typeof(Store))
            //    .SetFetchMode(productTableName, FetchMode.Eager) 
            //    .SetFetchMode("StoreProduct", FetchMode.Eager)
            //    .List<Store>();
            //return stores;

            var stores = session.QueryOver<Store>().Future();
            var storesWithPersons = session.QueryOver<Store>()
                    .Fetch(s => s.Staff).Eager
                    .Future();
            var storesWithProducts = session.QueryOver<Store>()
                    .Fetch(s => s.Products).Eager
                    .Future();
            return stores;
        }
    }
}
