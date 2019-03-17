using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoundCloud.Api.Web
{
    internal sealed class SoundCloudApiGateway : ISoundCloudApiGateway
    {
        private const string UserAgent = "SoundCloud.Api by prayzzz (https://github.com/prayzzz/SoundCloud.Api)";

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
                Converters = new List<JsonConverter> {new SoundCloudEntityJsonConverter()}
            };
        }

        public async Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, Entity data)
        {
            return await SendEntityAsync<TResult>(uri, data, HttpMethod.Post);
        }

        public async Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var parameter in parameters)
            {
                if (parameter.Value is string stringParameter)
                {
                    multipartFormDataContent.Add(new StringContent(stringParameter), parameter.Key);
                }

                var intParameter = parameter.Value as int?;
                if (intParameter != null)
                {
                    multipartFormDataContent.Add(new StringContent(intParameter.ToString()), parameter.Key);
                }

                if (parameter.Value is Stream streamParameter)
                {
                    multipartFormDataContent.Add(new StreamContent(streamParameter), parameter.Key);
                }

                if (parameter.Value is Enum enumParameter)
                {
                    multipartFormDataContent.Add(new StringContent(enumParameter.ToString()), parameter.Key);
                }
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = multipartFormDataContent
            };

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }

        public async Task<ApiResponse<TResult>> InvokeDeleteRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }

        public async Task<ApiResponse<TResult>> InvokeGetRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }

        public async Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, Entity data)
        {
            return await SendEntityAsync<TResult>(uri, data, HttpMethod.Put);
        }

        public async Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var parameter in parameters)
            {
                if (parameter.Value is string stringParameter)
                {
                    multipartFormDataContent.Add(new StringContent(stringParameter), parameter.Key);
                }

                var intParameter = parameter.Value as int?;
                if (intParameter != null)
                {
                    multipartFormDataContent.Add(new StringContent(intParameter.ToString()), parameter.Key);
                }

                if (parameter.Value is Stream streamParameter)
                {
                    multipartFormDataContent.Add(new StreamContent(streamParameter), parameter.Key);
                }

                if (parameter.Value is Enum enumParameter)
                {
                    multipartFormDataContent.Add(new StringContent(enumParameter.ToString()), parameter.Key);
                }
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = multipartFormDataContent
            };

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }

        public async Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }

        private async Task<ApiResponse<TResult>> SendEntityAsync<TResult>(Uri uri, Entity data, HttpMethod method)
        {
            var httpRequestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data.ToBoxedEntity(), _jsonSerializeSettings))
            };

            var httpClient = _clientFactory.CreateClient(SoundCloudClient.HttpClientName);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonDeserializeSettings);
            var apiResponse = new ApiResponse<TResult>(response.StatusCode, resultData);
            return apiResponse;
        }
    }
}