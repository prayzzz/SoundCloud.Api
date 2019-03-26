namespace SoundCloud.Api.Web
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the result of a web request with no response data
    ///     <para>
    ///         If the request was successful <see cref="P:SoundCloud.Api.Web.IWebResult.IsSuccess" /> is true and
    ///         <see cref="P:SoundCloud.Api.Web.IWebResult`1.Data" /> might be filled.
    ///         If the request failed <see cref="P:SoundCloud.Api.Web.IWebResult.IsSuccess" /> is false and
    ///         <see cref="P:SoundCloud.Api.Web.IWebResult.ErrorMessage" /> is maybe filled with a description of the error.
    ///     </para>
    /// </summary>
    public interface IWebResult<T> : IWebResult
    {
        T Data { get; }
    }

    /// <summary>
    ///     Represents the result of a web request with no response data
    ///     <para>
    ///         If the request was successful <see cref="IsSuccess" /> is true.
    ///         If the request failed <see cref="IsSuccess" /> is false and <see cref="ErrorMessage" /> is maybe filled with a description of the error.
    ///     </para>
    /// </summary>
    public interface IWebResult
    {
        string ErrorMessage { get; }

        bool IsSuccess { get; }
    }
}