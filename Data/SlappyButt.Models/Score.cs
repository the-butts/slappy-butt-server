namespace SlappyButt.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int Value { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime SubmitedOn { get; set; }
    }
}
