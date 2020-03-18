using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class DashboardSettings
    {
        public int Id { get; set; }

        public string WebsiteTitle { get; set; }

        public string WebsiteHeader { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPictureSrc { get; set; }

        public string AboutMeDescription { get; set; }

        public string AboutMeImage { get; set; }

        public string HomePageImage { get; set; }

        public string HomePageText { get; set; }

        public string FooterText { get; set; }
    }
}
