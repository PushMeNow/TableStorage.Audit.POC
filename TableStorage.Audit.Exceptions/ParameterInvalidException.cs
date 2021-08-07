using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TableStorage.Audit.Exceptions
{
    [Serializable]
    public class ParameterInvalidException : ValidationException
    {
        public ParameterInvalidException([NotNull] string argName) : base(argName, $"{argName} is invalid.")
        {
        }
        
        public ParameterInvalidException(string message, IEnumerable<FieldError> errors) : base(message, errors)
        { 
        }
    }
}