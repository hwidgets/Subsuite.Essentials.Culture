namespace SEC.Data
{
    /// <summary>
    /// Defines what a « culture » is.
    /// </summary>
    public interface ICulture
    {
        /// <summary>
        /// Culture database identifier (primary key).
        /// </summary>
        int CultureId { get; set; }

        /// <summary>
        /// Culture <see cref="Data.Language.Code"/> or what
        /// truly identifies the unique <see cref="ICulture"/>.
        /// </summary>
        int LanguageCode { get; set; }
    }
}
