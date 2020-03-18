using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class Skill
    {
        public Guid SkillId { get; set; }

        public string Name { get; set; }
        public string Level { get; set; }
        public string cssClass { get; set; }
        public int Group { get; set; }
    }
}
