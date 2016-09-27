using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockListChecker.Controllers
{
    public class CheckController : Controller
    {
        private ISuppressionListCheckService _checkService;

        /// <summary>
        /// Constructs a new CheckController with the supplied dependencies.
        /// </summary>
        /// <param name="suppressionList">The suppression list check service.</param>
        public CheckController(ISuppressionListCheckService checkService)
        {
            _checkService = checkService;
        }

        /// <summary>
        /// Returns a JSON list of suppressed email addresses from all ESPs.
        /// </summary>
        /// <returns>A JSON list of suppressed email addresses.</returns>
        [HttpGet]
        [Route("GetSuppressions")]
        public IEnumerable<SuppressedEmailViewModel> GetSuppressions()
        {
            return _checkService.GetAllSuppressedEmails();
        }

        [HttpGet]
        [Route("GetSuppression/{address}")]
        public IEnumerable<SuppressedEmailViewModel> GetSuppression(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException($"{nameof(address)} cannot be null or empty");
            return _checkService.GetSuppressedEmail(address);
        }
    }
}