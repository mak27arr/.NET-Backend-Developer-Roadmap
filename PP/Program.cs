using PP;
using System.Threading.Channels;

var option = new BoundedChannelOptions(10);
option.FullMode = BoundedChannelFullMode.Wait;
var chanel = Channel.CreateBounded<int>(option);
var p = Task.Run(() =>
{
    Task.WaitAll((new Producer(chanel.Writer)).Run());
});
var c = Task.Run(() =>
{
    Task.WaitAll(
        (new Consumer(chanel.Reader, "first")).Run(),
        (new Consumer(chanel.Reader, "second")).Run());
});
Task.WaitAll(p, c);

Console.ReadLine();
