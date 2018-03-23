using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{
    public interface IStory<T> : IStory
    {
        Task Execute(T payload);
    }

    public interface IStory
    {
        IEnumerable<ValidationResult> Validate(ICommand command);
    }
}