﻿using Core.Models;
using Core.Models.ThirdParty.Mailgun;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.ThirdParty
{
    internal class MailgunService
    {
        /// <summary>
        /// Retrieves the first 10,000 bounces (in lexicographical order) from mailgun.
        /// </summary>
        /// <returns>A list of <see cref="SuppressedEmailViewModel"/>s for the bounced email addresses.</returns>
        public List<SuppressedEmailViewModel> GetBounces()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
              new HttpBasicAuthenticator("api",
                                         "YOUR_API_KEY");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "YOUR_DOMAIN_NAME", ParameterType.UrlSegment);
            request.AddParameter("limit", 10000); // requesting first 10,000 bounces sorted by ABC.
            request.Resource = "{domain}/bounces";

            var response = client.Execute(request);

            var deserializer = new JsonDeserializer();

            var result = deserializer.Deserialize<BounceList>(response);

            var retList = new List<SuppressedEmailViewModel>();

            //iterate over all of the bounces and add them to the list.
            foreach(var bounce in result.Items)
            {
                retList.Add(new SuppressedEmailViewModel
                {
                    AddedOn = bounce.CreatedAt,
                    EmailAddress = bounce.Address,
                    ErrorCode = bounce.Code,
                    ErrorText = bounce.Error,
                    EmailServiceProvider = EspEnum.MAILGUN
                });
            }

            return retList;

        }
    }
}
