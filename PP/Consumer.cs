using System.Threading.Channels;

namespace PP
{
    internal class Consumer
    {
        private ChannelReader<int> _reader;
        private readonly string _name;

        public Consumer(ChannelReader<int> reader, string name)
        {
            _reader = reader;
            this._name = name;
        }

        internal async Task Run()
        {
            while (await _reader.WaitToReadAsync())
            {
                var item = _reader.ReadAsync();
                Console.WriteLine($"{_name} read {item}");
                Task.Delay(1000);
            }
        }
    }
}
