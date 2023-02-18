using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entiries
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // horizontal
        public string? Reference { get; set; }// horizontal
        public string? Extension { get; set; }// horizontal
        public DateTime Creation { get; set; }// horizontal HH:mm dd/MM
        public double Aggression { get; set; } // vertical
        public double Arousal { get; set; }// vertical
        public double Atmosphere { get; set; }// vertical
        public double ClStress { get; set; }// vertical
        public double Concentration { get; set; }// vertical
        public double Discomfort { get; set; }// vertical
        public double Excitement { get; set; }// vertical
        public double Hesitation { get; set; }// vertical
        public double Imagination { get; set; }// vertical
        public double Joy { get; set; }// vertical
        public double MentalEffort { get; set; }// vertical
        public double Sad { get; set; }// vertical
        public double Stress { get; set; }// vertical
        public double Uncertainty { get; set; }// vertical
        public double Uneasy { get; set; }// vertical
        public double DiscomfortEnd { get; set; }// vertical
        public double DiscomfortStart { get; set; }// vertical
        public string? Tags { get; set; }// horizontal
        public string NewTagsString { get; set; }// horizontal
    }
}
