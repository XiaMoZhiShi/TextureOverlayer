using Newtonsoft.Json;

namespace TextureOverlayer.GenerateConfig;

[Serializable]
public class OverlayConfig
{
    /// <summary>
    /// 覆盖层图像位置（相对路径）
    /// </summary>
    [JsonProperty("path")]
    public string Path = string.Empty;

    /// <summary>
    /// 覆盖层图像颜色
    /// </summary>
    [JsonProperty("color")]
    public string Color = "#FFFFFF";

    /// <summary>
    /// 覆盖层透明度
    /// </summary>
    [JsonProperty("opacity")]
    public int Opacity = 1;

    /// <summary>
    /// 覆盖层位置（X）
    /// </summary>
    [JsonProperty("x")]
    public int X = 0;

    /// <summary>
    /// 覆盖层位置（Y）
    /// </summary>
    [JsonProperty("y")]
    public int Y = 0;
}