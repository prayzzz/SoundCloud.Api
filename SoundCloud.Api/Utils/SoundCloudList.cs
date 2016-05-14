using System;
using System.Collections.Generic;
using System.Linq;

using SoundCloud.Api.Entities;

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

        public IEnumerable<T> Get()
        {
            for (var i = 0;; i++)
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
    }
}