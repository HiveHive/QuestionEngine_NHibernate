using System.Linq;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class QuestionAssembler
    {
        public Question Assemble(BaseQuestion baseQuestion)
        {
            var question = new Question();
            question.QuestionId = baseQuestion.QuestionId;
            question.Text = baseQuestion.Text;
            question.Choices = baseQuestion.Choices.Select(AssembleQuestionChoice).ToList();
            return question;
        }

        private QuestionChoice AssembleQuestionChoice(BaseQuestionChoice baseQuestionChoice)
        {
            var questionChoice = new QuestionChoice();
            questionChoice.ChoiceId = baseQuestionChoice.ChoiceId;
            questionChoice.Text = baseQuestionChoice.Text;
            return questionChoice;
        }
    }
}