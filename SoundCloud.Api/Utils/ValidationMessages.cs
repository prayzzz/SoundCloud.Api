using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundCloud.Api.Utils
{
    public class ValidationMessages
    {
        private readonly List<string> _messages;

        public ValidationMessages()
        {
            _messages = new List<string>();
        }

        public bool HasErrors => _messages.Any();

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var message in _messages.Where(x => !string.IsNullOrEmpty(x)))
            {
                sb.AppendLine(message);
            }

            return sb.ToString().Trim();
        }

        internal void Add(string message)
        {
            _messages.Add(message);
        }

        internal void Add(StringBuilder message)
        {
            _messages.Add(message.ToString().Trim());
        }
    }
}