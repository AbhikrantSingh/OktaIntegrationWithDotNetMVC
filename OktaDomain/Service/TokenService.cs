using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OktaDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OktaDomain.Service
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<OktaTokenSettings> options;

        public TokenService(IOptions<OktaTokenSettings> options)
        {
            this.options = options;
        }

        public async Task<OktaResponse> GetToken(string userName, string password)
        {
            //logic to Get Token from Okta.
            var token = new OktaResponse();
            var client = new HttpClient();

            var client_id = options.Value.ClientId;
            var client_secret = options.Value.ClientSecret;
            var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id} :{client_secret}");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));


            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var postMessage = new Dictionary<string, string>();
            postMessage.Add("grant_type", "password");
            postMessage.Add("username", userName);
            postMessage.Add("password", password);
            postMessage.Add("scope", "openid");

            var request = new HttpRequestMessage(HttpMethod.Post,
                                        $"{options.Value.Domain}/oauth2/default/v1/token")
            {
                Content = new FormUrlEncodedContent(postMessage)
            };

        var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonSerializerSettings = new JsonSerializerSettings();
                var json = await response.Content.ReadAsStringAsync();

                token = JsonConvert.DeserializeObject<OktaResponse>(json, jsonSerializerSettings);
                token.ExpiresAt = token.ExpiresAt;

                
            }
            else
            {
                var err = await response.Content?.ReadAsStringAsync();
                
            }

            return token;





        }
    }
}
