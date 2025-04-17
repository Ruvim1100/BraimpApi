namespace Braimp.Domain.Abstraction
{
    public abstract class BaseEntity<TKey>
    {
        public required TKey Id { get; set; }
    }
}
