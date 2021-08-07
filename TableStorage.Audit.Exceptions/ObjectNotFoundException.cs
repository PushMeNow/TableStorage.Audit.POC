using System;
using System.Diagnostics.CodeAnalysis;

namespace TableStorage.Audit.Exceptions
{
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException([NotNull] string objectName) : base($"{objectName} is not found.")
        {
        }
    }
}