using System;
using UnityEngine.Assertions;

namespace Hexagon
{
    public static class HexEnum
    {
        public static TEnum DefaultValue<TEnum>()
        {
            return Enum.IsDefined(typeof(TEnum), 0) ? (TEnum)(object)0 : default(TEnum);
        }

        public static TEnum Min<TEnum>() where TEnum : Enum
        {
            int i = 0;
            int min = int.MaxValue;
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                int v = Convert.ToInt32(value);
                if (v < min)
                {
                    min = v;
                }
                i++;
            }
            Assert.IsTrue(i > 0, $"Enum {typeof(TEnum).Name} must have at least one value defined.");
            return (TEnum)(object)min;
        }

        public static TEnum Max<TEnum>() where TEnum : Enum
        {
            int i = 0;
            int max = int.MinValue;
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                int v = Convert.ToInt32(value);
                if (v > max)
                {
                    max = v;
                }
                i++;
            }
            Assert.IsTrue(i > 0, $"Enum {typeof(TEnum).Name} must have at least one value defined.");
            return (TEnum)(object)max;
        }
    }
}