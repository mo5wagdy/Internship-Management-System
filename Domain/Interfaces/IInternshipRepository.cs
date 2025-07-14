using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInternshipRepository : IGenericRepository<Internship>
    {
        Task<IEnumerable<Internship>> GetByCompanyIdAsync(Guid companyId);
        Task<IEnumerable<Internship>> SearchByTitleAsync(string keyword);
    }
}
