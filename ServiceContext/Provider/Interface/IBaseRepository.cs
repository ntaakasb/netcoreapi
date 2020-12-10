using DatabaseContext.Interface;
using ModelContext.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ServiceContext.Provider.Interface
{
    public interface IBaseRepository<T>: IRepository<T> where T : class
    {
        IAttributeActionResult CallStoreProcedureAction(string query, params object[] parameters);
        IEnumerable<IAttributeActionResult> ExecStoreProcedureWithReturnResultExtend(string query, params object[] parameters);
        IAttributeActionResult CallStoreProcedureAction(string query, SqlParameter[] parameters);

    }
}
