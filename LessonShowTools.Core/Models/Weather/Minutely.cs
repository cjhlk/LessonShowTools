using System.Text.Json.Serialization;

namespace LessonShowTools.Core.Models.Weather;

public class Minutely
{
    [JsonPropertyName("precipitation")] public MinutelyPrecipitation Precipitation { get; set; } = new();
}