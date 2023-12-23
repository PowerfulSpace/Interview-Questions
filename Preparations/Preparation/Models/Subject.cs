using System.ComponentModel;

namespace Preparation.Models
{
    public class Subject
    {
        Guid Id { get; set; }
        List<Question> Questions { get; set; }
    }
}
