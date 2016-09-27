using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.ThirdParty.Mailgun
{
    public class Bounce
    {
        public string Address { get; set; }

        public string Code { get; set; }

        public string Error { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
