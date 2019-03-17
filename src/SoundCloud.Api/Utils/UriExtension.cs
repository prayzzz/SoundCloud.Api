using System;
using System.Linq;

namespace SoundCloud.Api.Utils
{
//    internal static class UriExtension
//    {
//        internal static Uri AppendCredentials(this Uri uri, SoundCloudCredentials credentials)
//        {
//            if (uri == null)
//            {
//                return null;
//            }
//
//            var delimiter = "&";
//            if (string.IsNullOrEmpty(uri.Query))
//            {
//                delimiter = "?";
//            }
//            else if (uri.Query.Last() == '?')
//            {
//                delimiter = "";
//            }
//
//            var uriString = uri.ToString();
//            if (!string.IsNullOrEmpty(credentials.AccessToken))
//            {
//                uriString += delimiter + "oauth_token=" + credentials.AccessToken;
//                return new Uri(uriString);
//            }
//
//            if (!string.IsNullOrEmpty(credentials.ClientId))
//            {
//                uriString += delimiter + "client_id=" + credentials.ClientId;
//                return new Uri(uriString);
//            }
//
//            return new Uri(uriString);
//        }
//    }
}