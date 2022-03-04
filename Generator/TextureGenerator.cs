using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TextureOverlayer.GenerateConfig;
using Color = SixLabors.ImageSharp.Color;
using Point = SixLabors.ImageSharp.Point;

// ReSharper disable AccessToDisposedClosure

namespace TextureOverlayer.Generator;

public class TextureGenerator
{
    public void Generate(FileConfig fileConfig)
    {
        int index = 1;
        foreach (var tgc in fileConfig.TexGenConfigs)
        {
            Console.WriteLine($"TexGen ({index} / {fileConfig.TexGenConfigs.Length}): {tgc.BasePath}({tgc.BaseColor}))");
            Generate(tgc);
            index++;
        }
    }

    public void Generate(TexGenConfig texGenConfig)
    {
        //基础图像
        var baseImage = Image.Load<Rgba32>(texGenConfig.BasePath);
        
        //Console.WriteLine("Get BaseImage");

        //处理基本层
        overlayColor(baseImage, Color.ParseHex(texGenConfig.BaseColor));

        foreach (var overlay in texGenConfig.Overlays)
        {
            //覆盖层图像
            if (string.IsNullOrEmpty(overlay.Path) || !File.Exists(overlay.Path))
                continue;
            
            Console.WriteLine($"Overlay {overlay.Path}: {overlay.Color}(X {overlay.X} | Y {overlay.Y})");

            //Console.WriteLine("Get Overlay");
            using var overlayImage = Image.Load<Rgba32>(overlay.Path);

            //处理覆盖层
            if (overlayImage == null)
                continue;

            overlayColor(overlayImage, Color.ParseHex(overlay.Color));

            baseImage.Mutate(x =>
            {
                x.DrawImage(overlayImage, new Point(overlay.X, overlay.Y), overlay.Opacity);
            });
        }

        if (texGenConfig.OutputTargets?.Length > 0 && !string.IsNullOrEmpty(texGenConfig.OutputTarget))
            throw new InvalidOperationException("为什么要同时设置output_target和output_targets?");

        var outputTargets = texGenConfig.OutputTargets?.ToList() ?? new List<string>();
        
        if (texGenConfig.OutputTargets == null && !string.IsNullOrEmpty(texGenConfig.OutputTarget))
            outputTargets.Add(texGenConfig.OutputTarget);

        foreach (var fname in outputTargets)
        {
            if (!Directory.Exists(fname)) Directory.CreateDirectory(Path.GetDirectoryName(fname));

            baseImage.Save(fname);   
        }
    }

    private void overlayColor(Image source, Color color)
    {
        source.Mutate(x =>
        {
            using var img = new Image<Rgba32>(source.Width, source.Height, color);

            x.DrawImage(img,
                new Point(0, 0),
                PixelColorBlendingMode.Multiply,
                PixelAlphaCompositionMode.SrcAtop,
                1);  

            //x.DrawImage(img, pointTopLeft, PixelColorBlendingMode.Darken, 1);
            //source.Save($"/dev/shm/work/" + $"mask_{color}_guid_{Guid.NewGuid()}.png");
        });
    }
}