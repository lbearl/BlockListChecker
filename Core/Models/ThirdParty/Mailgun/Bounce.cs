using System;

namespace Core.Models.ThirdParty.Mailgun
{
    public class Bounce
    {
        public string Address { get; set; }

        public string Code { get; set; }

        public string Error { get; set; }

        public string CreatedAt { get; set; }
    }
}
