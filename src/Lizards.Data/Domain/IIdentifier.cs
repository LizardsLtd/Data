
namespace Lizzards.Data.Domain
{
  using System;

  public interface IIdentifier : IEquatable<IIdentifier>, IComparable<IIdentifier>, IComparable
  {
    object Value { get; }
  }
}
