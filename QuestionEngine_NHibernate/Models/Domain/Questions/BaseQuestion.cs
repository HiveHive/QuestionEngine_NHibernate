using System.Collections.Generic;
using QuestionEngine_NHibernate.Models.DataAccess;
using QuestionEngine_NHibernate.Models.Domain.Exceptions;

namespace QuestionEngine_NHibernate.Models.Domain.Questions
{
    public class BaseQuestion: IEntity
    {
        public virtual int Id { get; set; }

        public virtual int QuestionId { get; set; }
        public virtual string Text { get; set; }
        public virtual IList<BaseQuestionChoice> Choices { get; set; }

        public BaseQuestion()
        {
            Choices = new List<BaseQuestionChoice>();
        }
        
        public virtual void Update(QuestionInputViewModel questionInputViewModel)
        {
            AssertQuestionIdIsValid(questionInputViewModel);

            QuestionId = questionInputViewModel.QuestionId;
            Text = questionInputViewModel.Text;
            UpdateQuestionChoices(questionInputViewModel.Choices);
        }

        private void AssertQuestionIdIsValid(QuestionInputViewModel questionInputViewModel)
        {
            var questionExists = BaseQuestionRepository.FindBaseQuestionByQuestionId(questionInputViewModel.QuestionId) != null;
            if (questionExists)
                throw new QuestionException("A question with question id '{0}' already exists.", questionInputViewModel.QuestionId);
        }

        private void UpdateQuestionChoices(IEnumerable<string> choiceTexts)
        {
            var choiceId = 1;
            foreach (var choiceText in choiceTexts)
            {
                Choices.Add(BaseQuestionRepository.CreateBaseQuestionChoice(this, choiceId++, choiceText));
            }
        }

    }
}