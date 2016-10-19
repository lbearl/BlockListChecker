using Core.Interfaces;
using System;
using System.Collections.Generic;
using Core.Models;

namespace Infrastructure.Services
{
    public class CompositeBounceCheckerService : IThirdPartyBounceService
    {
        private readonly IThirdPartyBounceService[] _bounceService;

        public CompositeBounceCheckerService(IThirdPartyBounceService[] bounceServices)
        {
            if (bounceServices == null) throw new ArgumentNullException(nameof(bounceServices));

            _bounceService = bounceServices;
        }

        public IEnumerable<SuppressedEmailViewModel> GetBounces()
        {
            var retlist = new List<SuppressedEmailViewModel>();
            foreach (var svc in _bounceService)
                retlist.AddRange(svc.GetBounces());

            return retlist;
        }

        public IEnumerable<SuppressedEmailViewModel> GetBounce(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException(nameof(address));

            var retList = new List<SuppressedEmailViewModel>();

            foreach (var svc in _bounceService)
                retList.AddRange(svc.GetBounce(address));

            return retList;
        }
    }
}
