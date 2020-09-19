using System;

namespace Madman.Games.Utilities
{
    public class TypeHelper
    {

        public static T GetRandomTypeName<T>()
        {
            System.Random random = new System.Random();
            Array arr = Enum.GetValues(typeof(T));
            int size = arr.Length;
            int randomOrdinal = random.Next(size);

            return (T)arr.GetValue(randomOrdinal);
        }
    }
}