using FluentMigrator;

namespace QuestionEngine_NHibernate.Migrations
{
    [Migration(1)]
    public class AddBaseQuestions : Migration
    {
        public override void Up()
        {
            Create.Table("BaseQuestion")
                .WithIdColumn()
                .WithColumn("QuestionId").AsInt32()
                .WithColumn("Text").AsString();

            Create.Table("BaseQuestionChoice")
                .WithIdColumn()
                .WithColumn("ChoiceId").AsInt32()
                .WithColumn("Text").AsString()
                .WithColumn("BaseQuestion_Id").AsInt32().ForeignKey("FK_BaseQuestion_Id", "BaseQuestion", "Id");
        }

        public override void Down()
        {
        }
    }
}