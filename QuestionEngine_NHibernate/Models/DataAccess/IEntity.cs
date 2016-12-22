using System.ComponentModel.DataAnnotations;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}