namespace SEC.Data
{
    /// <summary>
    /// Standard implementation of <see cref="ICultureInfo"/>.
    /// </summary>
    public class StdCultureInfo : ICultureInfo
    {
        /// <summary>
        /// See <see cref="ICultureInfo.CultureId"/>.
        /// </summary>
        public int CultureId { get; set; } = -1;

        /// <summary>
        /// See <see cref="ICultureInfo.LanguageCode"/>.
        /// </summary>
        public int LanguageCode { get; set; }
    }
}
