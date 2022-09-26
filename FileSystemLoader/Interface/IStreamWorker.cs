namespace FileSystemLoader.Interface
{
    public interface IStreamWorker : IFileWorker<MemoryStream>, IDisposable
    {
        void DeleteFile(string filePath);
    }
}
