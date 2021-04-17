using System;
using System.IO;
using System.Text.Json;
using YiJingFramework.References.Zhouyi.Exceptions;
using YiJingFramework.References.Zhouyi.Zhuan.Translations;

namespace YiJingFramework.References.Zhouyi.Zhuan
{
    /// <summary>
    /// 《象传》。
    /// Xiangzhuan.
    /// </summary>
    public sealed class XiangZhuan
    {
        private readonly XiangTranslationFile translation;

        private static readonly JsonSerializerOptions jsonSerializerOptions
            = new JsonSerializerOptions() {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

        /// <summary>
        /// 创建新实例。
        /// Initialize a new instance.
        /// </summary>
        /// <param name="translationFilePath">
        /// 翻译文件路径。
        /// Path of the translation file.
        /// </param>
        /// <exception cref="CannotReadTranslationException">
        /// 读取翻译失败。
        /// Cannot read the translation.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="translationFilePath"/> 为 <c>null</c> 。
        /// <paramref name="translationFilePath"/> is <c>null</c>.
        /// </exception>
        public XiangZhuan(string translationFilePath)
        {
            if (translationFilePath is null)
                throw new ArgumentNullException(nameof(translationFilePath));
            try
            {
                using (var stream = new StreamReader(translationFilePath))
                    this.translation = JsonSerializer.Deserialize<XiangTranslationFile>(
                        stream.ReadToEnd(), jsonSerializerOptions)!;
            }
            catch (IOException e)
            {
                throw new CannotReadTranslationException($"Cannot read translation file: {translationFilePath}", e);
            }
            catch (JsonException e)
            {
                throw new CannotReadTranslationException($"Invalid translation file: {translationFilePath}", e);
            }
            catch (ArgumentException e)
            {
                throw new CannotReadTranslationException($"Cannot read translation file: {translationFilePath}", e);
            }

            if (this.translation is null || !this.translation.Check())
                throw new CannotReadTranslationException($"Invalid translation file: {translationFilePath}");
        }

        /// <summary>
        /// 创建新实例。
        /// Initialize a new instance.
        /// </summary>
        /// <param name="translationStream">
        /// 翻译流。
        /// The stream of the translation.
        /// </param>
        /// <param name="leaveOpen">
        /// 是否在读取后保存流开启。
        /// Whether to keep the stream open after reading.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="translationStream"/> 为 <c>null</c> 。
        /// <paramref name="translationStream"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="CannotReadTranslationException">
        /// 读取翻译失败。
        /// Cannot read the translation.
        /// </exception>
        public XiangZhuan(Stream translationStream, bool leaveOpen = false)
        {
            if (translationStream is null)
                throw new ArgumentNullException(nameof(translationStream));
            try
            {
                using (var stream = new StreamReader(translationStream, null, true, -1, leaveOpen))
                    this.translation = JsonSerializer.Deserialize<XiangTranslationFile>(
                        stream.ReadToEnd(), jsonSerializerOptions)!;
            }
            catch (JsonException e)
            {
                throw new CannotReadTranslationException($"Invalid translation.", e);
            }

            if (this.translation is null || !this.translation.Check())
                throw new CannotReadTranslationException($"Invalid translation.");
        }

        /// <summary>
        /// 获取一卦的象辞。
        /// Get the text in Xiangzhuan of a hexagram.
        /// </summary>
        /// <param name="hexagram">
        /// 卦。
        /// The hexagram.
        /// </param>
        /// <returns>
        /// 象辞。
        /// The text in Xiangzhuan.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hexagram"/> 为 <c>null</c> 。
        /// <paramref name="hexagram"/> is <c>null</c>.
        /// </exception>
        public string this[ZhouyiHexagram hexagram]
        {
            get
            {
                if (hexagram is null)
                    throw new ArgumentNullException(nameof(hexagram));

                return this.translation.XiangTexts[hexagram.Index - 1].Text;
            }
        }

        /// <summary>
        /// 获取一爻的象辞。
        /// Get the text in Xiangzhuan of a line.
        /// </summary>
        /// <param name="line">
        /// 爻。
        /// The line.
        /// </param>
        /// <returns>
        /// 象辞。
        /// The text in Xiangzhuan.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="line"/> 为 <c>null</c> 。
        /// <paramref name="line"/> is <c>null</c>.
        /// </exception>
        public string this[ZhouyiHexagram.Line line]
        {
            get
            {
                if (line is null)
                    throw new ArgumentNullException(nameof(line));

                if (line.LineIndex is 0)
                    return line.LineAttribute is Core.LineAttribute.Yang ?
                        this.translation.XiangOfApplyNines : this.translation.XiangOfApplySixes;

                return this.translation.XiangTexts[line.From.Index - 1].Lines[line.LineIndex - 1];
            }
        }
    }
}
