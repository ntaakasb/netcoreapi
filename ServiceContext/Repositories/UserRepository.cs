using ModelContext.DatabaseModel;
using ServiceContext.Interface;
using ServiceContext.Provider;
using ServiceContext.Provider.SQL;

namespace ServiceContext
{
    public class UserRepository : BaseRepository<SYS_USER>, IUserRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public UserRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        #region Override GenericRepository

        #endregion

        #region Overload GenericRepository



        #endregion

    }
}
