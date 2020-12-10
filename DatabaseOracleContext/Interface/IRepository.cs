using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseOracleContext.Interface
{
    public interface IRepository<T> where T : class
    {
        T Add(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        void Dispose();
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
        ICollection<K> ExecWithStoreProcedure<K>(string query, OracleParameter[] parameters) where K : class;
        ICollection<K> ExecWithStoreProcedure<K>(string query, params object[] parameters) where K : class;
        Task<ICollection<T>> ExecWithStoreProcedureAsync(string query, OracleParameter[] parameters);
        ICollection<T> ExecWithStoreProcedure(string query, OracleParameter[] parameters);
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
        int ExecWithStoreProcedureCommand(string query, params object[] parameters);
        Task<int> ExecWithStoreProcedureCommandAsync(string query, params object[] parameters);
        ICollection<K> CallStoreProcedureObject<K>(string query, params object[] parameters) where K : class;
    }
}
