using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RaceDay_web.Models
{
    public class EventsModel
    {
        [JsonPropertyName("event_id")]
        public int Event_Id { get; set; }

        [JsonPropertyName("event_name")]
        [Required, Display(Name = "Event name")]
        public string Event_Name { get; set; } = "";

        [JsonPropertyName("date")]
        [Required, DataType(DataType.Date)]
        public string Date { get; set; } = "";

        [JsonPropertyName("time")]
        [Required]
        public string Time { get; set; } = "";

        [JsonPropertyName("event_type")]
        [Required, Display(Name = "Event type")]
        public string Event_Type { get; set; } = "";

        [JsonPropertyName("event_description")]
        [Display(Name = "Description")]
        public string Event_Description { get; set; } = "";

        [JsonPropertyName("location")]
        [Required]
        public string Location { get; set; } = "";

        [JsonPropertyName("race_route")]
        [Display(Name = "Race route")]
        public string Race_Route { get; set; } = "";

        [JsonPropertyName("weather")]
        public string Weather { get; set; } = "";
    }
}
