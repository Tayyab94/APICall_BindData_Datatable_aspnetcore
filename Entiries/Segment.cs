using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entiries
{
    public class Segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Extension { get; set; }
        public DateTime Creation { get; set; }
        public string? SegmentsJSONObj { get; set; }
        public string? HeadersJSONObj { get; set; }
        public string? HeadersPositionsJSONObj { get; set; }
        public string? AnalyzeWholeJSONObj { get; set; }
    }
}
