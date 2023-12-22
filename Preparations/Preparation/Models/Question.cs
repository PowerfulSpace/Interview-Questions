using System.ComponentModel;

namespace Preparation.Models
{
    public class Question
    {
        Guid Id { get; set; }
        [DisplayName("Вопросс")]
        string Name { get; set; } = string.Empty;

        [DisplayName("Ответ")]
        string Answer { get; set; } = string.Empty;

        [DisplayName("Тема")]
        string Subject { get; set; } = string.Empty;
    }
}
