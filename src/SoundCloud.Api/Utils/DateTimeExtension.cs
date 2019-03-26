using System;

namespace SoundCloud.Api.Utils
{
    internal static class DateTimeExtension
    {
        /// <summary>
        ///     Formats the given <paramref name="dateTime" /> with the DateTimePattern SoundCloud uses:
        ///     YYYY/MM/dd HH:mm:ss
        /// </summary>
        /// <param name="dateTime">Datetime to format</param>
        /// <returns>Formatted string</returns>
        internal static string ToSoundCloudString(this DateTime dateTime)
        {
            return dateTime.ToString(Settings.SoundCloudDateTimePattern);
        }
    }
}