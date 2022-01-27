using Newtonsoft.Json;

namespace TextureOverlayer.GenerateConfig;

[Serializable]
public class FileConfig
{
    [JsonProperty("configs")]
    public TexGenConfig[] TexGenConfigs = Array.Empty<TexGenConfig>();

    public override string ToString() => JsonConvert.SerializeObject(this);
}