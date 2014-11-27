namespace ExamASP.NET.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
