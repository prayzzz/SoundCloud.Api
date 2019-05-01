using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Entities
{
    public class SoundCloudList<T> : IEnumerable<T> where T : Entity
    {
        private readonly IEnumerable<T> _collection;
        private readonly Func<Task<SoundCloudList<T>>> _nextAction;

        public SoundCloudList(IEnumerable<T> collection, Func<Task<SoundCloudList<T>>> next)
        {
            _collection = collection;
            _nextAction = next;
        }

        public SoundCloudList(IEnumerable<T> collection)
        {
            _collection = collection;
            _nextAction = null;
        }

        public bool HasNextPage => _nextAction != null;

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public async Task<SoundCloudList<T>> GetNextPageAsync()
        {
            if (_nextAction == null)
            {
                return new SoundCloudList<T>(Enumerable.Empty<T>());
            }

            return await _nextAction();
        }
    }
}
