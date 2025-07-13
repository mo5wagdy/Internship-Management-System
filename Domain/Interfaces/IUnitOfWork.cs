using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IInternshipRepository Internships { get; }
        IInternshipApplicationRepository InternshipApplications { get; }
        Task<int> SaveChangesAsync();
    }
}
