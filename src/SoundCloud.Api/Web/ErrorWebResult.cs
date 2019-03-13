namespace SoundCloud.Api.Web
{
    internal class ErrorWebResult<T> : WebResult<T>
    {
        internal ErrorWebResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}