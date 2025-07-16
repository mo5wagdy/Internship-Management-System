using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInternshipApplicationRepository : IGenericRepository<InternshipApplication>
    {
        Task<IEnumerable<InternshipApplication>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<InternshipApplication>> GetByInternshipIdAsync(Guid internshipId);
    }
}
