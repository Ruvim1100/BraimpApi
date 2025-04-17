namespace Braimp.Application.Abstraction
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
