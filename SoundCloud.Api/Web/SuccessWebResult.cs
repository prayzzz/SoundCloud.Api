namespace SoundCloud.Api.Web
{
    internal class SuccessWebResult<T> : WebResult<T>
    {
        internal SuccessWebResult(T data) : base(true, data)
        {
        }
    }
}