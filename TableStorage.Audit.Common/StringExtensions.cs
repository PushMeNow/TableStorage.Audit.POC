namespace TableStorage.Audit.Common
{
    public static class StringExtensions
    {
        public static bool IsNull(this string stringValue)
        {
            return stringValue is null;
        }

        public static bool IsNotNull(this string stringValue)
        {
            return !stringValue.IsNull();
        }

        public static bool IsNullOrEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNotNullOrEmpty(this string stringValue)
        {
            return !stringValue.IsNullOrEmpty();
        }

        public static bool IsNullOrWhiteSpace(this string stringValue)
        {
            return string.IsNullOrWhiteSpace(stringValue);
        }

        public static bool IsNotNullOrWhiteSpace(this string stringValue)
        {
            return !stringValue.IsNullOrWhiteSpace();
        }
    }
}