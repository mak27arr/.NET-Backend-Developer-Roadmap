using FileSystemLoader.Loaders;
using Xunit;

namespace FileSystemLoader.Tests
{
    public class CacheStreamLoaderTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var worker = new StreamWorker();
            var obj = new CacheStreamWorker(worker);
            Assert.NotNull(obj);
        }

        [Fact]
        public async Task AddFileWithIncorectPathTestAsync()
        {
            var worker = new StreamWorker();
            var obj = new CacheStreamWorker(worker);
            MemoryStream ms = new MemoryStream();
            ms.WriteByte(1);
            await Assert.ThrowsAsync<InvalidOperationException>(() => obj.WriteFile(ms, string.Empty));
            await Assert.ThrowsAsync<InvalidOperationException>(() => obj.WriteFile(ms, ":\\d"));
            await Assert.ThrowsAsync<InvalidOperationException>(() => obj.WriteFile(ms, "dd:\\\\!~asd\\\\sdf"));
        }

        [Fact]
        public async Task AddEmptyFileTestAsync()
        {
            var worker = new StreamWorker();
            var obj = new CacheStreamWorker(worker);
            await Assert.ThrowsAsync<InvalidOperationException>(() => obj.WriteFile(null, string.Empty));
        }

        [Fact]
        public void AddNotExistPathTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CheackCacheFileTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CheackReadFileTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CheackDisposeMemoryStreamTest()
        {
            throw new NotImplementedException();
        }
    }
}
