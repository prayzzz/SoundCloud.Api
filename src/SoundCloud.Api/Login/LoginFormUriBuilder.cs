using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Login
{
    public sealed class LoginFormUriBuilder
    {
        private const string Uri = "https://soundcloud.com/connect";

        public LoginFormUriBuilder(string clientId, string redirectUri)
        {
            ClientId = clientId;
            RedirectUri = redirectUri;

            Scope = Scope.None;
            Display = Display.None;
            ResponseType = ResponseType.Token;
        }

        public string ClientId { get; }

        public Display Display { get; set; }

        public string RedirectUri { get; }

        public ResponseType ResponseType { get; set; }

        public Scope Scope { get; set; }

        public Uri Create()
        {
            var arguments = new Dictionary<string, string>();

            arguments.Add("client_id", ClientId);
            arguments.Add("response_type", ResponseType.GetAttributeOfType<EnumMemberAttribute>().Value);
            arguments.Add("display", Display.GetAttributeOfType<EnumMemberAttribute>().Value);
            arguments.Add("scope", Scope.GetAttributeOfType<EnumMemberAttribute>().Value);
            arguments.Add("redirect_uri", RedirectUri);

            var uri = new UriBuilder(Uri);
            uri.Query = string.Join("&", arguments.Select(x => x.Key + "=" + x.Value));

            return uri.Uri;
        }
    }
}