using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Hexagon
{
    public static class HexString
    {
        #region InplaceVariables

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format, string var1, string obj1)
        {
            return format.Replace($"{{{var1}}}", obj1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3,
            string obj3, string var4, string obj4, string var5, string obj5)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4, string var5, string obj5, string var6, string obj6)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5)
                .InplaceVariables(var6, obj6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4, string var5, string obj5, string var6, string obj6,
            string var7, string obj7)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5)
                .InplaceVariables(var6, obj6)
                .InplaceVariables(var7, obj7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4, string var5, string obj5, string var6, string obj6,
            string var7, string obj7, string var8, string obj8)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5)
                .InplaceVariables(var6, obj6)
                .InplaceVariables(var7, obj7)
                .InplaceVariables(var8, obj8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4, string var5, string obj5, string var6, string obj6,
            string var7, string obj7, string var8, string obj8, string var9, string obj9)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5)
                .InplaceVariables(var6, obj6)
                .InplaceVariables(var7, obj7)
                .InplaceVariables(var8, obj8)
                .InplaceVariables(var9, obj9);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string InplaceVariables(this string format,
            string var1, string obj1, string var2, string obj2, string var3, string obj3,
            string var4, string obj4, string var5, string obj5, string var6, string obj6,
            string var7, string obj7, string var8, string obj8, string var9, string obj9,
            string var10, string obj10)
        {
            return format
                .InplaceVariables(var1, obj1)
                .InplaceVariables(var2, obj2)
                .InplaceVariables(var3, obj3)
                .InplaceVariables(var4, obj4)
                .InplaceVariables(var5, obj5)
                .InplaceVariables(var6, obj6)
                .InplaceVariables(var7, obj7)
                .InplaceVariables(var8, obj8)
                .InplaceVariables(var9, obj9)
                .InplaceVariables(var10, obj10);
        }
        #endregion

        #region FormatVariables

        public static string FormatVariables(this string format,
            string var1, object obj1)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 }
        });
        }

        public static string FormatVariables(this string format,
        string var1, object obj1, string var2, object obj2, string var3, object obj3,
        string var4, object obj4, string var5, object obj5)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4, string var5, object obj5, string var6, object obj6)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 },
            { var6, obj6 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4, string var5, object obj5, string var6, object obj6,
            string var7, object obj7)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 },
            { var6, obj6 },
            { var7, obj7 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4, string var5, object obj5, string var6, object obj6,
            string var7, object obj7, string var8, object obj8)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 },
            { var6, obj6 },
            { var7, obj7 },
            { var8, obj8 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4, string var5, object obj5, string var6, object obj6,
            string var7, object obj7, string var8, object obj8, string var9, object obj9)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 },
            { var6, obj6 },
            { var7, obj7 },
            { var8, obj8 },
            { var9, obj9 }
        });
        }

        public static string FormatVariables(this string format,
            string var1, object obj1, string var2, object obj2, string var3, object obj3,
            string var4, object obj4, string var5, object obj5, string var6, object obj6,
            string var7, object obj7, string var8, object obj8, string var9, object obj9,
            string var10, object obj10)
        {
            return FormatVariables(format, new Dictionary<string, object>()
        {
            { var1, obj1 },
            { var2, obj2 },
            { var3, obj3 },
            { var4, obj4 },
            { var5, obj5 },
            { var6, obj6 },
            { var7, obj7 },
            { var8, obj8 },
            { var9, obj9 },
            { var10, obj10 }
        });
        }

        /// <summary>
        /// Replaces all variables in the format string using the provided dictionary.
        /// Supports standard .NET formatting (e.g., {var:P2}, {var:N}, etc.).
        /// If a variable is not found in the dictionary, it remains unchanged.
        /// </summary>
        /// <param name="format">The format string with placeholders like {var} or {var:format}.</param>
        /// <param name="variables">Dictionary of variable names and their associated values.</param>
        /// <returns>The formatted string with variables replaced.</returns>
        public static string FormatVariables(this string format, Dictionary<string, object> variables)
        {
            return Regex.Replace(format, @"\{(\w+)(:\w+)?\}", match =>
            {
                string varName = match.Groups[1].Value;
                string formatSpecifier = match.Groups[2].Value;

                if (variables.TryGetValue(varName, out object value))
                {
                    if (!string.IsNullOrEmpty(formatSpecifier))
                    {
                        string actualFormatSpecifier = formatSpecifier.Substring(1);
                        return string.Format($"{{0:{actualFormatSpecifier}}}", value);
                    }
                    else
                    {
                        return value?.ToString();
                    }
                }
                else
                {
                    return match.Value;
                }
            });
        }

        #endregion
    }
}