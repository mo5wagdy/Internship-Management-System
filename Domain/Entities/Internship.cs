using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Internship
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Guid CompanyId { get; set; }

        // Navigational
        public User Company { get; set; } 
        public ICollection<InternshipApplication> Applications { get; set; }
    }
}
