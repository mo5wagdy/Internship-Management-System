using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }
        public IInternshipRepository Internships { get; }
        public IInternshipApplicationRepository InternshipApplications { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Internships = new InternshipRepository(_context);
            InternshipApplications = new InternshipApplicationRepository(_context);
        }

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();
        
        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();
    }
}
