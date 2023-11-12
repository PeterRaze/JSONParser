namespace JSONParser
{
    public static class Constants
    {
        public enum SpecialCharacter
        {
            COMMA = ',',
            COLON = ':',
            LEFT_BRACKET = '[',
            RIGHT_BRACKET = ']',
            LEFT_BRACE = '{',
            RIGHT_BRACE = '}',
            QUOTE = '"'
        }

        public static List<char> WhiteSpaces => new List<char>()
        {
            ' ', '\t', '\n', '\r', '\b'
        };

        public static List<char> SpecialCharacters => ((SpecialCharacter[])Enum.GetValues(typeof(SpecialCharacter))).Select(x => (char)x).ToList();
    }
}
