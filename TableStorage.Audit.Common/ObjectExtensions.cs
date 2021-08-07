using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TableStorage.Audit.Common
{
    public static class ObjectExtensions
    {
        public static bool IsNotEmptyOrDefault<T>(this T value)
        {
            return !value.IsEmptyOrDefault();
        }
        
        public static bool IsEmptyOrDefault<T>(this T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                return true;
            }

            switch (value)
            {
                case string str when string.IsNullOrWhiteSpace(str):
                    return true;
                case ICollection { Count: 0 }:
                case Array { Length: 0 }:
                case IEnumerable e when !e.Cast<object>()
                                          .Any():
                    return true;
            }

            return false;
        }
    }
}