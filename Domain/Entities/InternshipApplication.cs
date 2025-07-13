using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class InternshipApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public User Student { get; set; }
        public Guid InternshipId { get; set; }
        public Internship Internship { get; set; }
        public InternshipApplicationStatus Status { get; set; } = InternshipApplicationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
