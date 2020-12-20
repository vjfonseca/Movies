using System;

namespace Util
{
    class Enumerator
    {
        public static T toEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public static bool enumIsDefined<T>(string value)
        {
            return Enum.IsDefined(typeof(T), value);
        }
    }
}