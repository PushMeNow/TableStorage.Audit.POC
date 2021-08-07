using TableStorage.Audit.Common;

namespace TableStorage.Audit.Exceptions
{
    public static class Guard
    {
        public static void ThrowObjectNotFoundIfEmpty<T>(T value, string objectName)
        {
            if (value.IsEmptyOrDefault())
            {
                throw new ObjectNotFoundException(objectName);
            }
        }

        public static void ThrowParameterInvalidIfEmpty<T>(T value, string paramName)
        {
            if (value.IsEmptyOrDefault())
            {
                throw new ParameterInvalidException(paramName);
            }
        }
    }
}