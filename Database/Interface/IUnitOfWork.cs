namespace Database.Interface
{
    internal interface IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
