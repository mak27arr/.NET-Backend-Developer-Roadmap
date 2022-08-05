namespace FileSystemLoader.Interface
{
    internal interface IFileWorker<T>
    {
        Task<T> LoadFile(string path);

        Task WriteFile(T file, string path);
    }
}
