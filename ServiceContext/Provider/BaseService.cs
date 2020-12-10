using ServiceContext.Provider.Interface;
using ServiceContext.Provider.SQL.Interface;

namespace ServiceContext.Provider
{
    public class BaseService : IService
    {
        public BaseService(IRepositoryUnitOfWork unitOfWork)
        {

        }

    }
}
