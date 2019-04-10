using System;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Exceptions
{
    public sealed class SoundCloudValidationException : Exception
    {
        internal SoundCloudValidationException(string message)
            : base(message)
        {
        }

        internal SoundCloudValidationException(ValidationMessages messages)
            : base(messages.ToString())
        {
        }
    }
}