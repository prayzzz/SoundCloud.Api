namespace SoundCloud.Api.Web
{
    internal abstract class WebResult<T> : IWebResult<T>
    {
        protected WebResult(bool isSuccess, T data)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = string.Empty;
        }

        protected WebResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool IsSuccess { get; }

        public T Data { get; }
    }
}