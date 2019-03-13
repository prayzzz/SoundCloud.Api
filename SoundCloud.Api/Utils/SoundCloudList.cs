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
    /// It lazy loads the needed items from SoundCloud using <see cref="_acquireNextPage"/> and caches them.
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SoundCloudList<T>
    {
        private readonly Func<Uri, IPagedResult<T>> _acquireNextPage;
        private readonly Func<Uri, Task<IPagedResult<T>>> _acquireNextPageAsync;
        private readonly List<T> _items;
        private bool _enumerationFinished;
        private Uri _nextPage;

        internal SoundCloudList(Uri firstPage, Func<Uri, IPagedResult<T>> acquireNextPage)
        {
            _nextPage = firstPage;
            _acquireNextPage = acquireNextPage;

            _enumerationFinished = false;
            _items = new List<T>();
        }

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
                    if (!TryGetPage())
                    {
                        yield break;
                    }
                }

                yield return _items[i];
            }
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            while (await TryGetPageAsync()) { }

            return _items;
        }

        private bool TryGetPage()
        {
            if (_enumerationFinished || _nextPage == null)
            {
                return false;
            }

            var pagedResult = _acquireNextPage(_nextPage);

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