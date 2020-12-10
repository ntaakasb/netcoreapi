using ModelContext.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext.Interface
{
    public interface IRepository<T> where T : class
    {
        T Add(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        void Dispose();
        Dictionary<string, object> ExecuteMultipleList(string connectionString, string query, List<Type> ObjectParse, params object[] parameters);
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        T Update(T t, object key);
        Task<ICollection<T>> ExecWithStoreProcedureAsync(string query, params object[] parameters);
        ICollection<T> ExecWithStoreProcedure(string query, params object[] parameters);
        ICollection<K> ExecWithStoreProcedure<K>(string query, SqlParameter[] parameters) where K : class;
        ICollection<K> ExecWithStoreProcedure<K>(string query, params object[] parameters) where K : class;
        Task<ICollection<T>> ExecWithStoreProcedureAsync(string query, SqlParameter[] parameters);
        ICollection<T> ExecWithStoreProcedure(string query, SqlParameter[] parameters);
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
        int ExecWithStoreProcedureCommand(string query, params object[] parameters);
        Task<int> ExecWithStoreProcedureCommandAsync(string query, params object[] parameters);
        ICollection<K> CallStoreProcedureObject<K>(string query, params object[] parameters) where K : class;
    }
}
