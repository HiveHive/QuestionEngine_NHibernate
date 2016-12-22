using System.Collections.Generic;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class QuestionInputViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<string> Choices { get; set; }

        public QuestionInputViewModel()
        {
            Choices = new List<string>();
        }
    }
}