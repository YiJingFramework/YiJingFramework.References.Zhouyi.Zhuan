using System;
using System.Linq;

namespace YiJingFramework.References.Zhouyi.Zhuan.Translations
{
    internal record TuanTranslationFile(string[] TuanTexts)
    {
        private static bool CheckLengthAndNullValue<T>(T?[] array, int length) where T : class
        {
            return array is not null &&
                array.Length == length &&
                !array.Contains(null);
        }
        internal bool Check()
        {
            return CheckLengthAndNullValue(this.TuanTexts, 64);
        }
    }
}
