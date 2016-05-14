using System;
using System.Collections.Generic;

namespace SoundCloud.Api.Entities
{
    internal interface IPagedResult<T>
    {
        List<T> collection { get; }

        bool HasNextPage { get; }

        Uri next_href { get; }
    }
}