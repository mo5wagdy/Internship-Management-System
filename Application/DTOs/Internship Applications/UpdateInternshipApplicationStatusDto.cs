using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Applications
{
    public class UpdateInternshipApplicationStatusDto
    {
        [Required]
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = String.Empty;
    }
}
