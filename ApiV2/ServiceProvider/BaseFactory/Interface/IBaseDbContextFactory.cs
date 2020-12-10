using ServiceContext.Provider;
using ServiceContext.Provider.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiV2.ServiceProvider.BaseFactory.Interface
{
    public interface IBaseDbContextFactory
    {
        RepositoryDbContext GetInstance();
    }
}