using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Applications;

namespace Application.Interfaces
{
    public interface IInternshipApplicationService
    {
        Task ApplyAsync(CreateInternshipApplicationDto dto);
        Task<IEnumerable<InternshipApplicationDto>> GetAllAsync();
        Task<IEnumerable<InternshipApplicationDto>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<InternshipApplicationDto>> GetByInternshipIdAsync(Guid internshipId);
        Task UpdateStatusAsync(UpdateInternshipApplicationStatusDto dto);

    }
}
