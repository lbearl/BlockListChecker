using Core.Interfaces;
using Core.Models;
using Infrastructure.Services.ThirdParty;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class SuppressionListCheckService : ISuppressionListCheckService
    {
        private readonly IThirdPartyBounceService _mailgunService;
        private readonly IThirdPartyBounceService _sendGridService;

        public SuppressionListCheckService(IThirdPartyBounceService mailgunService, IThirdPartyBounceService sendGridService)
        {
            _mailgunService = mailgunService;
            _sendGridService = sendGridService;
        }
        public List<SuppressedEmailViewModel> GetAllSuppressedEmails()
        {
            var retList = new List<SuppressedEmailViewModel>();

            retList.AddRange(_mailgunService.GetBounces());
            retList.AddRange(_sendGridService.GetBounces());
            //TODO - add additional ESPs.

            return retList;
        }

        public List<SuppressedEmailViewModel> GetSuppressedEmail(string address)
        {
            var retList = new List<SuppressedEmailViewModel>();

            retList.Add(_mailgunService.GetBounce(address));
            retList.Add(_sendGridService.GetBounce(address));

            return retList;
        }

    }
}
