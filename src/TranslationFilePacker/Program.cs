using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace TranslationFilePacker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================");
            Console.WriteLine("Translation File Packer For YiJingFramework.References.Zhouyi.Zhuan.");
            Console.WriteLine("Target translation file version: TR-1.");
            if (args.Length != 3 || (args[0].ToLower() is not "t" and not "x"))
            {
                Console.WriteLine("Format: <Type ('t'=tuan, 'x'=xiang)> <InputFile> <OutputFile>");
                Console.WriteLine("======================");
                return;
            }
            Console.WriteLine("======================");

            Console.WriteLine("Reading...");

            if(args[0] == "t")
            {
                var inp = File.ReadAllText(args[1]);
                JsonSerializerOptions options
                   = new JsonSerializerOptions() {
                       AllowTrailingCommas = true,
                       ReadCommentHandling = JsonCommentHandling.Skip,
                       WriteIndented = false,
                       Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                   };
                var translation = JsonSerializer.Deserialize<TuanTranslationFile>(
                         inp, options);

                Console.WriteLine("Checking...");
                if (translation.CheckAndWrite())
                {
                    Console.WriteLine("Writing...");
                    File.WriteAllText(args[2], JsonSerializer.Serialize(translation, options));
                    Console.WriteLine("Finished.");
                }

                Console.WriteLine("======================");
            }
            else
            {
                var inp = File.ReadAllText(args[1]);
                JsonSerializerOptions options
                   = new JsonSerializerOptions() {
                       AllowTrailingCommas = true,
                       ReadCommentHandling = JsonCommentHandling.Skip,
                       WriteIndented = false,
                       Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                   };
                var translation = JsonSerializer.Deserialize<XiangTranslationFile>(
                         inp, options);

                Console.WriteLine("Checking...");
                if (translation.CheckAndWrite())
                {
                    Console.WriteLine("Writing...");
                    File.WriteAllText(args[2], JsonSerializer.Serialize(translation, options));
                    Console.WriteLine("Finished.");
                }

                Console.WriteLine("======================");
            }
        }
    }
}
