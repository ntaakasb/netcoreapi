using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Asynchronously commits all changes
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// Synchronously commits all changes
        /// </summary>
        void Commit();
    }
}
