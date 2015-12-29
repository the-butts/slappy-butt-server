 namespace SlappyButt.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Avatar
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int ImageInfoId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ImageInfo ImageInfo { get; set; }
    }
}
