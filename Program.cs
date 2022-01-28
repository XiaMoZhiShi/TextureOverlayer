using Newtonsoft.Json;
using TextureOverlayer.GenerateConfig;
using TextureOverlayer.Generator;

namespace TextureOverlayer;

public static class Program
{
    public static readonly JsonSerializerSettings? SerializerSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore
    };

    public static void Main(string?[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("没有参数");
            Environment.Exit(1);
        }

        int index = 0;
        string? inputFilePath = null;

        foreach (var arg in args)
        {
            switch (arg)
            {
                case "--input":
                    if (index + 1 >= args.Length)
                        throw new ArgumentException("参数不足");

                    inputFilePath = args[index + 1];
                    break;
            }

            index++;
        }

        if (string.IsNullOrEmpty(inputFilePath))
            throw new InvalidOperationException("文件地址未给定");

        if (!File.Exists(inputFilePath))
            throw new InvalidOperationException("文件不存在");

        var rawContent = File.ReadAllText(inputFilePath);
        var fileConfig = JsonConvert.DeserializeObject<FileConfig>(rawContent, SerializerSettings)!;

        var generator = new TextureGenerator();
        generator.Generate(fileConfig);
    }
}