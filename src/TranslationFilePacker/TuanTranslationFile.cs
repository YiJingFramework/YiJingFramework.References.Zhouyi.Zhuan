using System;
using System.Linq;

namespace TranslationFilePacker
{
    internal record TuanTranslationFile(string[] TuanTexts)
    {
        private static bool CheckLengthAndNullValue<T>(T[] array, int length) where T : class
        {
            return array is not null &&
                array.Length == length &&
                !array.Contains(null);
        }
        internal bool CheckAndWrite()
        {
            if(!CheckLengthAndNullValue(this.TuanTexts, 64))
            {
                Console.WriteLine("There should be the translation for the tuan texts of 64 hexagrams.");
                return false;
            }
            return true;
        }
    }
}
