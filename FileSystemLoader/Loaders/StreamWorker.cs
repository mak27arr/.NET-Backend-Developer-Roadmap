using FileSystemLoader.Exception;
using FileSystemLoader.Interface;

namespace FileSystemLoader.Loaders
{
    internal class StreamWorker : IStreamWorker
    {
        public Task<MemoryStream> LoadFile(string path)
        {
            if (!IsValidPath(path))
                throw new InvalidDataException(path);

            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var result = new MemoryStream();

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
                file.CopyTo(result);

            return Task.FromResult(result);
        }

        public async Task WriteFile(MemoryStream file, string path)
        {
            if (!IsValidPath(path))
                throw new InvalidDataException(path);

            if (File.Exists(path))
                throw new FileExistException(path);

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
        }

        private bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                    return Path.IsPathRooted(path);
                else
                    return !string.IsNullOrEmpty(Path.GetPathRoot(path).Trim(new char[] { '\\', '/' }));
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
        }
    }
}
