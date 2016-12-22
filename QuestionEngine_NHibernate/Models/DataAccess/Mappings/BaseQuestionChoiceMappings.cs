using FluentNHibernate.Mapping;
using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.DataAccess.Mappings
{
    public class BaseQuestionChoiceMappings: ClassMap<BaseQuestionChoice>
    {
        public BaseQuestionChoiceMappings()
        {
            Id(x => x.Id);
            Map(x => x.ChoiceId);
            Map(x => x.Text);
            References(x => x.BaseQuestion);
        }
    }
}