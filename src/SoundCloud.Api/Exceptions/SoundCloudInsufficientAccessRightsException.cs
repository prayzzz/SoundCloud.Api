using System;

namespace SoundCloud.Api.Exceptions
{
    public sealed class SoundCloudInsufficientAccessRightsException : Exception
    {
        public SoundCloudInsufficientAccessRightsException()
        {
        }

        public SoundCloudInsufficientAccessRightsException(string message)
            : base(message)
        {
        }
    }
}