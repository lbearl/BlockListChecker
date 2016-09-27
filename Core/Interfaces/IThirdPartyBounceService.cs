using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    /// <summary>
    /// Common functions all 3rd party bounce services must implement.
    /// </summary>
    public interface IThirdPartyBounceService
    {
        /// <summary>
        /// Get all bounces.
        /// </summary>
        /// <returns>A list of all bounces for the given ESP.</returns>
        List<SuppressedEmailViewModel> GetBounces();

        /// <summary>
        /// Get an entry for a single address
        /// </summary>
        /// <param name="address">The email address to test.</param>
        /// <returns>A single bounce for a single address at a single ESP.</returns>
        SuppressedEmailViewModel GetBounce(string address);
    }
}
