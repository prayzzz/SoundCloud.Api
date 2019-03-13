using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Test.Data;

namespace SoundCloud.Api.IntegrationTest
{
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_Groups_Delete_Add_Contribution()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var group = new Group();
            group.id = GroupId;

            var track = new Track();
            track.id = Track3Id;

            var result = client.Groups.Post(group, track);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.id, Is.EqualTo(track.id));

            var tracks = client.Groups.GetContributions(group);
            Assert.That(tracks.Any(x => x.id == track.id), Is.True);

            client.Groups.DeleteContribution(group, track);

            tracks = client.Groups.GetContributions(group);
            Assert.That(tracks.All(x => x.id != track.id), Is.True);
        }

        [Test]
        public void Test_Groups_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var groups = client.Groups.Get(GroupId);

            Assert.That(groups.name, Is.EqualTo(SharpSoundGroupName));
        }

        [Test]
        public void Test_Groups_GetContributions()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var group = new Group();
            group.id = GroupId;

            var contributions = client.Groups.GetContributions(group).Take(100).ToList();

            Assert.That(contributions.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Groups_GetContributors()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var group = new Group();
            group.id = GroupId;

            var contributors = client.Groups.GetContributors(group).Take(100).ToList();

            Assert.That(contributors.Count, Is.GreaterThan(0));
            Assert.That(contributors.Any(x => x.id == UserId), Is.True);
        }

        [Test]
        public void Test_Groups_GetMembers()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var group = new Group();
            group.id = GroupId;

            var members = client.Groups.GetMembers(group).Take(100).ToList();

            Assert.That(members.Count, Is.GreaterThan(0));
            Assert.That(members.Any(x => x.id == UserId), Is.True);
        }

        [Test]
        public void Test_Groups_GetModerators()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var group = new Group();
            group.id = GroupId;

            var moderators = client.Groups.GetModerators(group).Take(100).ToList();

            Assert.That(moderators.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Groups_GetPendingTracks()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var group = new Group();
            group.id = GroupId;

            var pendingTracks = client.Groups.GetPendingTracks(group).Take(100).ToList();

            Assert.That(pendingTracks.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void Test_Groups_GetTracks()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var group = new Group();
            group.id = GroupId;

            var tracks = client.Groups.GetTracks(group).Take(100).ToList();

            Assert.That(tracks.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Groups_GetUsers()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var group = new Group();
            group.id = GroupId;

            var users = client.Groups.GetUsers(group).Take(100).ToList();

            Assert.That(users.Count, Is.GreaterThan(0));
            Assert.That(users.Any(x => x.id == UserId), Is.True);
        }

        [Test]
        public void Test_Groups_Post_Update_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var group = new Group();
            group.name = "TestGroup";
            group.short_description = "short_description";
            group.description = "description";

            var postResult = client.Groups.Post(group);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.name, Is.EqualTo(group.name));
            Assert.That(postResult.Data.short_description, Is.EqualTo(group.short_description));
            Assert.That(postResult.Data.description, Is.EqualTo(group.description));

            var postedGroup = postResult.Data;
            postedGroup.name = "TestGroup2";
            postedGroup.short_description = "short_description2";
            postedGroup.description = "description2";

            var updatedResult = client.Groups.Update(postResult.Data);

            Assert.That(updatedResult.IsSuccess, Is.True);
            Assert.That(updatedResult.Data.name, Is.EqualTo(postedGroup.name));
            Assert.That(updatedResult.Data.short_description, Is.EqualTo(postedGroup.short_description));
            Assert.That(updatedResult.Data.description, Is.EqualTo(postedGroup.description));

            updatedResult = client.Groups.UploadArtwork(postedGroup, TestDataProvider.GetArtwork());

            Assert.That(string.IsNullOrEmpty(updatedResult.Data.artwork_url), Is.False);

            client.Groups.Delete(postedGroup);
        }
    }
}