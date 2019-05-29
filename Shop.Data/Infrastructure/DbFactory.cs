using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ShopDBContext dbContext;

        public ShopDBContext Init()
        {
            return dbContext ?? (dbContext = new ShopDBContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
