using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Web
{
    internal sealed class SoundCloudApiGateway : ISoundCloudApiGateway
    {
        private readonly JsonSerializerSettings _jsonDeserializeSettings;
        private readonly JsonSerializerSettings _jsonSerializeSettings;
        private readonly IHttpClientFactory _clientFactory;

        public SoundCloudApiGateway(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonSerializeSettings = new JsonSerializerSettings
            {
                ContractResolver = new SpecialContractResolver()
            };

            _jsonDeserializeSettings = new JsonSerializerSettings
            {
                ContractResolver = new SpecialContractResolver(),
                Converters = new List<JsonConverter> { new SoundCloudEntityJsonConverter() }
            };
        }

        public Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, Entity data)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data.ToBoxedEntity(), _jsonSerializeSettings), Encoding.Default,
                    "application/json")
            };

            return SendAsync<TResult>(httpRequestMessage);
        }

        public async Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var multipartFormDataContent = CreateMultipartFormDataContent(parameters);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = multipartFormDataContent
            };

            return await SendAsync<TResult>(httpRequestMessage);
        }

        public Task<ApiResponse<TResult>> InvokeDeleteRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            return SendAsync<TResult>(httpRequestMessage);
        }

        public Task<ApiResponse<TResult>> InvokeGetRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            return SendAsync<TResult>(httpRequestMessage);
        }

        public Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, Entity data)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data.ToBoxedEntity(), _jsonSerializeSettings), Encoding.Default,
                    "application/json")
            };

            return SendAsync<TResult>(httpRequestMessage);
        }

        public Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var multipartFormDataContent = CreateMultipartFormDataContent(parameters);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = multipartFormDataContent
            };

            return SendAsync<TResult>(httpRequestMessage);
        }

        public Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            return SendAsync<TResult>(httpRequestMessage);
        }

        private static MultipartFormDataContent CreateMultipartFormDataContent(IDictionary<string, object> parameters)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var parameter in parameters)
            {
                if (parameter.Value is string stringParameter)
                {
                    var stringContent = new StringContent(stringParameter);
                    stringContent.Headers.Remove("Content-Type");
                    multipartFormDataContent.Add(stringContent, parameter.Key);
                }

                var intParameter = parameter.Value as int?;
                if (intParameter != null)
                {
                    var stringContent = new StringContent(intParameter.ToString());
                    stringContent.Headers.Remove("Content-Type");
                    multipartFormDataContent.Add(stringContent, parameter.Key);
                }

                if (parameter.Value is Stream streamParameter)
                {
                    var streamContent = new StreamContent(streamParameter);
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    multipartFormDataContent.Add(streamContent, parameter.Key);
                }

                if (parameter.Value is Enum enumParameter)
                {
                    var stringContent = new StringContent(enumParameter.ToString());
                    stringContent.Headers.Remove("Content-Type");
                    multipartFormDataContent.Add(stringContent, parameter.Key);
                }
            }

            return multipartFormDataContent;
        }

        private async Task<ApiResponse<TResult>> SendAsync<TResult>(HttpRequestMessage httpRequestMessage)
        {
            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<TResult>(response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            return new ApiResponse<TResult>(response.StatusCode, resultData);
        }
    }
}