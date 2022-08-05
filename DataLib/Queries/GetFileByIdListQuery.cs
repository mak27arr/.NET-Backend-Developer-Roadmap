using DataLib.Models;
using MediatR;

namespace DataLib.Queries
{
    public record GetFileByIdQuery(int Id) : IRequest<FileModel>;
}
