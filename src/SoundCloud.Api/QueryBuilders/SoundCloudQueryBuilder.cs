using System;
using System.Collections.Generic;
using System.Linq;

namespace SoundCloud.Api.QueryBuilders
{
    public abstract class SoundCloudQueryBuilder
    {
        private const string Host = "https://api.soundcloud.com/";
        private const int MaxLimit = 200;
        private const int MinLimit = 1;
        private int _customMaxLimit;
        private int _limit;

        protected SoundCloudQueryBuilder()
        {
            CustomMaxLimit = MaxLimit;
            Limit = MaxLimit;
            Paged = false;
            Path = "?";
        }

        protected int CustomMaxLimit
        {
            get => _customMaxLimit;
            set
            {
                if (value > MaxLimit)
                {
                    _customMaxLimit = MaxLimit;
                }
                else if (value < MinLimit)
                {
                    _customMaxLimit = MinLimit;
                }
                else
                {
                    _customMaxLimit = value;
                }

                if (Limit > _customMaxLimit)
                {
                    Limit = _customMaxLimit;
                }
            }
        }

        public int Limit
        {
            get => _limit;
            set
            {
                if (value > _customMaxLimit)
                {
                    _limit = _customMaxLimit;
                }
                else if (value < MinLimit)
                {
                    _limit = MinLimit;
                }
                else
                {
                    _limit = value;
                }
            }
        }

        internal bool Paged { get; set; }

        internal string Path { get; set; }

        protected virtual void AddArguments(IDictionary<string, string> queryArguments)
        {
            if (Paged)
            {
                ApplyPrimitiveType(queryArguments, "limit", Limit);
            }
        }

        protected static void ApplyList<T>(IDictionary<string, string> query, string key, IList<T> filters)
        {
            ApplyList(query, key, filters, t => t.ToString());
        }

        protected static void ApplyList<T>(IDictionary<string, string> query, string key, IList<T> filters, Func<T, string> selector)
        {
            if (!filters.Any())
            {
                return;
            }

            AddEscapedValue(query, key, string.Join(",", filters.Select(selector)));
        }

        protected static void ApplyNullableDateTimeType(IDictionary<string, string> query, string key, DateTime? filter,
                                                        Func<DateTime, string> selector)
        {
            if (!filter.HasValue)
            {
                return;
            }

            AddEscapedValue(query, key, selector(filter.Value));
        }

        protected static void ApplyNullableEnumType(IDictionary<string, string> query, string key, Enum filter, Enum defaulValue,
                                                    Func<Enum, string> selector)
        {
            if (filter.Equals(defaulValue))
            {
                return;
            }

            AddEscapedValue(query, key, selector(filter));
        }

        protected static void ApplyNullablePrimitiveType<T>(IDictionary<string, string> query, string key, T? filter, Func<T, string> selector)
            where T : struct
        {
            if (filter.HasValue)
            {
                AddEscapedValue(query, key, selector(filter.Value));
            }
        }

        protected static void ApplyPrimitiveType(IDictionary<string, string> query, string key, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return;
            }

            AddEscapedValue(query, key, filter);
        }

        protected string BuildQuery()
        {
            var queryArguments = new Dictionary<string, string>();
            AddArguments(queryArguments);

            return string.Join("&", queryArguments.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
        }

        internal Uri BuildUri()
        {
            var uriString = Host + Path + BuildQuery();

            if (Paged)
            {
                uriString += "&linked_partitioning=1";
            }

            return new Uri(uriString);
        }

        private static void AddEscapedValue(IDictionary<string, string> query, string key, string value)
        {
            query.Add(key, Uri.EscapeDataString(value));
        }

        private static void ApplyPrimitiveType(IDictionary<string, string> query, string key, int filter)
        {
            AddEscapedValue(query, key, filter.ToString());
        }
    }
}