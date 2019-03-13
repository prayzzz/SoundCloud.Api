namespace SoundCloud.Api.Web
{
    /// <summary>
    /// Represents the result of a web request with no response data
    /// <para>
    /// If the request was successfull <see cref="IWebResult.IsSuccess"/> is true and <see cref="Data"/> might be filled.
    /// If the request failed <see cref="IWebResult.IsSuccess"/> is false and <see cref="IWebResult.ErrorMessage"/> is maybe filled with a description of the error. 
    /// </para>
    /// </summary>
    public interface IWebResult<T> : IWebResult
    {
        T Data { get; }
    }

    /// <summary>
    /// Represents the result of a web request with no response data
    /// <para>
    /// If the request was successfull <see cref="IsSuccess"/> is true.
    /// If the request failed <see cref="IsSuccess"/> is false and <see cref="ErrorMessage"/> is maybe filled with a description of the error. 
    /// </para>
    /// </summary>
    public interface IWebResult
    {
        string ErrorMessage { get; }

        bool IsSuccess { get; }
    }
}