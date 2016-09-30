using Core.Interfaces;
using System;
using System.Collections.Generic;
using Core.Models;
using RestSharp;
using RestSharp.Deserializers;
using Core.Models.ThirdParty.SendGrid;
using System.Linq;
using System.Configuration;

namespace Infrastructure.Services.ThirdParty
{
    /// <summary>
    /// The SendGrid specific bounce checking implementation.
    /// </summary>
    public class SendGridService : IThirdPartyBounceService
    {
        private readonly string SENDGRID_API_KEY;

        /// <summary>
        /// Constructs a new SendGridService.
        /// </summary>
        public SendGridService()
        {
            SENDGRID_API_KEY = ConfigurationManager.AppSettings["SendGridApiKey"];
        }

        /// <summary>
        /// Get SendGrid bounces. Should return an IEnumerable of 0 or 1 elements.
        /// </summary>
        /// <param name="address">The address to test for.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of 0 or 1 <see cref="SuppressedEmailViewModel"/>s.</returns>
        public IEnumerable<SuppressedEmailViewModel> GetBounce(string address)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.sendgrid.com/v3");
            
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {SENDGRID_API_KEY}");
            request.AddHeader("Content", "application/json");
            request.Resource = $"suppression/bounces/{address}";

            var response = client.Execute(request);

            var deserializer = new JsonDeserializer();

            // per API docs, sendgrid returns a list of 1 element
            var result = deserializer.Deserialize<List<Bounce>>(response).FirstOrDefault();

            yield return new SuppressedEmailViewModel
            {
                AddedOn = DateTimeOffset.FromUnixTimeSeconds(int.Parse(result.Created)).LocalDateTime,
                EmailAddress = result.Email,
                ErrorCode = result.Status,
                ErrorText = result.Reason,
                EmailServiceProvider = EspEnum.SENDGRID
            };
        }

        /// <summary>
        /// Get SendGrid bounces. Should return an IEnumerable of all bounces.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="SuppressedEmailViewModel"/>s.</returns>
        public IEnumerable<SuppressedEmailViewModel> GetBounces()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.sendgrid.com/v3");

            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {SENDGRID_API_KEY}");
            request.AddHeader("Content", "application/json");
            request.Resource = $"suppression/bounces/";

            var response = client.Execute(request);

            var deserializer = new JsonDeserializer();

            // per API docs, sendgrid returns a list of 1 element
            var result = deserializer.Deserialize<List<Bounce>>(response);


            foreach (var bounce in result)
            {
                if(!string.IsNullOrEmpty(bounce.Created))
                    yield return new SuppressedEmailViewModel
                    {
                        AddedOn = DateTimeOffset.FromUnixTimeSeconds(int.Parse(bounce.Created)).LocalDateTime,
                        EmailAddress = bounce.Email,
                        ErrorCode = bounce.Status,
                        ErrorText = bounce.Reason,
                        EmailServiceProvider = EspEnum.SENDGRID
                    };
            }
        }
    }
}
