using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class Navbar
    {
        public Guid MenuId { get; set; }

        public int MenuOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
    }
}
