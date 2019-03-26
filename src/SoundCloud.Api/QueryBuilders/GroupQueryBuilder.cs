using System.Collections.Generic;

namespace SoundCloud.Api.QueryBuilders
{
    /// <summary>
    ///     Results are limited to 50 per request
    /// </summary>
    public class GroupQueryBuilder : SoundCloudQueryBuilder
    {
        public GroupQueryBuilder()
        {
            CustomMaxLimit = 50;
        }

        public string SearchString { get; set; }

        protected override void AddArguments(IDictionary<string, string> queryArguments)
        {
            base.AddArguments(queryArguments);

            ApplyPrimitiveType(queryArguments, "q", SearchString);
        }
    }
}