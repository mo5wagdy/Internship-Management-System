using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Applications
{
    public class InternshipApplicationDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid InternshipId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
