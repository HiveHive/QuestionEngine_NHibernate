using FluentNHibernate.Cfg;
using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.DataAccess.Mappings
{
    public static class DbMappings
    {
        public static void Mappings(MappingConfiguration mappings)
        {
            mappings.FluentMappings.AddFromAssemblyOf<BaseQuestion>();
            mappings.FluentMappings.AddFromAssemblyOf<BaseQuestionChoice>();
        }
    }
}