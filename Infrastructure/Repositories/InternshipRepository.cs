using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class InternshipRepository : GenericRepository<Internship>, IInternshipRepository
    {
        private readonly AppDbContext _context;
        public InternshipRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Internship>> GetByCompanyIdAsync(Guid companyId)
            => await _context.Internships
            .AsNoTracking()
            .Where(i => i.CompanyId == companyId)
            .ToListAsync();

        public async Task<IEnumerable<Internship>> SearchByTitleAsync(string keyword)
            => await _context.Internships
                .AsNoTracking()
                .Where(i => EF.Functions.Like(i.Title, $"%{keyword}%"))
                .ToListAsync();
    }
}
