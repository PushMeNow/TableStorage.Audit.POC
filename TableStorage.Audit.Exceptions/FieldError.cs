namespace TableStorage.Audit.Exceptions
{
    public class FieldError
    {
        public string Field { get; }
        public string Message { get; }

        public FieldError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}