using System.Collections.Generic;

namespace SEC.Data
{
    /// <summary>
    /// A <see cref="Language"/> is a distinction object
    /// for the culture type. This is what identifies a
    /// <see cref="Culture"/> and makes it unique. This
    /// class  « translates » the db-stored <see cref="int"/>
    /// in such a way as it can be easily intelligible.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Matching code (English is 0 by default).
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Creates a new <see cref="Language"/>.
        /// </summary>
        /// <param name="code">See <see cref="Code"/>.</param>
        public Language(int code) => Code = code;
    }
    
    /// <summary>
    /// A <see cref="LanguageLister"/> centralizes current
    /// project languages list (ex: English, French...).
    /// </summary>
    public sealed class LanguageLister
    {
        /// <summary>
        /// Current project languages list.
        /// </summary>
        public Dictionary<string, Language> Languages { get; }

        /// <summary>
        /// Creates a new <see cref="LanguageLister"/> with default value.
        /// </summary>
        public LanguageLister()
            => Languages = new Dictionary<string, Language>
            {
                { "English", new Language(1) }
            };
    }
}
