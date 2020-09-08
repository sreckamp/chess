using System;

namespace Chess.Model.Extensions
{
    public static class ArrayExtensions
    {
        public static T[][] TypedInitialize<T>(this T[][] array, int width, T value)
        {
            for (var y = 0; y < array.Length; y++)
            {
                array[y] = new T[width];
                for (var x = 0; x < array[y].Length; x++)
                {
                    array[y][x] = value;
                }
            }

            return array;
        }

        public static T[][] TypedInitialize<T>(this T[][] array, int width, Func<int, int, T> getValue)
        {
            for (var y = 0; y < array.Length; y++)
            {
                array[y] = new T[width];
                for (var x = 0; x < array[y].Length; x++)
                {
                    array[y][x] = getValue(x,y);
                }
            }

            return array;
        }

        public static T[][] DeepCopy<T>(this T[][] array)
        {
            return DeepCopy(array, arg => arg);
        }

        public static T[][] DeepCopy<T>(this T[][] array, Func<T,T> itemDeepCopy)
        {
            var target = new T[array.Length][];
            for (var y = 0; y < array.Length; y++)
            {
                target[y] = new T[array[y].Length];
                for (var x = 0; x < array[y].Length; x++)
                {
                    target[y][x] = itemDeepCopy(array[y][x]);
                }
            }

            return target;
        }
    }
}