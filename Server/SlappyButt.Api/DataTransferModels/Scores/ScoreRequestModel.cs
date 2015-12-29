namespace SlappyButt.Api.DataTransferModels.Scores
{
    using System.ComponentModel.DataAnnotations;

    public class ScoreRequestModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Value { get; set; }
    }
}