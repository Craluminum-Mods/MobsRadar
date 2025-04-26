using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;

namespace MobsRadar.Configuration;

public class MobsRadarConfig : IModConfig
{
    public const string ConfigName = "MobsRadarConfig.json";

    [JsonProperty(Order = 1)]
    public bool AutoFill { get; set; } = true;

    [JsonProperty(Order = 2)]
    public string Description => "Set AutoFill to false to prevent the config from resetting to default values";

    [JsonProperty(Order = 3)]
    public int RefreshRate { get; set; } = 1000;
    
    [JsonProperty(Order = 4)]
    public int HorizontalRadius { get; set; } = 999;
    
    [JsonProperty(Order = 5)]
    public int VerticalRadius { get; set; } = 20;

    [JsonProperty(Order = 6)]
    public Dictionary<string, EntityMark> Markers { get; set; } = new();

    public MobsRadarConfig(ICoreAPI api, MobsRadarConfig previousConfig)
    {
        if (previousConfig != null)
        {
            foreach ((string key, EntityMark value) in previousConfig.Markers.Where(keyVal => !Markers.ContainsKey(keyVal.Key)))
            {
                Markers.Add(key, value);
            }

            AutoFill = previousConfig.AutoFill;
            RefreshRate = previousConfig.RefreshRate;
            HorizontalRadius = previousConfig.HorizontalRadius;
            VerticalRadius = previousConfig.VerticalRadius;
        }

        if (AutoFill)
        {
            FillDefault();
        }
    }

    private void FillDefault()
    {
        Markers.Clear();

        foreach ((string key, EntityMark value) in Core.DefaultMarkers.Where(keyVal => !Markers.ContainsKey(keyVal.Key)))
        {
            Markers.Add(key, value);
        }
    }
}