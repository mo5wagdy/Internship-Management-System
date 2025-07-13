using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInternshipApplicationRepository
    {
        Task<InternshipApplication?> GetByIdAsync(Guid id);
        Task<IEnumerable<InternshipApplication>> GetAllAsync();
        Task<IEnumerable<InternshipApplication>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<InternshipApplication>> GetByInternshipIdAsync(Guid internshipId);
        Task AddAsync(InternshipApplication application);
        void Update(InternshipApplication application);
        void Delete(InternshipApplication application);
    }
}
