using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class LikePortfolio
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid PortfolioId { get; set; }
    }
}
