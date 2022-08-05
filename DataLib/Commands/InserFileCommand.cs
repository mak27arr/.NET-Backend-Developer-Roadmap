using DataLib.Models;
using MediatR;

namespace DataLib.Commands
{

    public record InserFileCommand(string Name, string Path) : IRequest<FileModel>;

    //public class InserFileCommand : IRequest<FileModel>
    //{
    //    public string Name { get; set; }
    //    public string Path { get; set; }

    //    public InserFileCommand(string name, string path)
    //    {
    //        Name = name;
    //        Path = path;
    //    }
    //}
}
