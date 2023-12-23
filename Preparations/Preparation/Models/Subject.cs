using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preparation.Models
{
    public class Subject
    {
        public Guid Id { get; set; }

        [DisplayName("Тема")]
        public string Name { get; set; } = string.Empty;


        [Display(Name = "Вопросы")]
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
