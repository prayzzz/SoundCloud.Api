using SoundCloud.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundCloud.Api.Utils
{
    /// <summary>
    /// Custom list implementation for SoundCloud.
    /// <para>
    /// It lazy loads the needed items from SoundCloud using <see cref="_acquireNextPageAsync"/> and caches them.
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SoundCloudList<T>
    {
        private readonly Func<Uri, Task<IPagedResult<T>>> _acquireNextPageAsync;
        private readonly List<T> _items;
        private bool _enumerationFinished;
        private Uri _nextPage;

        internal SoundCloudList(Uri firstPage, Func<Uri, Task<IPagedResult<T>>> acquireNextPageAsync)
        {
            _nextPage = firstPage;
            _acquireNextPageAsync = acquireNextPageAsync;

            _enumerationFinished = false;
            _items = new List<T>();
        }

        public IEnumerable<T> Get()
        {
            for (var i = 0; ; i++)
            {
                // new _items needed
                if (i >= _items.Count)
                {
                    if (!TryGetPageAsync().Result)
                    {
                        yield break;
                    }
                }

                yield return _items[i];
            }
        }

        private async Task<bool> TryGetPageAsync()
        {
            if (_enumerationFinished || _nextPage == null)
            {
                return false;
            }

            var pagedResult = await _acquireNextPageAsync(_nextPage);

            if (pagedResult == null || !pagedResult.collection.Any())
            {
                _enumerationFinished = true;
                return false;
            }

            _items.AddRange(pagedResult.collection);

            if (pagedResult.HasNextPage)
            {
                _nextPage = pagedResult.next_href;
                return true;
            }

            _enumerationFinished = true;
            return true;
        }
    }
}