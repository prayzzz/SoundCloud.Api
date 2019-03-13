using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Web;

namespace SoundCloud.Api
{
    public sealed class SoundCloudClient : ISoundCloudClient
    {
        private readonly Apps _apps;
        private readonly Comments _comments;
        private readonly Groups _groups;
        private readonly Me _me;
        private readonly OAuth2 _oAuth2;
        private readonly Playlists _playlists;
        private readonly Resolve _resolve;
        private readonly Tracks _tracks;
        private readonly Users _users;
        private string _clientId;
        private string _token;

        private SoundCloudClient()
        {
            var gateway = new SoundCloudApiGateway();
            _comments = new Comments(gateway);
            _oAuth2 = new OAuth2(gateway);
            _playlists = new Playlists(gateway);
            _tracks = new Tracks(gateway);
            _users = new Users(gateway);
            _groups = new Groups(gateway);
            _me = new Me(gateway);
            _apps = new Apps(gateway);
            _resolve = new Resolve(gateway);
        }

        internal string ClientId
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                _apps.Credentials.ClientId = value;
                _comments.Credentials.ClientId = value;
                _groups.Credentials.ClientId = value;
                _me.Credentials.ClientId = value;
                _oAuth2.Credentials.ClientId = value;
                _playlists.Credentials.ClientId = value;
                _resolve.Credentials.ClientId = value;
                _tracks.Credentials.ClientId = value;
                _users.Credentials.ClientId = value;
            }
        }

        internal string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                _apps.Credentials.AccessToken = value;
                _comments.Credentials.AccessToken = value;
                _groups.Credentials.AccessToken = value;
                _me.Credentials.AccessToken = value;
                _oAuth2.Credentials.AccessToken = value;
                _playlists.Credentials.AccessToken = value;
                _resolve.Credentials.AccessToken = value;
                _tracks.Credentials.AccessToken = value;
                _users.Credentials.AccessToken = value;
            }
        }

        public IApps Apps
        {
            get { return _apps; }
        }

        public IComments Comments
        {
            get { return _comments; }
        }

        public IGroups Groups
        {
            get { return _groups; }
        }

        public bool IsAuthorized => !string.IsNullOrEmpty(Token);

        public IMe Me
        {
            get { return _me; }
        }

        public IOAuth2 OAuth2
        {
            get { return _oAuth2; }
        }

        public IPlaylists Playlists
        {
            get { return _playlists; }
        }

        public IResolve Resolve
        {
            get { return _resolve; }
        }

        public ITracks Tracks
        {
            get { return _tracks; }
        }

        public IUsers Users
        {
            get { return _users; }
        }

        public static ISoundCloudClient CreateAuthorized(string token)
        {
            return new SoundCloudClient {Token = token};
        }

        public static ISoundCloudClient CreateUnauthorized(string clientId)
        {
            return new SoundCloudClient {ClientId = clientId};
        }

        internal static SoundCloudClient Create()
        {
            return new SoundCloudClient();
        }
    }
}