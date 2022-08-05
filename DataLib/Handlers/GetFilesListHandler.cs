using DataLib.DataAccess;
using DataLib.Models;
using DataLib.Queries;
using MediatR;

namespace DataLib.Handlers
{
    public class GetFilesListHandler : IRequestHandler<GetFilesListQuery, List<FileModel>>
    {
        private readonly IDataAccess _data;

        public GetFilesListHandler(IDataAccess data)
        {
            _data = data;
        }

        public Task<List<FileModel>> Handle(GetFilesListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetFiles());
        }
    }
}
