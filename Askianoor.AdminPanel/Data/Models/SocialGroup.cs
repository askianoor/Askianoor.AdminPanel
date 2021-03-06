﻿using System;
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

        public SocialGroup()
        {
            SocialId = new Guid();
            Name = "";
            Icon = "";
            Link = "";
        }

        public SocialGroup(SocialGroup socialGroup)
        {
            SocialId = socialGroup.SocialId;
            Name = socialGroup.Name;
            Icon = socialGroup.Icon;
            Link = socialGroup.Link;
        }

    }
}
