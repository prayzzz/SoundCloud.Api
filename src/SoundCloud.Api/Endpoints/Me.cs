using System.Collections.Generic;

using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Me : Endpoint, IMe
    {
        private const string MeActivitesPath = "me/activities?";
        private const string MeCommentsPath = "me/comments?";
        private const string MeConnectionsPath = "me/connections?";
        private const string MeFavoritesPath = "me/favorites?";
        private const string MeFavoritPath = "me/favorites/{0}?";
        private const string MeFollowersPath = "me/followers?";
        private const string MeFollowingsPath = "me/followings?";
        private const string MeFollowPath = "me/followings/{0}?";
        private const string MeGroupsPath = "me/groups?";
        private const string MePath = "me?";
        private const string MePlaylistsPath = "me/playlists?";
        private const string MeTracksPath = "me/tracks?";
        private const string MeWebProfilePath = "me/web-profiles/{0}?";
        private const string MeWebProfilesPath = "me/web-profiles?";

        public Me(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public IWebResult DeleteWebProfile(WebProfile profile)
        {
            EnsureToken();
            Validate(profile.ValidateDelete);

            var builder = new MeQueryBuilder();
            builder.Path = string.Format(MeWebProfilePath, profile.id);

            return Delete(builder.BuildUri());
        }

        public IWebResult Follow(User user)
        {
            EnsureToken();
            Validate(user.ValidateFollowUnfollow);

            var builder = new MeQueryBuilder();
            builder.Path = string.Format(MeFollowPath, user.id);

            return Update(builder.BuildUri());
        }

        public User Get()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MePath;

            return GetById<User>(builder.BuildUri());
        }

        public IEnumerable<Activity> GetActivities()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeActivitesPath;
            builder.Paged = true;

            return GetList<Activity>(builder.BuildUri());
        }

        public IEnumerable<Comment> GetComments()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeCommentsPath;
            builder.Paged = true;

            return GetList<Comment>(builder.BuildUri());
        }

        public IEnumerable<Connection> GetConnections()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeConnectionsPath;
            builder.Paged = true;

            return GetList<Connection>(builder.BuildUri());
        }

        public IEnumerable<Track> GetFavorites()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeFavoritesPath;
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public IEnumerable<User> GetFollowers()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeFollowersPath;
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IEnumerable<User> GetFollowings()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeFollowingsPath;
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IEnumerable<Group> GetGroups()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeGroupsPath;
            builder.Paged = true;

            return GetList<Group>(builder.BuildUri());
        }

        public IEnumerable<Playlist> GetPlaylists()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MePlaylistsPath;
            builder.Paged = true;

            return GetList<Playlist>(builder.BuildUri());
        }

        public IEnumerable<Track> GetTracks()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeTracksPath;
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public IEnumerable<WebProfile> GetWebProfiles()
        {
            EnsureToken();

            var builder = new MeQueryBuilder();
            builder.Path = MeWebProfilesPath;
            builder.Paged = true;

            return GetList<WebProfile>(builder.BuildUri());
        }

        public IWebResult Like(Track track)
        {
            EnsureToken();
            Validate(track.ValidateLikeUnlike);

            var builder = new MeQueryBuilder();
            builder.Path = string.Format(MeFavoritPath, track.id);

            return Update(builder.BuildUri());
        }

        public IWebResult<WebProfile> PostWebProfile(WebProfile profile)
        {
            EnsureToken();
            Validate(profile.ValidatePost);

            var builder = new MeQueryBuilder();
            builder.Path = MeWebProfilesPath;

            return Create<WebProfile>(builder.BuildUri(), profile);
        }

        public IWebResult Unfollow(User user)
        {
            EnsureToken();
            Validate(user.ValidateFollowUnfollow);

            var builder = new MeQueryBuilder();
            builder.Path = string.Format(MeFollowPath, user.id);

            return Delete(builder.BuildUri());
        }

        public IWebResult Unlike(Track track)
        {
            EnsureToken();
            Validate(track.ValidateLikeUnlike);

            var builder = new MeQueryBuilder();
            builder.Path = string.Format(MeFavoritPath, track.id);

            return Delete(builder.BuildUri());
        }
    }
}