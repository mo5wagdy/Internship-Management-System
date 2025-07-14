/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DataSeedingService
    {
        private readonly AppDbContext _context;
        public DataSeedingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new Domain.Entities.User
                {
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}*/
