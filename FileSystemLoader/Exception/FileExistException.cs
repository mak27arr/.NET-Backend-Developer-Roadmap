namespace FileSystemLoader.Exception
{
    internal class FileExistException : IOException
    {
        public FileExistException(string? message) : base(message)
        {
        }
    }
}
