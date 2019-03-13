using System.Collections.Generic;

namespace SoundCloud.Api.QueryBuilders
{
    public class UserQueryBuilder : SoundCloudQueryBuilder
    {
        public UserQueryBuilder()
        {
            SearchString = "";
        }

        public string SearchString { get; set; }

        protected override void AddArguments(IDictionary<string, string> queryArguments)
        {
            base.AddArguments(queryArguments);

            if (!string.IsNullOrEmpty(SearchString))
            {
                ApplyPrimitiveType(queryArguments, "q", SearchString);
            }
        }
    }
}