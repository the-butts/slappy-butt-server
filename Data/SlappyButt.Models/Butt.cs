namespace SlappyButt.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SlappyButt.Common.Constants;

    public class Butt
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinButtNameLength)]
        [MaxLength(ValidationConstants.MaxButtNameLength)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
