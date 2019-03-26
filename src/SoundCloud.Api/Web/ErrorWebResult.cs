namespace SoundCloud.Api.Web
{
    internal sealed class ErrorWebResult<T> : WebResult<T>
    {
        internal ErrorWebResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}