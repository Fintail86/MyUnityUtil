using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public static class MyUtil
{
    public static bool IsNumericType<T>()
    {
        var type = typeof(T);
        return type == typeof(byte) ||
               type == typeof(sbyte) ||
               type == typeof(short) ||
               type == typeof(ushort) ||
               type == typeof(int) ||
               type == typeof(uint) ||
               type == typeof(long) ||
               type == typeof(ulong) ||
               type == typeof(float) ||
               type == typeof(double) ||
               type == typeof(decimal) ||
               type == typeof(System.Numerics.BigInteger);

        //return typeof(T).GetInterfaces().Contains(typeof(IConvertible))
        //    || typeof(T) == typeof(System.Numerics.Complex)
        //    || typeof(T) == typeof(System.Numerics.BigInteger)
        //    || typeof(T) == typeof(System.Numerics.Quaternion);
    }

    public static string GetThousandCommaText<T>(T value, bool useDecimalPoint = false, int decimalSize = 2)
    {
        if (IsNumericType<T>())
        {
            var convertedValue = Convert.ChangeType(value, typeof(double), CultureInfo.InvariantCulture);
            var stringFromat = useDecimalPoint ? $"N{decimalSize}" : "N0";

            return (double)convertedValue < 1 ? ((double)convertedValue).ToString(stringFromat, CultureInfo.CurrentCulture) : "0";
        }
        else
        {
            //MadDebug.LogError($"{typeof(T).Name}은 System.Numerics 이 아닙니다.");
            return value.ToString();
        }
    }

    public static string TostringTimeSpan(TimeSpan time)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)time.TotalHours, time.Minutes, time.Seconds);
    }
}