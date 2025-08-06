using System;

public static class HexEnum
{
    public static TEnum DefaultValue<TEnum>()
    {
        return Enum.IsDefined(typeof(TEnum), 0) ? (TEnum)(object)0 : default(TEnum);
    }
}