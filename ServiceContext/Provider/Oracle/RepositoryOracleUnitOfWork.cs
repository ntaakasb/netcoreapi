using DatabaseOracleContext;
using ServiceContext.Provider.Interface.Oracle;

namespace ServiceContext.Provider.Oracle
{
    public class RepositoryOracleUnitOfWork : BaseUnitOfWork, IRepositoryOracleUnitOfWork
    {
        private readonly RepositoryDbOracleContext _dbContext;

        #region Repositories

        //public IRepository<User> UserRepository = new GenericRepository<User>(_dbContext);

        #endregion

        public RepositoryOracleUnitOfWork(RepositoryDbOracleContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
