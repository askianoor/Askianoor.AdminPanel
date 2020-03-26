using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class PortfolioCategory
    {
        public Guid PortfolioCategoryId { get; set; }

        public string CategoryName { get; set; }

        public PortfolioCategory()
        {
            PortfolioCategoryId = new Guid();
            CategoryName = "";

        }

        public PortfolioCategory(PortfolioCategory portfolioCategory)
        {
            PortfolioCategoryId = portfolioCategory.PortfolioCategoryId;
            CategoryName = portfolioCategory.CategoryName;
        }
    }
}
