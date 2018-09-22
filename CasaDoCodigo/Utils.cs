using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public static class Utils
    {
        public static string RemoverAcentos(this string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
        public static bool ContainsSearch(this string text, string text2)
        {
            var text01 = text.RemoverAcentos().Trim().ToUpper();
            var text02 = text2.RemoverAcentos().Trim().ToUpper();

            return text01.Contains(text02);
        }
    }
}
