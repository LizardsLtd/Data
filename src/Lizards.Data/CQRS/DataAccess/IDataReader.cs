namespace Lizzards.Data.CQRS.DataAccess
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Lizzards.Maybe;

  public interface IDataReader<TPayload>
  {
    Task<IQueryable<TPayload>> Collection(Func<TPayload, bool> predicate);

    Task<Maybe<TPayload>> Single(Func<TPayload, bool> predicate, Func<IEnumerable<TPayload>, TPayload> reduce);
  }
}
