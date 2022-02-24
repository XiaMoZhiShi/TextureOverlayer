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

    [JsonProperty("overlays")]
    public OverlayConfig[] Overlays;

    /// <summary>
    /// 基础图像颜色
    /// </summary>
    [JsonProperty("base_color")]
    public string BaseColor = "#FFFFFF";

    /// <summary>
    /// 输出路径（相对位置）
    /// </summary>
    [JsonProperty("output_target")]
    public string OutputTarget = string.Empty;

    [JsonProperty("output_targets")]
    public string[]? OutputTargets;

    public override string ToString() => JsonConvert.SerializeObject(this);
}