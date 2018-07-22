namespace Lizards.Data.CQRS
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public interface IStory
    {
        IEnumerable<ValidationResult> Validate(ICommand command);
    }
}