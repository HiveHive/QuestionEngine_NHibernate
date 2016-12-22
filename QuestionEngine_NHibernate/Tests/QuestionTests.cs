using System;
using FluentAssertions;
using NUnit.Framework;
using QuestionEngine_NHibernate.Models.Domain.Exceptions;
using QuestionEngine_NHibernate.Models.Domain.Questions;
using QuestionEngine_NHibernate.Models.Facade;

namespace QuestionEngine_NHibernate.Tests
{
    public class QuestionTests: QuestionEngineTests
    {
        [Test]
        public void CreateQuestion()
        {
            // assemble
            var questionInputViewModel = new QuestionInputViewModel();
            questionInputViewModel.QuestionId = 1;
            questionInputViewModel.Text = "Do you like kittens?";
            questionInputViewModel.Choices.Add("Yes");
            questionInputViewModel.Choices.Add("No");

            // act
            FacadeFactory.GetDomainFacade().CreateQuestion(questionInputViewModel);

            // assert
            var question = FacadeFactory.GetDomainFacade().FindQuestionByQuestionId(questionInputViewModel.QuestionId);
            question.Should().NotBeNull();
            question.QuestionId.ShouldBeEquivalentTo(questionInputViewModel.QuestionId);
            question.Text.ShouldBeEquivalentTo(questionInputViewModel.Text);
            question.Choices.Count.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void CreateQuestion_QuestionWithQuestionIdAlreadyExists()
        {
            // assemble
            var questionInputViewModel = new QuestionInputViewModel();
            questionInputViewModel.QuestionId = 1;
            questionInputViewModel.Text = "Do you like kittens?";
            questionInputViewModel.Choices.Add("Yes");
            questionInputViewModel.Choices.Add("No");

            FacadeFactory.GetDomainFacade().CreateQuestion(questionInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateQuestion(questionInputViewModel);

            // assert
            act.ShouldThrow<QuestionException>().WithMessage("A question with question id '1' already exists.");
        }
    }
}