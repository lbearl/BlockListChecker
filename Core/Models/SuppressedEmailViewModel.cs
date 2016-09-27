using System;

namespace Core.Models
{
    public class SuppressedEmailViewModel
    {
        /// <summary>
        /// The email service provider which has suppressed this email address.
        /// </summary>
        public EspEnum EmailServiceProvider { get; set; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The date time that this email address was added to the suppression list.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// The (normally numeric) identifier of the error (i.e. 550)
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// The error text associated with this suppression.
        /// </summary>
        public string ErrorText { get; set; }
    }
}
