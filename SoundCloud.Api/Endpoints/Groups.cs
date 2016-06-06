using System.Collections.Generic;
using System.IO;

using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Groups : Endpoint, IGroups
    {
        private const string GroupArtworkDataKey = "group[artwork_data]";
        private const string GroupContributionPath = "groups/{0}/contributions/{1}?";
        private const string GroupContributionsPath = "groups/{0}/contributions?";
        private const string GroupContributorsPath = "groups/{0}/contributors?";
        private const string GroupMembersPath = "groups/{0}/members?";
        private const string GroupModeratorsPath = "groups/{0}/moderators?";
        private const string GroupPath = "groups/{0}?";
        private const string GroupPendingTrackPath = "groups/{0}/pending_tracks/{1}?";
        private const string GroupPendingTracksPath = "groups/{0}/pending_tracks?";
        private const string GroupsPath = "groups?";
        private const string GroupTracksPath = "groups/{0}/tracks?";
        private const string GroupUsersPath = "groups/{0}/users?";

        public Groups(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public IWebResult Delete(Group group)
        {
            EnsureToken();
            Validate(group.ValidateDelete);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPath, group.id);

            return Delete(builder.BuildUri());
        }

        public IWebResult DeleteContribution(Group group, Track track)
        {
            EnsureToken();
            Validate(track.ValidateDelete);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupContributionPath, group.id, track.id);

            return Delete(builder.BuildUri());
        }

        public IWebResult DeletePendingTrack(Group group, Track track)
        {
            EnsureToken();
            Validate(track.ValidateDelete);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPendingTrackPath, group.id, track.id);

            return Delete(builder.BuildUri());
        }

        public Group Get(int groupId)
        {
            EnsureClientId();

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPath, groupId);

            return GetById<Group>(builder.BuildUri());
        }

        public IEnumerable<Group> Get() => Get(new GroupQueryBuilder());

        public IEnumerable<Group> Get(GroupQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = GroupsPath;
            queryBuilder.Paged = true;

            return GetList<Group>(queryBuilder.BuildUri());
        }

        public IEnumerable<Track> GetContributions(Group group)
        {
            EnsureToken();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupContributionsPath, group.id);
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public IEnumerable<User> GetContributors(Group group)
        {
            EnsureClientId();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupContributorsPath, group.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IEnumerable<User> GetMembers(Group group)
        {
            EnsureClientId();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupMembersPath, group.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IEnumerable<User> GetModerators(Group group)
        {
            EnsureClientId();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupModeratorsPath, group.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IEnumerable<Track> GetPendingTracks(Group group)
        {
            EnsureToken();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPendingTracksPath, group.id);
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public IEnumerable<Track> GetTracks(Group group)
        {
            EnsureClientId();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupTracksPath, group.id);
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public IEnumerable<User> GetUsers(Group group)
        {
            EnsureClientId();
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupUsersPath, group.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public IWebResult<Track> Post(Group group, Track track)
        {
            EnsureToken();
            Validate(track.ValidateDelete);

            var param = new Dictionary<string, object>();
            param.Add("track[id]", track.id);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupContributionsPath, group.id);

            return Create<Track>(builder.BuildUri(), param);
        }

        public IWebResult<Group> Post(Group group)
        {
            EnsureToken();
            Validate(group.ValidatePost);

            var builder = new GroupQueryBuilder();
            builder.Path = GroupsPath;

            return Create<Group>(builder.BuildUri(), group);
        }

        public IWebResult<Group> Update(Group group)
        {
            EnsureToken();
            Validate(group.ValidateUpdate);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPath, group.id);

            return Update<Group>(builder.BuildUri(), group);
        }

        public IWebResult<Group> UploadArtwork(Group group, Stream file)
        {
            EnsureToken();
            Validate(group.ValidateUploadArtwork);

            var param = new Dictionary<string, object>();
            param.Add(GroupArtworkDataKey, file);

            var builder = new GroupQueryBuilder();
            builder.Path = string.Format(GroupPath, group.id);

            return Update<Group>(builder.BuildUri(), param);
        }
    }
}