using DatabaseOracleContext.Interface;
using System.Threading.Tasks;

namespace DatabaseOracleContext
{
    public class BaseUnitOfWork: IUnitOfWork
    {
        private readonly BaseDbContext _dbContext;

        public BaseUnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
