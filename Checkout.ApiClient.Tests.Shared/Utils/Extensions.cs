using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string using OrdinalIgnoreCase.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="match"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string match, StringComparison comparison)
        {
            if (source == null) return false;
            return source.IndexOf(match, comparison) != -1;
        }

        /// <summary>
        /// Replace char in string at a specific index
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="newchar"></param>
        /// <returns></returns>
        public static string ReplaceAt(this string value, int index, char newChar)
        {
            if (value.Length <= index)
                return value;
            else
                return string.Concat(value.Select((c, i) => i == index ? newChar : c));
        }

        /// <summary>
        /// Replaces substring with repeated char in a determined interval
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="newchar"></param>
        /// <returns></returns>
        public static string Replace(this string value, int startIndex, int length, char newChar)
        {
            for (int i = startIndex; i < startIndex + length; i++)
                value = value.ReplaceAt(i, newChar);
            return value;
        }
    }
}
