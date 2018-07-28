using System;
using System.Linq.Expressions;

namespace Lizzards.Data.CQRS.Queries
{
    public interface IWithSpecification<TResult, TPayload>
    {
        TResult WithSpecification(Expression<Func<TPayload, bool>> specification);
    }
}