using System.Collections.Generic;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<QuestionChoice> Choices { get; set; }
    }
}