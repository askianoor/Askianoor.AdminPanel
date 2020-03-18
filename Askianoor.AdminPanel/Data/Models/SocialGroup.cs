using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class SocialGroup
    {
        public Guid SocialId { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
