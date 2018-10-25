namespace Lizzards.Data.CQRS.DataAccess
{
  using System;
  using Lizzards.Data.Domain;

  public interface IDataContext : IDisposable
  {
    IDataReader<T> GetReader<T>()
      where T : IAggregateRoot;

    IDataWriter<T> GetWriter<T>()
      where T : IAggregateRoot;
  }
}
