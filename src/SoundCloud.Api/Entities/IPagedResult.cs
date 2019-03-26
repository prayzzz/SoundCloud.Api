using System;
using System.Collections.Generic;

namespace SoundCloud.Api.Entities
{
    internal interface IPagedResult<T>
    {
        List<T> Collection { get; }

        bool HasNextPage { get; }

        Uri NextHref { get; }
    }
}