using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Utils;
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

        public async Task<IWebResult> DeleteAsync(Group group)
        {
            Validate(group.ValidateDelete);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupPath, group.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<IWebResult> DeleteContributionAsync(Group group, Track track)
        {
            Validate(track.ValidateDelete);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupContributionPath, group.Id, track.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<IWebResult> DeletePendingTrackAsync(Group group, Track track)
        {
            Validate(track.ValidateDelete);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupPendingTrackPath, group.Id, track.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<Group> GetAsync(int groupId)
        {
            var builder = new GroupQueryBuilder { Path = string.Format(GroupPath, groupId) };
            return await GetByIdAsync<Group>(builder.BuildUri());
        }

        public async Task<IEnumerable<Group>> GetAsync()
        {
            return await GetAsync(new GroupQueryBuilder());
        }

        public async Task<IEnumerable<Group>> GetAsync(GroupQueryBuilder queryBuilder)
        {
            queryBuilder.Path = GroupsPath;
            queryBuilder.Paged = true;

            return await GetListAsync<Group>(queryBuilder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetContributionsAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupContributionsPath, group.Id), Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetContributorsAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupContributorsPath, group.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetMembersAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupMembersPath, group.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetModeratorsAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupModeratorsPath, group.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetPendingTracksAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupPendingTracksPath, group.Id), Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupTracksPath, group.Id), Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetUsersAsync(Group group)
        {
            Validate(group.ValidateGet);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupUsersPath, group.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IWebResult<Track>> PostAsync(Group group, Track track)
        {
            Validate(track.ValidateDelete);

            var param = new Dictionary<string, object> { { "track[id]", track.Id } };
            var builder = new GroupQueryBuilder { Path = string.Format(GroupContributionsPath, group.Id) };
            return await CreateAsync<Track>(builder.BuildUri(), param);
        }

        public async Task<IWebResult<Group>> PostAsync(Group group)
        {
            Validate(group.ValidatePost);

            var builder = new GroupQueryBuilder { Path = GroupsPath };
            return await CreateAsync<Group>(builder.BuildUri(), group);
        }

        public async Task<IWebResult<Group>> UpdateAsync(Group group)
        {
            Validate(group.ValidateUpdate);

            var builder = new GroupQueryBuilder { Path = string.Format(GroupPath, group.Id) };
            return await UpdateAsync<Group>(builder.BuildUri(), group);
        }

        public async Task<IWebResult<Group>> UploadArtworkAsync(Group group, Stream file)
        {
            Validate(group.ValidateUploadArtwork);

            var param = new Dictionary<string, object> { { GroupArtworkDataKey, file } };
            var builder = new GroupQueryBuilder { Path = string.Format(GroupPath, group.Id) };
            return await UpdateAsync<Group>(builder.BuildUri(), param);
        }
    }
}