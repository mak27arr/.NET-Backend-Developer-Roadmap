namespace FileSystemLoader.Interface
{
    public interface IStreamWorker : IFileWorker<MemoryStream>, IDisposable
    {
        void DeleteFile(string filePath);
        Task<bool> SaveFile(string filePath, MemoryStream file);
    }
}
