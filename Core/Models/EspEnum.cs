namespace Core.Models
{
    /// <summary>
    /// Provides a listing of all configurable Email Service Providers.
    /// </summary>
    public enum EspEnum
    {
        /// <summary>
        /// Mailgun ESP.
        /// </summary>
        MAILGUN = 10,

        /// <summary>
        /// SendGrid ESP.
        /// </summary>
        SENDGRID = 20,

        /// <summary>
        /// SparkPost ESP.
        /// </summary>
        SPARKPOST = 30,

        /// <summary>
        /// Mandrill ESP.
        /// </summary>
        MANDRILL = 40,

        /// <summary>
        /// AN unknown ESP. Should result in an error.
        /// </summary>
        UNKNOWN = 0

    }
}
