using System.Net;

namespace SoundCloud.Api.Web
{
    internal class ApiResponse<T>
    {
        internal ApiResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        internal ApiResponse(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        internal bool ContainsData => Data != null;

        internal T Data { get; set; }

        internal bool IsError => ((int) StatusCode >= 400) && ((int) StatusCode <= 599);

        internal bool IsSuccess => ((int) StatusCode >= 200) && ((int) StatusCode <= 299);

        internal HttpStatusCode StatusCode { get; }
    }
}