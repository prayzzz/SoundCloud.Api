using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Utils;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal abstract class Endpoint
    {
        protected readonly ISoundCloudApiGateway Gateway;
        internal SoundCloudCredentials Credentials;

        internal Endpoint(ISoundCloudApiGateway gateway)
        {
            Credentials = new SoundCloudCredentials();
            Gateway = gateway;
        }

        /// <summary>
        /// Perfoms a POST request with <paramref name="entity"/> using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the POST request</param>
        /// <param name="entity">Entity to be created</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult<T> Create<T>(Uri uri, Entity entity) where T : Entity
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeCreateRequest<T>(uri, entity);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusDescription);
            }

            if (apiResponse.ContainsData)
            {
                apiResponse.Data.AppendCredentialsToProperties(Credentials);
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        /// Perfoms a POST request with <paramref name="parameters"/> using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the POST request</param>
        /// <param name="parameters">Additional Parameters send with the request</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult<T> Create<T>(Uri uri, IDictionary<string, object> parameters) where T : Entity
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeCreateRequest<T>(uri, parameters);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusDescription);
            }

            if (apiResponse.ContainsData)
            {
                apiResponse.Data.AppendCredentialsToProperties(Credentials);
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        /// Perfoms a DELETE request using <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Target of the DELETE request</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult Delete(Uri uri)
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeDeleteRequest<StatusResponse>(uri);

            if (apiResponse.IsSuccess)
            {
                return new SuccessWebResult<object>(null);
            }

            var errorMessage = new StringBuilder();
            if (apiResponse.ContainsData)
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.Data.Error);
                foreach (var message in apiResponse.Data.Errors)
                {
                    errorMessage.AppendLineIfNotEmpty(message.error_message);
                }
            }
            else
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.StatusDescription);
            }

            return new ErrorWebResult<object>(errorMessage.ToString().Trim());
        }

        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown, if no ClientId or OAuth_Token is set.</exception>
        protected void EnsureClientId()
        {
            if (string.IsNullOrEmpty(Credentials.ClientId) && string.IsNullOrEmpty(Credentials.AccessToken))
            {
                throw new SoundCloudInsufficientAccessRightsException("Atleast the ClientId is needed for this operation.");
            }
        }

        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown, if no OAuth_Token is set.</exception>
        protected void EnsureToken()
        {
            if (string.IsNullOrEmpty(Credentials.AccessToken))
            {
                throw new SoundCloudInsufficientAccessRightsException("The OAuth_Token is needed for this operation.");
            }
        }

        /// <summary>
        /// Perfoms a GET request using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response ntity</typeparam>
        /// <param name="uri">Target of the GET request</param>
        /// <returns>The response entity of the request</returns>
        protected T GetById<T>(Uri uri) where T : Entity
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeGetRequest<T>(uri);

            if (apiResponse.IsSuccess && apiResponse.ContainsData)
            {
                apiResponse.Data.AppendCredentialsToProperties(Credentials);
                return apiResponse.Data;
            }

            return null;
        }

        /// <summary>
        /// Perfoms a GET request using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response ntity</typeparam>
        /// <param name="uri">Target of the GET request</param>
        /// <returns>The list of response entity of the request</returns>
        protected IEnumerable<T> GetList<T>(Uri uri) where T : Entity
        {
            Func<Uri, IPagedResult<T>> getPage = x =>
            {
                var apiResponse = Gateway.InvokeGetRequest<PagedResult<T>>(x.AppendCredentials(Credentials));
                if (apiResponse.IsSuccess && apiResponse.ContainsData)
                {
                    return apiResponse.Data;
                }

                return new PagedResult<T>();
            };

            return new SoundCloudList<T>(uri, getPage).Get().Select(x =>
            {
                x.AppendCredentialsToProperties(Credentials);
                return x;
            });
        }

        /// <summary>
        /// Perfoms a PUT request using <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">Target of the PUT request</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult Update(Uri uri)
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeUpdateRequest<StatusResponse>(uri);

            if (apiResponse.IsSuccess)
            {
                return new SuccessWebResult<object>(null);
            }

            var errorMessage = new StringBuilder();
            if (apiResponse.ContainsData)
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.Data.Error);
                foreach (var message in apiResponse.Data.Errors)
                {
                    errorMessage.AppendLineIfNotEmpty(message.error_message);
                }
            }
            else
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.StatusDescription);
            }

            return new ErrorWebResult<object>(errorMessage.ToString().Trim());
        }

        /// <summary>
        /// Perfoms a PUT request with <paramref name="entity"/> using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the PUT request</param>
        /// <param name="entity">Entity to be created</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult<T> Update<T>(Uri uri, Entity entity) where T : Entity
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeUpdateRequest<T>(uri, entity);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusDescription);
            }

            if (apiResponse.ContainsData)
            {
                apiResponse.Data.AppendCredentialsToProperties(Credentials);
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        /// Perfoms a PUT request with <paramref name="parameters"/> using <paramref name="uri"/>.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the PUT request</param>
        /// <param name="parameters">Additional Parameters send with the request</param>
        /// <returns>The response entity of the request</returns>
        protected IWebResult<T> Update<T>(Uri uri, IDictionary<string, object> parameters) where T : Entity
        {
            uri = uri.AppendCredentials(Credentials);
            var apiResponse = Gateway.InvokeUpdateRequest<T>(uri, parameters);

            if (!apiResponse.IsSuccess)
            {
                return new ErrorWebResult<T>(apiResponse.StatusDescription);
            }

            if (apiResponse.ContainsData)
            {
                apiResponse.Data.AppendCredentialsToProperties(Credentials);
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        /// Calls the passed method for validation.
        /// Throws <see cref="SoundCloudValidationException" />, if validation fails.
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown, if validation fails.</exception>
        protected void Validate(Func<ValidationMessages, bool> validateMethod)
        {
            var messages = new ValidationMessages();
            if (!validateMethod(messages))
            {
                throw new SoundCloudValidationException(messages);
            }
        }
    }
}