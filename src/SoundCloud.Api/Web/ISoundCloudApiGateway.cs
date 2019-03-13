using System;
using System.Collections.Generic;

using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Web
{
    internal interface ISoundCloudApiGateway
    {
        ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, Entity data);

        ApiResponse<TResult> InvokeCreateRequest<TResult>(Uri uri, IDictionary<string, object> parameters);

        ApiResponse<TResult> InvokeDeleteRequest<TResult>(Uri uri);

        ApiResponse<TResult> InvokeGetRequest<TResult>(Uri uri);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, Entity data);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri, IDictionary<string, object> parameters);

        ApiResponse<TResult> InvokeUpdateRequest<TResult>(Uri uri);
    }
}