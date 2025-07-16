using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Internships;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InternshipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateInternshipDto dto)
        {
            var internship = new Internship()
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CompanyId = dto.CompanyId,
                IsAvailable = true
            };

            await _unitOfWork.Internships.AddAsync(internship);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<InternshipDto>> GetAllAsync()
        {
            var internships = await _unitOfWork.Internships.GetAllAsync();

            return internships.Select(i => new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                IsAvailable = i.IsAvailable,
                CompanyId = i.CompanyId,
            });
        }

        public async Task<IEnumerable<InternshipDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var internship = await _unitOfWork.Internships.GetByCompanyIdAsync(companyId);

            return internship.Select(i => new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                IsAvailable = i.IsAvailable,
                CompanyId = i.CompanyId,
            });
        }

        public async Task UpdateAsync(UpdateInternshipDto dto)
        {
            var internship = await _unitOfWork.Internships.GetByIdAsync(dto.Id);

            if (internship == null)
                throw new Exception("Internship Not Found");
            
            internship.Title = dto.Title;
            internship.Description = dto.Description;
            internship.StartDate = dto.StartDate;
            internship.EndDate = dto.EndDate;
            internship.IsAvailable = dto.IsAvailable;

            _unitOfWork.Internships.Update(internship);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var internship = await _unitOfWork.Internships.GetByIdAsync(id);

            if (internship == null)
                throw new Exception("Internship Not Found");

            _unitOfWork.Internships.Delete(internship);
            await _unitOfWork.CompleteAsync();
        }

    }
}
