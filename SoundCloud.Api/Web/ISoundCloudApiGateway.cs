using SoundCloud.Api.Entities.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundCloud.Api.Web
{
    internal interface ISoundCloudApiGateway
    {
        ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, Entity data);

        Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, Entity data);

        ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, IDictionary<string, object> parameters);

        Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters);

        ApiResponse<TResult> InvokeDeleteRequest<TResult>(Uri uri);

        Task<ApiResponse<TResult>> InvokeDeleteRequestAsync<TResult>(Uri uri);

        ApiResponse<TResult> InvokeGetRequest<TResult>(Uri uri);

        Task<ApiResponse<TResult>> InvokeGetRequestAsync<TResult>(Uri uri);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, Entity data);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, Entity data);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, IDictionary<string, object> parameters);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri);

    }
}