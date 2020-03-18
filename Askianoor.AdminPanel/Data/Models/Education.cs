using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class Education
    {
        public Guid EducationId { get; set; }

        public string educationTitle { get; set; }
        public string universityTitle { get; set; }
        public string universityAddress { get; set; }
        public string universityPlace { get; set; }
        public string degree { get; set; }
        public string description { get; set; }
        public string year { get; set; }
        public string icon { get; set; }
    }
}
