using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Applications
{
    public class CreateInternshipApplicationDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid InternshipId { get; set; }
    }
}
