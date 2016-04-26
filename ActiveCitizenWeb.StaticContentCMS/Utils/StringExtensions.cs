namespace ActiveCitizenWeb.StaticContentCMS.Utils
{
    public static class StringExtensions
    {
        public static string Cut(this string s, int maxLength)
        {
            if (s == null) return null;

            if (s.Length <= maxLength) return s;
            return s.Substring(0, maxLength) + @"...";
        }
    }
}