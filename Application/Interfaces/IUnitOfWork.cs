using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IUserRepository Users { get; }
        IInternshipRepository Internships { get; }
        IInternshipApplicationRepository InternshipApplications { get; }
        Task<int> CompleteAsync();
    }
}
