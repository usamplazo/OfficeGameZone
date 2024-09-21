using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Scoreboard
    {
        [Key]
        public Guid ScoreboardId { get; set; }

        [Required]
        public Guid ContestantId { get; set; }

        [Required]
        public Guid DisciplineId { get; set; }

        [Required]
        // for better performance in data queries
        // format: yyyyMMdd
        public int DateDisciplinePlayed { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}
