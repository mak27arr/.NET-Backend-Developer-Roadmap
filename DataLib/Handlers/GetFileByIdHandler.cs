using DataLib.DataAccess;
using DataLib.Models;
using DataLib.Queries;
using MediatR;

namespace DataLib.Handlers
{
    public class GetFileByIdHandler : IRequestHandler<GetFileByIdQuery, FileModel>
    {
        private readonly IDataAccess _data;

        public GetFileByIdHandler(IDataAccess data)
        {
            _data = data;
        }

        public Task<FileModel> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            //var list = await _mediator.Send(new GetFilesListQuery());
            //return list.FirstOrDefault(x => x.Id == request.Id);
            return Task.FromResult(_data.GetFiles().First(x => x.Id == request.Id));
        }
    }
}
