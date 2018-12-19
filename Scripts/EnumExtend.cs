using System;
using UnityEngine;

namespace it.amalfi.Pearl
{
    public static class EnumExtend
    {
        public static T GetRandom<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);
            int aux = Enum.GetNames(typeof(T)).Length;
            aux = UnityEngine.Random.Range(0, aux);
            return (T)Enum.ToObject(typeof(T), aux);
        }

        public static T GetInverse<T>(T value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum && Enum.GetNames(typeof(T)).Length == 2);
            byte index = Convert.ToByte(value);
            if (index == 1)
                index = 0;
            else
                index = 1;
            return (T)Enum.ToObject(typeof(T), index);
        }
    }

}