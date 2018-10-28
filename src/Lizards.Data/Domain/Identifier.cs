
namespace Lizzards.Data.Domain
{
  using System;

  public sealed class Identifier<TType> : IIdentifier
    where TType : IEquatable<TType>, IComparable<TType>, IComparable
  {
    private readonly TType value;

    public Identifier(TType value) => this.value = value;

    public object Value => value;

    public int CompareTo(IIdentifier other) => value.CompareTo(other?.Value);

    public int CompareTo(object obj) => CompareTo(obj as IIdentifier);

    public bool Equals(IIdentifier other) => value.Equals(other?.Value);
  }
}
