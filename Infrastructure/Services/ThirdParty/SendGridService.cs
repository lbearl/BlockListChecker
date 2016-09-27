using Core.Interfaces;
using System;
using System.Collections.Generic;
using Core.Models;

namespace Infrastructure.Services.ThirdParty
{
    public class SendGridService : IThirdPartyBounceService
    {

        public SuppressedEmailViewModel GetBounce(string address)
        {
            throw new NotImplementedException();
        }

        public List<SuppressedEmailViewModel> GetBounces()
        {
            throw new NotImplementedException();
        }
    }
}
