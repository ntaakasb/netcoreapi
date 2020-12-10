using DatabaseContext;
using ServiceContext.Provider.SQL.Interface;

namespace ServiceContext.Provider.SQL
{
    public class RepositoryUnitOfWork : BaseUnitOfWork, IRepositoryUnitOfWork
    {
        private readonly RepositoryDbContext _dbContext;

        #region Repositories

        //public IRepository<User> UserRepository = new GenericRepository<User>(_dbContext);

        #endregion

        public RepositoryUnitOfWork(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
