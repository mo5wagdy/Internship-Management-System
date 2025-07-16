using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Internships;

namespace Application.Interfaces
{
    public interface IInternshipService
    {
        Task CreateAsync(CreateInternshipDto dto);
        Task<IEnumerable<InternshipDto>> GetAllAsync();
        Task<IEnumerable<InternshipDto>> GetByCompanyIdAsync(Guid companyId);
        Task UpdateAsync(UpdateInternshipDto dto);
        Task DeleteAsync(Guid Id);
    }
}
