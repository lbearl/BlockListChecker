using Core.Interfaces;
using System;
using System.Collections.Generic;
using Core.Models;
using RestSharp;
using RestSharp.Deserializers;
using Core.Models.ThirdParty.SendGrid;
using System.Linq;

namespace Infrastructure.Services.ThirdParty
{
    public class SendGridService : IThirdPartyBounceService
    {
        private readonly string SENDGRID_API_KEY;
        public SendGridService()
        {
            SENDGRID_API_KEY = "TODO";
        }

        public SuppressedEmailViewModel GetBounce(string address)
        {
            //https://api.sendgrid.com/v3/suppression/bounces/{email}
            //GET https://api.sendgrid.com/v3/suppression/bounces
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

            return new SuppressedEmailViewModel
            {
                AddedOn = DateTimeOffset.FromUnixTimeSeconds(int.Parse(result.Created)).LocalDateTime,
                EmailAddress = result.Email,
                ErrorCode = result.Status,
                ErrorText = result.Reason,
                EmailServiceProvider = EspEnum.SENDGRID
            };
        }

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
