using System;
using System.Text;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Exceptions
{
    public sealed class SoundCloudValidationException : Exception
    {
        public SoundCloudValidationException()
        {
        }

        public SoundCloudValidationException(string message)
            : base(message)
        {
        }

        public SoundCloudValidationException(StringBuilder message)
            : base(message.ToString().Trim())
        {
        }

        public SoundCloudValidationException(ValidationMessages messages)
            : base(messages.ToString())
        {
        }
    }
}