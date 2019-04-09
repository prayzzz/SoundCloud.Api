using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Web
{
    internal interface ISoundCloudApiGateway
    {
        Task<TResult> SendPostRequestAsync<TResult>(Uri uri, Entity data);

        Task<TResult> SendPostRequestAsync<TResult>(Uri uri, IDictionary<string, object> formData);

        Task<TResult> SendDeleteRequestAsync<TResult>(Uri uri);

        Task<TResult> SendGetRequestAsync<TResult>(Uri uri);

        Task<TResult> SendPutRequestAsync<TResult>(Uri uri, Entity data);

        Task<TResult> SendPutRequestAsync<TResult>(Uri uri, IDictionary<string, object> formData);

        Task<TResult> SendPutRequestAsync<TResult>(Uri uri);
    }
}