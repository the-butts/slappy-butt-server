namespace SlappyButt.Api.DataTransferModels.Avatars
{
    using System.ComponentModel.DataAnnotations;
    using SlappyButt.Common.Constants;
    using SlappyButt.Common.Models;

    public class AvaratRequstModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxImageInfoOriginalName)]
        public string OriginalName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxImageInfoFileExtension)]
        [MinLength(ValidationConstants.MinImageInfoFileExtension)]
        public string OriginalExtension { get; set; }
        
        [Required]
        public byte[] ByteArrayContent { get; set; }

        public RawImage ToRawImage()
        {
            return new RawImage()
            {
                OriginalFileName = this.OriginalName,
                FileExtension = this.OriginalExtension,
                Content = this.ByteArrayContent
            };
        }
    }
}