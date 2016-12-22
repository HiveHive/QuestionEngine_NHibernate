using System;

namespace QuestionEngine_NHibernate.Models.Domain.Exceptions
{
    public class QuestionException: Exception
    {
        public QuestionException(string message, params object[] parameterStrings): base(string.Format(message, parameterStrings))
        {
        }
    }
}