using System.Collections.Generic;

namespace Core.Models.ThirdParty.Mailgun
{
    public class BounceList
    {
        public List<Bounce> Items { get; set; }

        public Paging Paging {get;set;}
    }
}
