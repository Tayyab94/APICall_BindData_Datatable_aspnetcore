namespace Application.Entiries.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; } // horizontal
        public string? Reference { get; set; }// horizontal
        public DateTime Creation { get; set; }// horizontal HH:mm dd/MM
        public double Aggression { get; set; } // vertical
        public double Arousal { get; set; }// vertical
    }

}
