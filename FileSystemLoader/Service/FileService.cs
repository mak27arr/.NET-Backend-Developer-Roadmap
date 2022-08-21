using DTO.File;
using FileSystemLoader.Interface;
using myCloudDAL.DAL.Interface;

namespace FileSystemLoader.Service
{
    internal class FileService : IFileService
    {
        private readonly IStreamWorker _streamWorker;
        private readonly IUnitOfWork _unityOfWork;

        public FileService(IStreamWorker streamWorker, IUnitOfWork unityOfWork)
        {
            _streamWorker = streamWorker;
            _unityOfWork = unityOfWork;
        }

        public Guid CreateFile(UserFileDTO fileInfo, MemoryStream file, bool createPreview)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PreviewFileDTO> FindPreviewFiles(Predicate<PreviewFileDTO> filter)
        {
            throw new NotImplementedException();
        }

        public MemoryStream GetFile(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<UserFileDTO> GetFiles()
        {
            throw new NotImplementedException();
        }

        public MemoryStream GetPreviewFile(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PreviewFileDTO> GetPreviewFiles()
        {
            throw new NotImplementedException();
        }
    }
}
