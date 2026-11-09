using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RaceDay_web.Models
{
    public class ResultsModel
    {
        [JsonPropertyName("result_id")]
        public int Result_Id { get; set; }

        [JsonPropertyName("finish_time")]
        [Required, Display(Name = "Finish time")]
        public string Finish_Time { get; set; } = "";

        [JsonPropertyName("race_Positions")]
        [Required, Display(Name = "Race position")]
        public string Race_Positions { get; set; } = "";

        [JsonPropertyName("Avg_pace")]
        [Required, Display(Name = "Average pace")]
        public string Avg_Pace { get; set; } = "";
    }
}