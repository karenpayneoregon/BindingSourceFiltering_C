namespace ExtensionLibrary
{
    public static class StringExtensions
    {
        public static string EscapeApostrophe(this string pSender)
        {
            return pSender.Replace("'", "''");
        }
    }
}
