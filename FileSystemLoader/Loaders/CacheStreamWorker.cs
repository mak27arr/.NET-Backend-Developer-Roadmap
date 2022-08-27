using FileSystemLoader.Interface;

namespace FileSystemLoader.Loaders
{
    public class CacheStreamWorker : IStreamWorker
    {
        private readonly IStreamWorker _streamLoader;
        private Dictionary<string, Tuple<DateTime, MemoryStream>> _cache = new Dictionary<string, Tuple<DateTime, MemoryStream>>();

        public CacheStreamWorker(IStreamWorker streamLoader)
        {
            _streamLoader = streamLoader;
        }

        public async Task<MemoryStream> LoadFile(string path)
        {
            if (!_cache.TryGetValue(path, out var value))
            {
                var file = await _streamLoader.LoadFile(path);
                value = new Tuple<DateTime, MemoryStream>(DateTime.Now, file);
                _cache.Add(path, value);
            }

            ClearCashe();

            return value.Item2;
        }

        public async Task WriteFile(MemoryStream file, string path)
        {
            await _streamLoader.WriteFile(file, path);

            if (!_cache.ContainsKey(path))
            {
                var value = new Tuple<DateTime, MemoryStream>(DateTime.Now.AddMinutes(10), file);
                _cache.Add(path, value);
            }

            ClearCashe();
        }

        private void ClearCashe()
        {
            if (_cache.Count > 10)
            {
                var itemToRemove = _cache.Where(item => item.Value.Item1 < DateTime.Now);

                foreach (var item in itemToRemove)
                {
                    item.Value.Item2?.Dispose();
                    _cache.Remove(item.Key);
                }
            }
        }

        public void DeleteFile(string filePath)
        {
            if (_cache.ContainsKey(filePath))
                _cache.Remove(filePath);

            _streamLoader.DeleteFile(filePath);
        }

        public void Dispose()
        {
            if (_cache != null)
                foreach (var item in _cache)
                    item.Value.Item2?.Dispose();
        }
    }
}
