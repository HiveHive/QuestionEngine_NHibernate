using QuestionEngine_NHibernate.Models.DataAccess;
using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.Facade
{
    public class DomainFacade
    {
        public void CreateQuestion(QuestionInputViewModel questionInputViewModel)
        {
            TransactionManager.Execute(() =>
            {
                BaseQuestionRepository.CreateBaseQuestion(questionInputViewModel);
                return null;
            });
        }

        public Question FindQuestionByQuestionId(int questionId)
        {
            return TransactionManager.Execute(() =>
            {
                var baseQuestion = BaseQuestionRepository.FindBaseQuestionByQuestionId(questionId);
                return new QuestionAssembler().Assemble(baseQuestion);
            });
        }
    }
}