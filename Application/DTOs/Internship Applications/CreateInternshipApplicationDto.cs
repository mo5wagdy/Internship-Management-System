using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Applications
{
    public class CreateInternshipApplicationDto
    {
        public Guid StudentId { get; set; }
        public Guid InternshipId { get; set; }
    }
}
