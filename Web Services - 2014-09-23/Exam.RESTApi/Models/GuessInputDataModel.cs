namespace Exam.RESTApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GuessInputDataModel
    {
        [Required]
        public string Number { get; set; }
    }
}