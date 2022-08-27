namespace FileSystemLoader.Interface
{
    public interface IFileWorker<T>
    {
        Task<T> LoadFile(string path);

        Task WriteFile(T file, string path);
    }
}
