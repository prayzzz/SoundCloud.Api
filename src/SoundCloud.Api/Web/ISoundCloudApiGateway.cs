using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Web
{
    internal interface ISoundCloudApiGateway
    {
        Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, Entity data);

        Task<ApiResponse<TResult>> InvokeCreateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters);

        Task<ApiResponse<TResult>> InvokeDeleteRequestAsync<TResult>(Uri uri);

        Task<ApiResponse<TResult>> InvokeGetRequestAsync<TResult>(Uri uri);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, Entity data);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri, IDictionary<string, object> parameters);

        Task<ApiResponse<TResult>> InvokeUpdateRequestAsync<TResult>(Uri uri);
    }
}