using DataLib.Commands;
using DataLib.DataAccess;
using DataLib.Models;
using MediatR;

namespace DataLib.Handlers
{
    public class InserFileHandler : IRequestHandler<InserFileCommand, FileModel>
    {
        private readonly IDataAccess _data;

        public InserFileHandler(IDataAccess data)
        {
            _data = data;
        }
        public Task<FileModel> Handle(InserFileCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.InsertFile(request.Name, request.Path));
        }
    }
}
