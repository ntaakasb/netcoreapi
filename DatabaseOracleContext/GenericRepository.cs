using DatabaseOracleContext.Interface;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseOracleContext
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly BaseDbContext _dbContext;
        private DbSet<T> _dbSet => _dbContext.Set<T>();
        public GenericRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {

            _dbContext.Set<T>().Add(t);
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _dbContext.Set<T>().Find(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public ICollection<T> ExecWithStoreProcedure(string query, OracleParameter[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";

            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    paramsList += " :" + param.ParameterName + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return _dbContext.Set<T>().FromSql(fullquery, parameters).ToList();
        }

        private List<object> ConvertToListObject(DataTable dt, Type t)
        {
            List<object> resultList = new List<object>();
            //Type t = typeof(K);
            PropertyDescriptorCollection curProp = TypeDescriptor.GetProperties(t);
            foreach (DataRow row in dt.Rows)
            {
                var objConvert = Activator.CreateInstance(t);
                foreach (PropertyDescriptor prop in curProp)
                {
                    prop.SetValue(objConvert, row[prop.Name.ToUpper()]);
                }
                resultList.Add(objConvert);
            }
            return resultList;
        }

        public ICollection<K> ExecWithStoreProcedure<K>(string query, OracleParameter[] parameters) where K : class
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    paramsList += " :" + param.ParameterName + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return _dbContext.Set<K>().FromSql(fullquery, parameters).ToList();
        }

        public async Task<ICollection<T>> ExecWithStoreProcedureAsync(string query, OracleParameter[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    paramsList += " :" + param.ParameterName + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return await _dbContext.Set<T>().FromSql(string.Format(queryString, query, paramsList.Remove(paramsList.Length - 1)), parameters)?.ToListAsync();
        }

        public async Task<ICollection<T>> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return await _dbContext.Set<T>().FromSql(fullquery, parameters).ToListAsync();
        }

        public ICollection<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return _dbContext.Set<T>().FromSql(fullquery, parameters).ToList();
        }

        public ICollection<K> ExecWithStoreProcedure<K>(string query, params object[] parameters) where K : class
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return _dbContext.Set<K>().FromSql(fullquery, parameters).ToList();
        }

        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            await _dbContext.Database.ExecuteSqlCommandAsync(fullquery, parameters);
        }

        public int ExecWithStoreProcedureCommand(string query, params object[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return _dbContext.Database.ExecuteSqlCommand(fullquery, parameters);
        }


        public async Task<int> ExecWithStoreProcedureCommandAsync(string query, params object[] parameters)
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            return await _dbContext.Database.ExecuteSqlCommandAsync(fullquery, parameters);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public ICollection<K> CallStoreProcedureObject<K>(string query, params object[] parameters) where K : class
        {
            var queryString = "BEGIN {0} ({1}); END;";
            var paramsList = string.Empty;
            string paramstring = string.Empty;
            if (parameters != null)
            {
                int i = 0;
                foreach (var param in parameters)
                {
                    i++;
                    paramsList += " :p" + i.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(paramsList))
                paramstring = paramsList.Substring(0, paramsList.Length - 1);
            string fullquery = String.Format(queryString, query, paramstring);
            var result = _dbContext.Set<K>().FromSql(fullquery, parameters)?.ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
