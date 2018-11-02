namespace Lizzards.Data.Domain
{
  using System;

  public sealed class Identifier<TType> : IIdentifier
    where TType : IEquatable<TType>, IComparable<TType>, IComparable
  {
    private readonly TType value;

    public Identifier(TType value) => this.value = value;

    public object Value => this.value;

    public static implicit operator Identifier<TType>(TType id) => new Identifier<TType>(id);

    public static implicit operator TType(Identifier<TType> id) => (TType)id.Value;

    public int CompareTo(IIdentifier other) => this.value.CompareTo(other?.Value);

    public int CompareTo(object obj) => this.CompareTo(obj as IIdentifier);

    public bool Equals(IIdentifier other) => this.value.Equals(other?.Value);

    public override bool Equals(object obj) => this.Value.Equals(obj);

    public override int GetHashCode() => this.Value.GetHashCode();
  }
}
