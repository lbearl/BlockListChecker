using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISuppressionListCheckService
    {
        /// <summary>
        /// Gets all suppressed emails from all ESPs.
        /// </summary>
        /// <returns>A list of <see cref="SuppressedEmailViewModel"/> corresponding to all suppressed emails.</returns>
        List<SuppressedEmailViewModel> GetAllSuppressedEmails();

        /// <summary>
        /// Gets all suppressions for a single email address.
        /// </summary>
        /// <param name="address">The email address to test for.</param>
        /// <returns>A list of <see cref="SuppressedEmailViewModel"/> one for each ESP which the passed in address is suppressed on.</returns>
        List<SuppressedEmailViewModel> GetSuppressedEmail(string address);
    }
}
