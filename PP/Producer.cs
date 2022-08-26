
using System.Threading.Channels;

namespace PP
{
    internal class Producer
    {
        private ChannelWriter<int> _writer;

        public Producer(ChannelWriter<int> writer)
        {
            _writer = writer;
        }

        internal async Task Run()
        {
            var random = new Random();

            while(await _writer.WaitToWriteAsync())
            {
                var item = random.Next(150);
                _writer.WriteAsync(item);
                Console.WriteLine($"writing {item}");
                await Task.Delay(200);
            }
        }
    }
}
