using System;
using System.IO;
using System.Text.Json;
using YiJingFramework.References.Zhouyi.Exceptions;
using YiJingFramework.References.Zhouyi.Zhuan.Translations;

namespace YiJingFramework.References.Zhouyi.Zhuan
{
    /// <summary>
    /// 《彖传》。
    /// Tuanzhuan.
    /// </summary>
    public sealed class Tuanzhuan
    {
        private readonly TuanTranslationFile translation;

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
        public Tuanzhuan(string translationFilePath)
        {
            if (translationFilePath is null)
                throw new ArgumentNullException(nameof(translationFilePath));
            try
            {
                using (var stream = new StreamReader(translationFilePath))
                    this.translation = JsonSerializer.Deserialize<TuanTranslationFile>(
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
        public Tuanzhuan(Stream translationStream, bool leaveOpen = false)
        {
            if (translationStream is null)
                throw new ArgumentNullException(nameof(translationStream));
            try
            {
                using (var stream = new StreamReader(translationStream, null, true, -1, leaveOpen))
                    this.translation = JsonSerializer.Deserialize<TuanTranslationFile>(
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
        /// 获取一卦的彖辞。
        /// Get the text in Tuanzhuan of a hexagram.
        /// </summary>
        /// <param name="hexagram">
        /// 卦。
        /// The hexagram.
        /// </param>
        /// <returns>
        /// 彖辞。
        /// The text in Tuanzhuan.
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

                return this.translation.TuanTexts[hexagram.Index - 1];
            }
        }
    }
}
