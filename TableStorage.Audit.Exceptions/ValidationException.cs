using System;
using System.Collections.Generic;
using System.Linq;
using TableStorage.Audit.Common;

namespace TableStorage.Audit.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        private Dictionary<string, FieldError> _errors = new();

        public IReadOnlyDictionary<string, FieldError> Errors => _errors;

        public ValidationException(string field, string message) : this(message,
                                                                        new[]
                                                                        {
                                                                            new FieldError(field, message)
                                                                        })
        {
        }

        public ValidationException(string message, IEnumerable<FieldError> errors) : base(message)
        {
            if (errors.IsEmptyOrDefault())
            {
                return;
            }

            _errors = errors.ToDictionary(q => q.Field);
        }

        protected ValidationException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}