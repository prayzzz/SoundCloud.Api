using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Utils;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal abstract class Endpoint
    {
        private readonly ISoundCloudApiGateway _gateway;

        internal Endpoint(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        /// <summary>
        ///     Performs a POST request with <paramref name="entity" /> using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the POST request</param>
        /// <param name="entity">Entity to be created</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult<T>> CreateAsync<T>(Uri uri, Entity entity) where T : Entity
        {
            var apiResponse = await _gateway.InvokeCreateRequestAsync<T>(uri, entity);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusCode.ToString());
            }

            if (apiResponse.ContainsData)
            {
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        ///     Performs a POST request with <paramref name="parameters" /> using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the POST request</param>
        /// <param name="parameters">Additional Parameters send with the request</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult<T>> CreateAsync<T>(Uri uri, IDictionary<string, object> parameters) where T : Entity
        {
            var apiResponse = await _gateway.InvokeCreateRequestAsync<T>(uri, parameters);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusCode.ToString());
            }

            if (apiResponse.ContainsData)
            {
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        ///     Performs a DELETE request using <paramref name="uri" />.
        /// </summary>
        /// <param name="uri">Target of the DELETE request</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult> DeleteAsync(Uri uri)
        {
            var apiResponse = await _gateway.InvokeDeleteRequestAsync<StatusResponse>(uri);

            if (apiResponse.IsSuccess)
            {
                return new SuccessWebResult<object>(null);
            }

            var errorMessage = new StringBuilder();
            if (apiResponse.ContainsData)
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.Data.Error);
                foreach (var error in apiResponse.Data.Errors)
                {
                    errorMessage.AppendLineIfNotEmpty(error.Message);
                }
            }
            else
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.StatusCode.ToString());
            }

            return new ErrorWebResult<object>(errorMessage.ToString().Trim());
        }

//        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown, if no ClientId or OAuth_Token is set.</exception>
//        protected void EnsureClientId()
//        {
//            if (string.IsNullOrEmpty(Credentials.ClientId) && string.IsNullOrEmpty(Credentials.AccessToken))
//            {
//                throw new SoundCloudInsufficientAccessRightsException("ClientId or OAuth_Token is needed for this operation.");
//            }
//        }
//
//        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown, if no OAuth_Token is set.</exception>
//        protected void EnsureToken()
//        {
//            if (string.IsNullOrEmpty(Credentials.AccessToken))
//            {
//                throw new SoundCloudInsufficientAccessRightsException("OAuth_Token is needed for this operation.");
//            }
//        }

        /// <summary>
        ///     Performs a GET request using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the GET request</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<T> GetByIdAsync<T>(Uri uri) where T : Entity
        {
            var apiResponse = await _gateway.InvokeGetRequestAsync<T>(uri);

            if (apiResponse.IsSuccess && apiResponse.ContainsData)
            {
                return apiResponse.Data;
            }

            return null;
        }

        /// <summary>
        ///     Performs a GET request using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the GET request</param>
        /// <returns>The list of response entity of the request</returns>
        protected async Task<IEnumerable<T>> GetListAsync<T>(Uri uri) where T : Entity
        {
            async Task<IPagedResult<T>> GetPage(Uri x)
            {
                var apiResponse = await _gateway.InvokeGetRequestAsync<PagedResult<T>>(x);
                if (apiResponse.IsSuccess && apiResponse.ContainsData)
                {
                    return apiResponse.Data;
                }

                return new PagedResult<T>();
            }

            return new SoundCloudList<T>(uri, GetPage).Get();
        }

        /// <summary>
        ///     Performs a PUT request using <paramref name="uri" />.
        /// </summary>
        /// <param name="uri">Target of the PUT request</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult> UpdateAsync(Uri uri)
        {
            var apiResponse = await _gateway.InvokeUpdateRequestAsync<StatusResponse>(uri);

            if (apiResponse.IsSuccess)
            {
                return new SuccessWebResult<object>(null);
            }

            var errorMessage = new StringBuilder();
            if (apiResponse.ContainsData)
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.Data.Error);
                foreach (var error in apiResponse.Data.Errors)
                {
                    errorMessage.AppendLineIfNotEmpty(error.Message);
                }
            }
            else
            {
                errorMessage.AppendLineIfNotEmpty(apiResponse.StatusCode.ToString());
            }

            return new ErrorWebResult<object>(errorMessage.ToString().Trim());
        }

        /// <summary>
        ///     Performs a PUT request with <paramref name="entity" /> using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the PUT request</param>
        /// <param name="entity">Entity to be created</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult<T>> UpdateAsync<T>(Uri uri, Entity entity) where T : Entity
        {
            var apiResponse = await _gateway.InvokeUpdateRequestAsync<T>(uri, entity);

            if (apiResponse.IsError)
            {
                return new ErrorWebResult<T>(apiResponse.StatusCode.ToString());
            }

            if (apiResponse.ContainsData)
            {
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        ///     Performs a PUT request with <paramref name="parameters" /> using <paramref name="uri" />.
        /// </summary>
        /// <typeparam name="T">Type of the response entity</typeparam>
        /// <param name="uri">Target of the PUT request</param>
        /// <param name="parameters">Additional Parameters send with the request</param>
        /// <returns>The response entity of the request</returns>
        protected async Task<IWebResult<T>> UpdateAsync<T>(Uri uri, IDictionary<string, object> parameters) where T : Entity
        {
            var apiResponse = await _gateway.InvokeUpdateRequestAsync<T>(uri, parameters);

            if (!apiResponse.IsSuccess)
            {
                return new ErrorWebResult<T>(apiResponse.StatusCode.ToString());
            }

            if (apiResponse.ContainsData)
            {
                return new SuccessWebResult<T>(apiResponse.Data);
            }

            return new SuccessWebResult<T>(null);
        }

        /// <summary>
        ///     Calls the passed method for validation.
        ///     Throws <see cref="SoundCloudValidationException" />, if validation fails.
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