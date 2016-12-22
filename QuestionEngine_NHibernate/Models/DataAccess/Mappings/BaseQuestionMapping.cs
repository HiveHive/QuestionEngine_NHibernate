using FluentNHibernate.Mapping;
using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.DataAccess.Mappings
{
    public class BaseQuestionMapping: ClassMap<BaseQuestion>
    {
        public BaseQuestionMapping()
        {
            Id(x => x.Id);
            Map(x => x.QuestionId);
            Map(x => x.Text);
            HasMany(x => x.Choices).Cascade.All();
        }
    }
}