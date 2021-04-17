using System;
using System.Linq;

namespace TranslationFilePacker
{
    internal record XiangTranslationFile(
        string XiangOfApplyNines, string XiangOfApplySixes,
        XiangTranslationFile.XiangTranslationFileHexagrams[] XiangTexts)
    {
        private static bool CheckLengthAndNullValue<T>(T[] array, int length) where T : class
        {
            return array is not null &&
                array.Length == length &&
                !array.Contains(null);
        }
        internal bool CheckAndWrite()
        {
            if (this.XiangOfApplyNines is null)
            {
                Console.WriteLine($"There should be the translation for the xiang text of the apply nines.");
                return false;
            }
            if (this.XiangOfApplySixes is null)
            {
                Console.WriteLine($"There should be the translation for the xiang text of the apply sixes.");
                return false;
            }
            if (!CheckLengthAndNullValue(this.XiangTexts, 64))
            {
                Console.WriteLine($"There should be the translation for the xiang text of the 64 hexagrams.");
                return false;
            }
            for(int i = 0; i < 64; i++)
            {
                if (!this.XiangTexts[i].Check(i))
                    return false;
            }
            return true;
        }
        internal record XiangTranslationFileHexagrams(string Text, string[] Lines)
        {
            internal bool Check(int i)
            {
                if (this.Text is null)
                {
                    Console.WriteLine($"There should be the translation for the xiang text of the hexagram {i + 1}.");
                    return false;
                }
                if(!CheckLengthAndNullValue(this.Lines, 6))
                {
                    Console.WriteLine($"There should be the translation for the xiang text of the 6 lines of hexagram {i + 1}.");
                    return false;
                }
                return true;
            }
        }
    }
}
