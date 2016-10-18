using Core.Interfaces;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public class SuppressionListCheckService : ISuppressionListCheckService
    {
        private readonly IThirdPartyBounceService _service;

        public SuppressionListCheckService(IThirdPartyBounceService composedService)
        {
            _service = composedService;
        }

        public List<SuppressedEmailViewModel> GetAllSuppressedEmails()
        {
            return _service.GetBounces().ToList();
        }

        public List<SuppressedEmailViewModel> GetSuppressedEmail(string address)
        {
            return _service.GetBounce(address).ToList();
        }

    }
}
