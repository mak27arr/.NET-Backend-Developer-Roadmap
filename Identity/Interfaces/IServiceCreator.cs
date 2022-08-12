namespace Identity.Interfaces
{
    internal interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
