using ModelContext.DatabaseModel.Interface;
using ServiceContext.Interface;
using ServiceContext.Provider;
using ServiceContext.Provider.SQL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ServiceContext
{
    public class UserService : BaseService, IUserService
    {
        #region Store Procedure

        private readonly string Store_Search = "dbo.SYS_USER_Search";

        #endregion

        #region Init
        
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IUserRepository _Repository;
        public UserService(IRepositoryUnitOfWork unitOfWork, IUserRepository Repository) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
        }

        #endregion

        public IEnumerable<ISYS_USER> GetList(ref string Err,ref long totalRows, long PageIndex = 0, long PageSize = 10, string OrderBy = " ID ASC ", string FilterQuery = " 1 = 1 ")
        {
            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = " ID ASC ";
            if (string.IsNullOrEmpty(FilterQuery))
                FilterQuery = " 1 = 1 ";
            try
            {
                totalRows = 0;
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ParameterName = "PageIndex", DbType = DbType.Int64, Value = PageIndex},
                    new SqlParameter{ParameterName = "PageSize", DbType = DbType.Int64, Value = PageSize},
                    new SqlParameter{ParameterName = "OrderBy", DbType = DbType.String, Value = OrderBy},
                    new SqlParameter{ParameterName = "Filter", DbType = DbType.String, Value = FilterQuery},
                    new SqlParameter{ParameterName = "TotalRows", DbType = DbType.Int64, Direction = ParameterDirection.Output},
                };
                var result = _Repository.ExecWithStoreProcedure(Store_Search, prms);
                totalRows = Convert.ToInt32(prms.FirstOrDefault(x => x.ParameterName == "TotalRows").Value);
                if (result != null)
                {
                    if (result.Count() > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Err = ex.ToString();
                return null;
            }
        }

    }
}
