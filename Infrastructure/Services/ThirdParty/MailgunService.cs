using Core.Interfaces;
using Core.Models;
using Core.Models.ThirdParty.Mailgun;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Infrastructure.Services.ThirdParty
{
    public class MailgunService : IThirdPartyBounceService
    {
        private readonly string MAILGUN_API_KEY;
        private readonly string MAILGUN_DOMAIN_NAME;

        public MailgunService()
        {
            MAILGUN_API_KEY = ConfigurationManager.AppSettings["MailgunAPI"];
            MAILGUN_DOMAIN_NAME = ConfigurationManager.AppSettings["MailgunDomain"];
        }

        public IEnumerable<SuppressedEmailViewModel> GetBounce(string address)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
              new HttpBasicAuthenticator("api",
                                         MAILGUN_API_KEY); 
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 MAILGUN_DOMAIN_NAME, ParameterType.UrlSegment); 
            request.AddParameter("limit", 10000); // requesting first 10,000 bounces sorted by ABC.
            request.Resource = $"{{domain}}/bounces/{address}";

            var response = client.Execute(request);

            var deserializer = new JsonDeserializer();

            var result = deserializer.Deserialize<Bounce>(response);

            if (!string.IsNullOrEmpty(result.Address))
                yield return new SuppressedEmailViewModel
                {
                    // .net doesn't seem to have a nice way to parse RFC 2822 datetimes with UTC timezone
                    // so just strip it off.
                    AddedOn = DateTime.Parse(result.CreatedAt.Substring(0, 26)),
                    EmailAddress = result.Address,
                    ErrorCode = result.Code,
                    ErrorText = result.Error,
                    EmailServiceProvider = EspEnum.MAILGUN
                };
            else
                yield return new SuppressedEmailViewModel
                {
                    AddedOn = DateTime.Now,
                    EmailAddress = "unknown",
                    ErrorCode = "Internal",
                    ErrorText = "Error getting results from ESP",
                    EmailServiceProvider = EspEnum.MAILGUN
                };
        }

        /// <summary>
        /// Retrieves the first 10,000 bounces (in lexicographical order) from mailgun.
        /// </summary>
        /// <returns>A list of <see cref="SuppressedEmailViewModel"/>s for the bounced email addresses.</returns>
        public IEnumerable<SuppressedEmailViewModel> GetBounces()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
              new HttpBasicAuthenticator("api",
                                         MAILGUN_API_KEY);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 MAILGUN_DOMAIN_NAME, ParameterType.UrlSegment);
            request.AddParameter("limit", 10000); // requesting first 10,000 bounces sorted by ABC.
            request.Resource = "{domain}/bounces";

            var response = client.Execute(request);

            var deserializer = new JsonDeserializer();

            var result = deserializer.Deserialize<BounceList>(response);

            //iterate over all of the bounces and add them to the list.
            foreach(var bounce in result.Items)
            {
               yield return new SuppressedEmailViewModel
                {
                    AddedOn = DateTime.Parse(bounce.CreatedAt),
                    EmailAddress = bounce.Address,
                    ErrorCode = bounce.Code,
                    ErrorText = bounce.Error,
                    EmailServiceProvider = EspEnum.MAILGUN
                };
            }
        }
    }
}
