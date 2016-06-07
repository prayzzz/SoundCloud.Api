using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

using Newtonsoft.Json;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Web
{
    internal sealed class SoundCloudApiGateway : ISoundCloudApiGateway
    {
        private const string CreateMethod = "POST";
        private const string DeleteMethod = "DELETE";
        private const string GetMethod = "GET";
        private const string UpdateMethod = "PUT";
        private const string UserAgent = "SoundCloud.Api by prayzzz (https://github.com/prayzzz/SoundCloud.Api)";

        private readonly JsonSerializerSettings _jsonDeserializeSettings;
        private readonly JsonSerializerSettings _jsonSerializeSettings;

        public SoundCloudApiGateway()
        {
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

        public ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, Entity data) => SendEntity<TResult>(uri, data, CreateMethod);

        public ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var builder = new MultipartDataFormRequestBuilder();
            builder.Add(parameters);

            var request = CreateRequest(uri, WebRequestMethods.Http.Post);
            builder.ApplyTo(request);

            return Evaluate<TResult>(ExecuteRequest(request));
        }

        public ApiResponse<TResult> InvokeDeleteRequest<TResult>(Uri uri) => Evaluate<TResult>(ExecuteRequest(CreateRequest(uri, DeleteMethod)));

        public ApiResponse<TResult> InvokeGetRequest<TResult>(Uri uri) => Evaluate<TResult>(ExecuteRequest(CreateRequest(uri, GetMethod)));

        public ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, Entity data) => SendEntity<TResult>(uri, data, UpdateMethod);

        public ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, IDictionary<string, object> parameters)
        {
            var builder = new MultipartDataFormRequestBuilder();
            builder.Add(parameters);

            var request = CreateRequest(uri, UpdateMethod);
            builder.ApplyTo(request);

            return Evaluate<TResult>(ExecuteRequest(request));
        }

        public ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri)
        {
            var request = CreateRequest(uri, WebRequestMethods.Http.Put);
            return Evaluate<TResult>(ExecuteRequest(request));
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static HttpWebRequest CreateRequest(Uri uri, string method)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = method;
            request.ContentType = "application/json";
            request.UserAgent = UserAgent;

            return request;
        }

        private ApiResponse<TResult> Evaluate<TResult>(HttpWebResponse response)
        {
            string json;

            if (response == null)
            {
                return new ApiResponse<TResult>(HttpStatusCode.GatewayTimeout, "No connection available.");
            }

            var responseStream = response.GetResponseStream();
            if (responseStream == null)
            {
                return new ApiResponse<TResult>(HttpStatusCode.GatewayTimeout, "No connection available.");
            }

            using (var stream = new StreamReader(responseStream))
            {
                json = stream.ReadToEnd();
            }

            try
            {
                var data = JsonConvert.DeserializeObject<TResult>(json, _jsonDeserializeSettings);
                var apiResponse = new ApiResponse<TResult>(response.StatusCode, response.StatusDescription);
                apiResponse.Data = data;

                return apiResponse;
            }
            catch (JsonException)
            {
                return new ApiResponse<TResult>(response.StatusCode, response.StatusDescription);
            }
        }

        private static HttpWebResponse ExecuteRequest(WebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                return (HttpWebResponse)ex.Response;
            }
        }

        private ApiResponse<TResult> SendEntity<TResult>(Uri uri, Entity data, string method)
        {
            var jsonData = JsonConvert.SerializeObject(data.ToBoxedEntity, _jsonSerializeSettings);
            var request = CreateRequest(uri, method);

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }

            return Evaluate<TResult>(ExecuteRequest(request));
        }
    }
}