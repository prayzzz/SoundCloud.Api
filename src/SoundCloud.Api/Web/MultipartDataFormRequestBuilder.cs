using SoundCloud.Api.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SoundCloud.Api.Web
{
    internal sealed class MultipartDataFormRequestBuilder
    {
        private const string ApplicationOctetStreamContentType = "application/octet-stream";
        private const string BoundaryEndPattern = "\r\n--{0}--\r\n";
        private const string BoundaryStartPattern = "--{0}\r\n";
        private const string ContentDispositionPattern = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n";
        private const string ContentDispositionWithFilePattern = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{0}\"\r\n\r\n";
        private const string ContentTypePattern = "Content-Type: {0}\r\n";
        private readonly string _boundary;
        private readonly Dictionary<string, object> _contents;

        internal MultipartDataFormRequestBuilder()
        {
            _contents = new Dictionary<string, object>();
            _boundary = Guid.NewGuid().ToString();
        }

        internal void Add(string key, string content)
        {
            _contents.Add(key, content);
        }

        internal void Add(string key, Stream content)
        {
            _contents.Add(key, content);
        }

        internal void Add(string key, int content)
        {
            _contents.Add(key, content);
        }

        internal void Add(string key, Enum content)
        {
            _contents.Add(key, content.GetAttributeOfType<EnumMemberAttribute>().Value);
        }

        internal void Add(IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                var stringParameter = parameter.Value as string;
                if (stringParameter != null)
                {
                    Add(parameter.Key, stringParameter);
                }

                var intParameter = parameter.Value as int?;
                if (intParameter != null)
                {
                    Add(parameter.Key, intParameter.Value);
                }

                var streamParameter = parameter.Value as Stream;
                if (streamParameter != null)
                {
                    Add(parameter.Key, streamParameter);
                }

                var enumParameter = parameter.Value as Enum;
                if (enumParameter != null)
                {
                    Add(parameter.Key, enumParameter);
                }
            }
        }

        internal void ApplyTo(WebRequest request)
        {
            request.ContentType = "multipart/form-data; boundary=" + _boundary;

            using (var str = BuildContent(_boundary))
            {
                str.CopyTo(request.GetRequestStream());
            }
        }

        internal async Task ApplyToAsync(WebRequest request)
        {
            request.ContentType = "multipart/form-data; boundary=" + _boundary;

            using (var str = BuildContent(_boundary))
            {
                await str.CopyToAsync(await request.GetRequestStreamAsync());
            }
        }

        internal void Remove(string name)
        {
            _contents.Remove(name);
        }

        private Stream BuildContent(string boundary)
        {
            var boundaryStart = string.Format(BoundaryStartPattern, boundary);
            var boundaryStartBytes = boundaryStart.GetBytes();

            var boundaryEnd = string.Format(BoundaryEndPattern, boundary);
            var boundaryEndBytes = boundaryEnd.GetBytes();

            var linebreak = false;
            var str = new MemoryStream();
            foreach (var content in _contents)
            {
                // don't write line break on first item
                if (linebreak)
                {
                    str.Write("\r\n".GetBytes(), 0, 2);
                }

                linebreak = true;

                var contentType = GetContentType(content.Value).GetBytes();
                var contentDisposition = GetContentDisposition(content.Key, content.Value).GetBytes();
                var data = GetBytes(content.Value);

                str.Write(boundaryStartBytes, 0, boundaryStartBytes.Length);
                str.Write(contentType, 0, contentType.Length);
                str.Write(contentDisposition, 0, contentDisposition.Length);
                str.Write(data, 0, data.Length);
            }

            str.Write(boundaryEndBytes, 0, boundaryEndBytes.Length);
            str.Position = 0;

            return str;
        }

        private async Task<Stream> BuildContentAsync(string boundary)
        {
            var boundaryStart = string.Format(BoundaryStartPattern, boundary);
            var boundaryStartBytes = boundaryStart.GetBytes();

            var boundaryEnd = string.Format(BoundaryEndPattern, boundary);
            var boundaryEndBytes = boundaryEnd.GetBytes();

            var linebreak = false;
            var str = new MemoryStream();
            foreach (var content in _contents)
            {
                // don't write line break on first item
                if (linebreak)
                {
                    str.Write("\r\n".GetBytes(), 0, 2);
                }

                linebreak = true;

                var contentType = GetContentType(content.Value).GetBytes();
                var contentDisposition = GetContentDisposition(content.Key, content.Value).GetBytes();
                var data = GetBytes(content.Value);

                await str.WriteAsync(boundaryStartBytes, 0, boundaryStartBytes.Length);
                await str.WriteAsync(contentType, 0, contentType.Length);
                await str.WriteAsync(contentDisposition, 0, contentDisposition.Length);
                await str.WriteAsync(data, 0, data.Length);
            }

            str.Write(boundaryEndBytes, 0, boundaryEndBytes.Length);
            str.Position = 0;

            return str;
        }

        private static byte[] GetBytes(object content)
        {
            var str = content as string;
            if (str != null)
            {
                return str.GetBytes();
            }

            var number = content as int?;
            if (number != null)
            {
                return number.ToString().GetBytes();
            }

            var stream = content as Stream;
            if (stream != null)
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }

            throw new ArgumentOutOfRangeException(nameof(content));
        }

        private static string GetContentDisposition(string name, object content)
        {
            if (content is string || content is int)
            {
                return string.Format(ContentDispositionPattern, name);
            }

            if (content is Stream)
            {
                return string.Format(ContentDispositionWithFilePattern, name);
            }

            return string.Format(ContentDispositionPattern, name);
        }

        private static string GetContentType(object content)
        {
            if (content is string || content is int)
            {
                return string.Empty;
            }

            if (content is Stream)
            {
                return string.Format(ContentTypePattern, ApplicationOctetStreamContentType);
            }

            throw new ArgumentOutOfRangeException(nameof(content));
        }
    }
}