using System;
using System.Linq;

namespace Core.Models
{
    public class SuppressedEmailViewModel
    {
        /// <summary>
        /// The email service provider which has suppressed this email address.
        /// </summary>
        public EspEnum EmailServiceProvider { get; set; }

        /// <summary>
        /// Returns a human readable representation of the ESP enumeration.
        /// </summary>
        public string EmailServiceProviderName
        {
            get
            {
                switch (EmailServiceProvider)
                {
                    case EspEnum.MAILGUN:
                        return "Mailgun";
                    case EspEnum.SENDGRID:
                        return "SendGrid";
                    case EspEnum.SPARKPOST:
                        return "SparkPost";
                    case EspEnum.MANDRILL:
                        return "Mandrill";
                    default:
                        return "Unknown";
                }
            }
        }
        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The date time that this email address was added to the suppression list.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Returns a ISO8601 compliant representation of the datetime.
        /// </summary>
        public string AddedOnDisplay => AddedOn.ToString("o");

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
