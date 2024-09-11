namespace AGSR.Domain.Entities;

public class BaseEntity: BaseEntity<Guid>
{
}

public class BaseEntity<T> where T: struct
{
    public T Id { get; init; }
}