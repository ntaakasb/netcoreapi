using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelContext;
using ModelContext.Interface;
using ServiceContext.Provider.Interface;
using ServiceContext.Provider.SQL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ServiceContext.Provider
{
    public class BaseRepository<T> : GenericRepository<T>, IBaseRepository<T> where T : class
    {
        private readonly RepositoryDbContext _dbContext;
        public BaseRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }
        public IEnumerable<IAttributeActionResult> ExecStoreProcedureWithReturnResultExtend(string query, params object[] parameters)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        pamameterString += " @p" + i.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = query + pamameterString;
            var result = _dbContext.Set<AttributeActionResult>().FromSql(fullquery, parameters);
            return result;
        }

        public IAttributeActionResult CallStoreProcedureAction(string query, params object[] parameters)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        pamameterString += " @p" + i.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = query + pamameterString;
            var result = _dbContext.Set<IAttributeActionResult>().FromSql(fullquery, parameters).ToList();
            if (result != null)
            {
                return result.FirstOrDefault();
            }
            else
            {
                return new AttributeActionResult()
                {
                    Successful = false
                };
            }
        }

        public IAttributeActionResult CallStoreProcedureAction(string query, SqlParameter[] parameters)
        {
            var queryString = "{0} {1}";
            string paramsList = string.Empty;
            foreach (var param in parameters)
            {
                paramsList += "@" + param.ParameterName + (param.Direction != ParameterDirection.Output ? "," : "OUPUT,");
            }

            string fullquery = string.Format(queryString, query, paramsList.Substring(0, paramsList.Length - 1));
            var result = _dbContext.Set<AttributeActionResult>().FromSql(fullquery, parameters).ToList();
            if (result != null)
            {
                return result.FirstOrDefault();
            }
            else
            {
                return new AttributeActionResult()
                {
                    Successful = false,
                    ErrorMessage = "result null"
                };
            }
        }

    }
}
