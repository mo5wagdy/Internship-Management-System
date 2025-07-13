using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInternshipRepository
    {
        Task<Internship?> GetByIdAsync(Guid id);
        Task<IEnumerable<Internship>> GetAllAsync();
        Task<IEnumerable<Internship>> GetByCompanyIdAsync(Guid companyId);
        Task AddAsync(Internship internship);
        void Update(Internship internship);
        void Delete(Internship internship);
    }
}
