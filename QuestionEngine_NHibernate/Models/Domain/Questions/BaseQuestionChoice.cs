using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class BaseQuestionChoice: IEntity
    {
        public virtual int Id { get; set; }
        public virtual BaseQuestion BaseQuestion { get; set; }
        public virtual int ChoiceId { get; set; }
        public virtual string Text { get; set; }

        public virtual void Update(BaseQuestion baseQuestion, int choiceId, string choiceText)
        {
            BaseQuestion = baseQuestion;
            ChoiceId = choiceId;
            Text = choiceText;
        }
    }
}