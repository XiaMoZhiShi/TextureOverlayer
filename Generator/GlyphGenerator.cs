using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TextureOverlayer.GenerateConfig;
using Color = SixLabors.ImageSharp.Color;
using Point = SixLabors.ImageSharp.Point;

// ReSharper disable AccessToDisposedClosure

namespace TextureOverlayer.Generator;

public class GlyphGenerator
{
    public void Generate(FileConfig fileConfig)
    {
        int index = 1;
        foreach (var tgc in fileConfig.TexGenConfigs)
        {
            Console.WriteLine($"TexGen ({index} / {fileConfig.TexGenConfigs.Length}): {tgc.BasePath}({tgc.BaseColor}) <- {tgc.OverlayPath}({tgc.OverlayColor})");
            Generate(tgc);
            index++;
        }
    }

    private readonly Point pointTopLeft = new Point
    {
        X = 0,
        Y = 0
    };

    public void Generate(TexGenConfig texGenConfig)
    {
        //基础图像
        var baseImage = Image.Load<Rgba32>(texGenConfig.BasePath);
        Image<Rgba32>? overlayImage = null;
        
        //Console.WriteLine("Get BaseImage");

        //覆盖层图像
        if (!string.IsNullOrEmpty(texGenConfig.OverlayPath) && File.Exists(texGenConfig.OverlayPath))
        {
            //Console.WriteLine("Get Overlay");
            overlayImage = Image.Load<Rgba32>(texGenConfig.OverlayPath);
        }

        //处理基本层
        overlayColor(baseImage, Color.ParseHex(texGenConfig.BaseColor));

        //处理覆盖层
        if (overlayImage != null)
        {
            overlayColor(overlayImage, Color.ParseHex(texGenConfig.OverlayColor));

            baseImage.Mutate(x =>
            {
                x.DrawImage(overlayImage, pointTopLeft, texGenConfig.OverlayOpacity);
            });
        }
 
        baseImage.Save(texGenConfig.OutputTarget);
        //overlayImage.Save(texGenConfig.OutputTarget + "_OVERLAY.png");
    }

    private void overlayColor(Image source, Color color)
    {
        source.Mutate(x =>
        {
            using var img = new Image<Rgba32>(source.Width, source.Height);
            img.Mutate(xx =>
            {
                xx.BackgroundColor(color);
                xx.DrawImage(source, pointTopLeft, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.DestAtop,
                    1);
            });

            x.DrawImage(img, pointTopLeft, PixelColorBlendingMode.Darken, 1);
            
            //img.Save($"/dev/shm/work/mask_{color}.png");
        });
    }
}