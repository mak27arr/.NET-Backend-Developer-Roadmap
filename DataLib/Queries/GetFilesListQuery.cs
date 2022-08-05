using DataLib.Models;
using MediatR;

namespace DataLib.Queries
{
    public record GetFilesListQuery() : IRequest<List<FileModel>>;
}
