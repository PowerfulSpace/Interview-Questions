using System.ComponentModel;

namespace Preparation.Models
{
    public class Question
    {
        Guid Id { get; set; }

        [DisplayName("Вопросс")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Ответ")]
        public string Answer { get; set; } = string.Empty;


        [DisplayName("Тема")]
        public Guid? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
