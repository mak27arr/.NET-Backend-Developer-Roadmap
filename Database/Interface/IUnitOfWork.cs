namespace Database.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        Task SaveAsync();
    }
}
