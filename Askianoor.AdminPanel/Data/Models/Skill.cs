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

        public Skill()
        {
            SkillId = new Guid();
            Name = "";
            Level = "";
            cssClass = "";
            Group = 0;
        }

        public Skill(Skill skill)
        {
            SkillId = skill.SkillId;
            Name = skill.Name;
            Level = skill.Level;
            cssClass = skill.cssClass;
            Group = skill.Group;
        }
    }
}
