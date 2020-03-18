﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class Experience
    {
        public Guid ExperienceId { get; set; }

        public string jobTitle { get; set; }
        public string companyTitle { get; set; }
        public string companyAddress { get; set; }
        public string description { get; set; }
        public string year { get; set; }
        public string icon { get; set; }
    }
}
