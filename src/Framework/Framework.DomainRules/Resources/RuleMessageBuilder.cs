namespace Framework.DomainRules.Resources
{
    internal static class RuleMessageBuilder
    {
        public static class StringRuleMessages
        {
            public static string NotEmpty(string source)
            {
                return string.Format(RuleMessages.StringRule_NotEmpty, source);
            }

            public static string NotEmptyMaxLength(string source, int maxLength)
            {
                return string.Format(RuleMessages.StringRule_NotEmptyMaxLength, source, maxLength);
            }

            public static string MinLength(string source, int minLength)
            {
                return string.Format(RuleMessages.StringRule_MinLength, source, minLength);
            }

            public static string MinMaxLength(string source, int minLength, int maxLength)
            {
                return string.Format(RuleMessages.StringRule_MinMaxLength, source, minLength, maxLength);
            }

            public static string MaxLength(string source, int maxLength)
            {
                return string.Format(RuleMessages.StringRule_MaxLength, source, maxLength);
            }
        }
    }
}
