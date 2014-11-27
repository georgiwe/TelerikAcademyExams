namespace Exam.RESTApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InputNumberDataModel
    {
        [Required]
        public string Number { get; set; }
    }
}