using ModelContext;
using ModelContext.DatabaseModel.Interface;
using ModelContext.Interface;
using ServiceContext.Provider.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContext.Interface
{
    public interface IUserService: IService
    {
        IEnumerable<ISYS_USER> GetList(ref string Err, ref long totalRows, long PageIndex = 0, long PageSize = 10, string OrderBy = " ID ASC ", string FilterQuery = " 1 = 1 ");
    }
}
