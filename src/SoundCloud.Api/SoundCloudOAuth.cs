using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api
{
    public static class SoundCloudOAuth
    {
        private static readonly string Version = typeof(SoundCloudClient).Assembly.GetName().Version.ToString();

        public static async Task<Credentials> FromClientCredentials(string clientId, string clientSecret)
        {
            using (var httpClient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();
                formData.Add(CreateStringContent("client_credentials"), "\"grant_type\"");
                formData.Add(CreateStringContent(clientId), "\"client_id\"");
                formData.Add(CreateStringContent(clientSecret), "\"client_secret\"");

                var message = new HttpRequestMessage(HttpMethod.Post, "https://api.soundcloud.com/oauth2/token");
                message.Headers.UserAgent.Add(new ProductInfoHeaderValue("SoundCloud.Api", Version));
                message.Content = formData;

                var response = await httpClient.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                {
                    throw new SoundCloudApiException(response.StatusCode, response.Content);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Credentials>(responseContent);
            }
        }

        public static async Task<Credentials> FromPassword(string clientId, string clientSecret, string username, string password)
        {
            using (var httpClient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();
                formData.Add(CreateStringContent("password"), "\"grant_type\"");
                formData.Add(CreateStringContent(clientId), "\"client_id\"");
                formData.Add(CreateStringContent(clientSecret), "\"client_secret\"");
                formData.Add(CreateStringContent(username), "\"username\"");
                formData.Add(CreateStringContent(password), "\"password\"");

                var message = new HttpRequestMessage(HttpMethod.Post, "https://api.soundcloud.com/oauth2/token");
                message.Headers.UserAgent.Add(new ProductInfoHeaderValue("SoundCloud.Api", Version));
                message.Content = formData;

                var response = await httpClient.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                {
                    throw new SoundCloudApiException(response.StatusCode, response.Content);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Credentials>(responseContent);
            }
        }

        public static async Task<Credentials> FromRefreshToken(string clientId, string clientSecret, string refreshToken)
        {
            using (var httpClient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();
                formData.Add(CreateStringContent("refresh_token"), "\"grant_type\"");
                formData.Add(CreateStringContent(clientId), "\"client_id\"");
                formData.Add(CreateStringContent(clientSecret), "\"client_secret\"");
                formData.Add(CreateStringContent(refreshToken), "\"refresh_token\"");

                var message = new HttpRequestMessage(HttpMethod.Post, "https://api.soundcloud.com/oauth2/token");
                message.Headers.UserAgent.Add(new ProductInfoHeaderValue("SoundCloud.Api", Version));
                message.Content = formData;

                var response = await httpClient.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                {
                    throw new SoundCloudApiException(response.StatusCode, response.Content);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Credentials>(responseContent);
            }
        }

        private static StringContent CreateStringContent(string content)
        {
            var stringContent = new StringContent(content);
            stringContent.Headers.Remove("Content-Type");
            return stringContent;
        }
    }
}
