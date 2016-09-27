using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.ThirdParty.Mailgun
{
    public class Paging
    {
        public string First{ get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public string Last { get; set; }
    }
}
