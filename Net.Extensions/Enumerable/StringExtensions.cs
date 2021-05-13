using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Net.Extensions
{
    public static class StringExtensions
    {
        static readonly Regex CamelCaseRegex = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        static Regex DashCaseRegex = new Regex(@"([A-Z][a-z])|([a-z][A-Z])", RegexOptions.Compiled);
        static CultureInfo EnglishCulture = new CultureInfo("en-US",false);
        static Dictionary<char, char> turkishCharset = new Dictionary<char, char>()
        {
            { 'ç','c' },
            { 'ş','s' },
            { 'ı','i' },
            { 'ö','o' },
            { 'ü','u' },
            { 'ğ','g' },
            { 'Ç','c' },
            { 'Ş','s' },
            { 'İ','i' },
            { 'Ö','o' },
            { 'Ü','u' },
            { 'Ğ','g' },
            { 'I','i' },
        };
        public static string ConvertTurkishChars(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            var newChars = s.ToCharArray();
            for (int i = 0; i < newChars.Length; i++)
            {
                if (!turkishCharset.ContainsKey(newChars[i])) continue;
                newChars[i] = turkishCharset[newChars[i]];
            }
            return new string(newChars);

        }
        public static bool IsCaseInsensitiveContains(this string a, string b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return CultureInfo.CurrentCulture
                .CompareInfo.IndexOf(a.ConvertTurkishChars(), b.ConvertTurkishChars(), CompareOptions.IgnoreCase) >= 0;

        }
        public static bool IsCaseInsensitiveEqual(this string a, string b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return CultureInfo.CurrentCulture
                .CompareInfo.Compare(a.ConvertTurkishChars(), b.ConvertTurkishChars(), CompareOptions.IgnoreCase) == 0;

        }
        public static bool IsCaseInsensitiveStartsWith(this string a, string b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return CultureInfo.CurrentCulture
                .CompareInfo.IndexOf(a.ConvertTurkishChars(), b.ConvertTurkishChars(), CompareOptions.IgnoreCase) == 0;

        }

        public static bool IsCaseInsensitiveEndsWith(this string a, string b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (b.Length > a.Length) return false;

            return CultureInfo.CurrentCulture
                .CompareInfo.IndexOf(a.ConvertTurkishChars(), b.ConvertTurkishChars(), CompareOptions.IgnoreCase) == a.Length - b.Length;
        }

        public static string ToUpperFirstLetter(this string source,bool isInvariant=false)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] =isInvariant? char.ToUpperInvariant(letters[0]): char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
        public static string ToLowerFirstLetter(this string source, bool isInvariant = false)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // lower case the first char
            letters[0] = isInvariant ? char.ToLowerInvariant(letters[0]) : char.ToLower(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        public static string[] NormalizeToLowerStringArray(this IEnumerable<string> items)
        {
            if (items == null) return new string[] { };
            return items.Select(p => p?.Trim().ToLowerInvariant()).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
        }
        public static bool IsEmpty(this string src)
        {
            return string.IsNullOrWhiteSpace(src);
        }
        public static bool IsUpper(this string src)
        {
            if (src.IsEmpty()) return true;
            return src.All(p => char.IsUpper(p));
        }
        public static bool IsLower(this string src)
        {
            if (src.IsEmpty()) return true;
            return src.All(p => char.IsLower(p));
        }
        public static string ToStringJoin<T>(this IEnumerable<T> items,Func<T,string> property,string seperator = ",")
        {
            if (items == null || !items.Any()) return string.Empty;
            return items.Select(property).Aggregate((pre, next) => $"{pre}{seperator}{next}");
            
        }
        public static string ToDashCase(this string str)
        {
            var result=DashCaseRegex.Replace(str, delegate (Match match)
            {
                string v = match.ToString();
                return char.IsUpper(v[0])? "-" + v:new string(new[] { v[0], '-', v[1] });
            }).ToLowerInvariant();
            return result.StartsWith("-") ? result.Substring(1) : result;
        }
        public static string ToWordsCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
        }
        public static string ToCamelCase(this string str)
        {
            if (str.IsEmpty()) return str;
            string SimpleCamelCase(string txt) {
                return new string(
                EnglishCulture.TextInfo
                    .ToTitleCase(
                      string.Join(" ", CamelCaseRegex.Matches(txt)).ToLower()
                    )
                    .Replace(@" ", "")
                    .Select((x, i) => i == 0 ? char.ToLower(x) : x)
                    .ToArray());
            }
            return str.Split(".").Select(p => SimpleCamelCase(p)).ToJoinedString(".");
           
        }
        public static string  DashToCamelCase(this string input)
        {
            StringBuilder sb = new StringBuilder();
            bool caseFlag = false;
            for (int i = 0; i < input.Length; ++i)
            {
                char c = input[i];
                if (c == '-')
                {
                    caseFlag = true;
                }
                else if (caseFlag)
                {
                    sb.Append(char.ToUpper(c));
                    caseFlag = false;
                }
                else
                {
                    sb.Append(char.ToLower(c));
                }
            }
            return sb.ToString();
        }
        public static string ToPointCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "." + x.ToString() : x.ToString())).ToLower();
        }
        public static string ToReversePointCase(this string str)
        {
            var pointCase = str.ToPointCase();
            return pointCase.SplitBy(".").Reverse().Aggregate((pre, next) => $"{pre}.{next}");
        }
        public static  string[] SplitBy(this string text,string seperator)
        {
            if (text.IsEmpty()) return new string[] { };
            return text.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string TrimThenBy(this string str,string seperator)
        {
            if (str.IsEmpty()) return str;
            var index = str.IndexOf(seperator);
            return index < 0 ? str : index ==0 ? string.Empty : str.Substring(0,index);
        }
        public static string TrimLeftBy(this string str, string seperator)
        {
            if (str.IsEmpty()) return str;
            var index = str.IndexOf(seperator);
            return index < 0 ? str : str.Substring(index+seperator.Length);
        }
    }
}
