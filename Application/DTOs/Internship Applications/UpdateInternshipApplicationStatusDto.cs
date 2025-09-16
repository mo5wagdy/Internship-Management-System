using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Applications
{
    public class UpdateInternshipApplicationStatusDto
    {
        public Guid ApplicationId { get; set; }
        public string Status { get; set; } = String.Empty;
    }
}
