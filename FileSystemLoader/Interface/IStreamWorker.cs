namespace FileSystemLoader.Interface
{
    internal interface IStreamWorker : IFileWorker<MemoryStream>, IDisposable
    {
    }
}
