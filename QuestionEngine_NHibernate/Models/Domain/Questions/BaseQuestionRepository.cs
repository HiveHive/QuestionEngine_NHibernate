using System.Linq;
using NHibernate;
using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class BaseQuestionRepository
    {
        public static void CreateBaseQuestion(QuestionInputViewModel questionInputViewModel)
        {
            var baseQuestion = new BaseQuestion();
            baseQuestion.Update(questionInputViewModel);
            TransactionManager.CurrentUnitOfWork().AddEntity(baseQuestion);
        }

        public static BaseQuestion FindBaseQuestionByQuestionId(int questionId)
        {
            return TransactionManager.CurrentUnitOfWork().Query<BaseQuestion>().Where(x => x.QuestionId == questionId).SingleOrDefault();
        }

        public static BaseQuestionChoice CreateBaseQuestionChoice(BaseQuestion baseQuestion, int choiceId, string choiceText)
        {
            var baseQuestionChoice = new BaseQuestionChoice();
            baseQuestionChoice.Update(baseQuestion, choiceId, choiceText);
            TransactionManager.CurrentUnitOfWork().AddEntity(baseQuestion);
            return baseQuestionChoice;
        }
    }
}