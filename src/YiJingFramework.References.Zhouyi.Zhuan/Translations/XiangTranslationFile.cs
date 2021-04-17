using System;
using System.Linq;

namespace YiJingFramework.References.Zhouyi.Zhuan.Translations
{
    internal record XiangTranslationFile(
        string XiangOfApplyNines, string XiangOfApplySixes,
        XiangTranslationFile.XiangTranslationFileHexagrams[] XiangTexts)
    {
        private static bool CheckLengthAndNullValue<T>(T?[] array, int length) where T : class
        {
            return array is not null &&
                array.Length == length &&
                !array.Contains(null);
        }
        internal bool Check()
        {
            if (this.XiangOfApplyNines is null || this.XiangOfApplySixes is null
                || (!CheckLengthAndNullValue(this.XiangTexts, 64)))
                return false;
            foreach (var t in this.XiangTexts)
                if (!t.Check())
                    return false;
            return true;
        }
        internal record XiangTranslationFileHexagrams(string Text, string[] Lines)
        {
            internal bool Check()
            {
                return this.Text is not null && CheckLengthAndNullValue(this.Lines, 6);
            }
        }
    }
}
