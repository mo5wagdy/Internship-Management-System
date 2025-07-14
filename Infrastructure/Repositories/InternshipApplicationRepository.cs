using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;


namespace Infrastructure.Repositories
{
    public class InternshipApplicationRepository : GenericRepository<InternshipApplication>, IInternshipApplicationRepository
    {
        private readonly AppDbContext _context;
        public InternshipApplicationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InternshipApplication>> GetByStudentIdAsync(Guid studentId)
            => await _context.InternshipApplications
            .AsNoTracking()
            .Where(a => a.StudentId == studentId)
            .ToListAsync();

        public async Task<IEnumerable<InternshipApplication>> GetByInternshipIdAsync(Guid internshipId)
            => await _context.InternshipApplications
            .AsNoTracking()
            .Where(a => a.InternshipId == internshipId)
            .ToListAsync();
    }
}
