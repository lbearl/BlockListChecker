using System;

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
