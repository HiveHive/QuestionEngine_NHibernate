using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.Facade
{
    public class FacadeFactory
    {
        public static DomainFacade GetDomainFacade()
        {
            return new DomainFacade();
        }
    }
}