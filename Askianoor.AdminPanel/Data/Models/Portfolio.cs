using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string BackTitle { get; set; }

        public string Body { get; set; }

        public string BackBody { get; set; }

        public string PictureSrc { get; set; }
        public string CirclePictureSrc { get; set; }

        public string Technologies { get; set; }

        public string projectLink { get; set; }

        public Guid PortfolioCategoryId { get; set; }
    }
}
