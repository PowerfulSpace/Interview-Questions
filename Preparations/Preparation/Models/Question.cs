using System.ComponentModel;

namespace Preparation.Models
{
    public class Question
    {
        public Guid Id { get; set; }

        [DisplayName("Вопросс")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Ответ")]
        public string Answer { get; set; } = string.Empty;


        
        public Guid? SubjectId { get; set; }
        [DisplayName("Тема")]
        public Subject? Subject { get; set; }
    }
}
