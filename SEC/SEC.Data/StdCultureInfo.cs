namespace SEC.Data
{
    public class StdCultureInfo : ICulture
    {
        public int CultureId { get; set; } = -1;

        public int LanguageCode { get; set; }
    }
}
