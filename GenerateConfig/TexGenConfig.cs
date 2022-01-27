using Newtonsoft.Json;

namespace TextureOverlayer.GenerateConfig;

[Serializable]
public class TexGenConfig
{
    /// <summary>
    /// 基础图像位置（相对路径）
    /// </summary>
    [JsonProperty("base")]
    public string BasePath = string.Empty;

    /// <summary>
    /// 覆盖层图像位置（相对路径）
    /// </summary>
    [JsonProperty("overlay")]
    public string OverlayPath = string.Empty;

    /// <summary>
    /// 基础图像颜色
    /// </summary>
    [JsonProperty("base_color")]
    public string BaseColor = "#FFFFFF";

    /// <summary>
    /// 覆盖层图像颜色
    /// </summary>
    [JsonProperty("overlay_color")]
    public string OverlayColor = "#FFFFFF";

    /// <summary>
    /// 覆盖层透明度
    /// </summary>
    [JsonProperty("overlay_opacity")]
    public int OverlayOpacity = 1;

    /// <summary>
    /// 输出路径（相对位置）
    /// </summary>
    [JsonProperty("output_target")]
    public string OutputTarget = string.Empty;

    public override string ToString() => JsonConvert.SerializeObject(this);
}