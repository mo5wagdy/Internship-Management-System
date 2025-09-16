using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Applications;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class InternshipApplicationService : IInternshipApplicationService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public InternshipApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ApplyAsync(CreateInternshipApplicationDto dto)
        {
            var application = new InternshipApplication
            {
                StudentId = dto.StudentId,
                InternshipId = dto.InternshipId,
                Status = InternshipApplicationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InternshipApplications.AddAsync(application);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<InternshipApplicationDto>> GetAllAsync()
        {
            var applications = await _unitOfWork.InternshipApplications.GetAllAsync();
            return applications.Select(a => new InternshipApplicationDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                InternshipId = a.InternshipId,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt
            });
        }

        public async Task<IEnumerable<InternshipApplicationDto>> GetByStudentIdAsync(Guid studentId)
        {
            var applications = await _unitOfWork.InternshipApplications.GetByStudentIdAsync(studentId);

            return applications.Select(a => new InternshipApplicationDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                InternshipId = a.InternshipId,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt
            });
        }

        public async Task<IEnumerable<InternshipApplicationDto>> GetByInternshipIdAsync(Guid internshipId)
        {
            var applications = await _unitOfWork.InternshipApplications.GetByInternshipIdAsync(internshipId);

            return applications.Select(a => new InternshipApplicationDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                InternshipId= a.InternshipId,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt
            });
        }

        public async Task UpdateStatusAsync(UpdateInternshipApplicationStatusDto dto)
        {
            var application = await _unitOfWork.InternshipApplications.GetByIdAsync(dto.ApplicationId);

            if (application == null)
                throw new Exception("Application Not Found");
            application.Status = Enum.Parse<InternshipApplicationStatus>(dto.Status, true);

            _unitOfWork.InternshipApplications.Update(application);
            await _unitOfWork.CompleteAsync();
        }
    }
}

