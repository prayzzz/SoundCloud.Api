using System;

namespace SharpSound.SoundCloud.Query
{
    internal class InvalidQueryException : Exception
    {
        private readonly string message;

        public InvalidQueryException(string message)
        {
            this.message = message + " ";
        }

        public override string Message
        {
            get { return "The client query is invalid: " + this.message; }
        }
    }
}