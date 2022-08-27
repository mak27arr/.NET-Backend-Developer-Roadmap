using DTO.File;
using FileSystemLoader.Interface;
using myCloudDAL.DAL.Interface;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using myCloudDAL.DAL.Entities.File;
using System.Linq.Expressions;

namespace FileSystemLoader.Service
{
    public class FileService : IFileService
    {
        private readonly IStreamWorker _streamWorker;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IConfigurationProvider _mapperConfig;
        private readonly IMapper _mapper;
        private readonly IPathGenerator _pathGenerator;
        private readonly IPreviewGenerator _prGenerator;

        public FileService(IStreamWorker streamWorker, IUnitOfWork unityOfWork, IConfigurationProvider mapperConfig, IPathGenerator pathGenerator, IPreviewGenerator prGenerator)
        {
            _streamWorker = streamWorker;
            _unityOfWork = unityOfWork;
            _mapperConfig = mapperConfig;
            _pathGenerator = pathGenerator;
            _prGenerator = prGenerator;
            _mapper = new Mapper(_mapperConfig);
        }

        public async Task<Guid> CreateFileAsync(Guid userID, UserFileDTO fileInfoDto, MemoryStream file, bool createPreview)
        {
            if (file == null)
                throw new ArgumentNullException("file cant be empty");

            var fileInfo = _mapper.Map<UserFile<Guid>>(fileInfoDto);
            fileInfo.Owner = userID;
            fileInfo.Created = DateTimeOffset.Now;
            fileInfo.FilePath = _pathGenerator.GeneratePath(fileInfo);
            await _streamWorker.SaveFile(fileInfo.FilePath, file);
            await _unityOfWork.FileRepository.CreateAsync(fileInfo);
            var filePreview = _prGenerator.Generate(fileInfo);
            await _unityOfWork.PreviewRepository.CreateAsync(filePreview);
            return fileInfo.Id;
        }

        public async Task<bool> DeleteFile(Guid userID, Guid id)
        {
            var file = await _unityOfWork.FileRepository.GetAsync(id);

            if (file.Owner != userID)
                throw new UnauthorizedAccessException("You don't have access to this file");

            _streamWorker.DeleteFile(file.PreviewFile.FilePath);
            _streamWorker.DeleteFile(file.FilePath);
            return await _unityOfWork.FileRepository.DeleteAsync(id);
        }

        public async Task<IQueryable<PreviewFileDTO>> FindPreviewFiles(Guid userID, Predicate<PreviewFileDTO> filter)
        {
            var expression = _mapper.Map<Expression<Predicate<PreviewFile<Guid>>>>(filter).Compile();
            var files = await _unityOfWork.PreviewRepository.FindAsync(x => x.File.Owner == userID && expression.Invoke(x));
            return files.ProjectTo<PreviewFileDTO>(_mapperConfig);
        }

        public async Task<MemoryStream> GetFile(Guid userID, Guid id)
        {
            var file = await GetFileEntityAsync(userID, id);
            return await _streamWorker.LoadFile(file.FilePath);
        }

        public async Task<UserFileDTO> GetFileInfo(Guid userID, Guid id)
        {
            var file = await GetFileEntityAsync(userID, id);
            return _mapper.Map<UserFileDTO>(file);
        }

        public async Task<IQueryable<UserFileDTO>> GetFilesInfo(Guid userID)
        {
            var files = await _unityOfWork.FileRepository.FindAsync(x => x.Owner == userID);
            return files.ProjectTo<UserFileDTO>(_mapperConfig);
        }

        public async Task<MemoryStream> GetPreviewFile(Guid userID, Guid id)
        {
            var file = await GetFileEntityAsync(userID, id);
            return await _streamWorker.LoadFile(file.PreviewFile.FilePath);
        }

        public async Task<IQueryable<PreviewFileDTO>> GetPreviewFiles(Guid userID)
        {
            var files = await _unityOfWork.PreviewRepository.FindAsync(x => x.File.Owner == userID);
            return files.ProjectTo<PreviewFileDTO>(_mapperConfig);
        }

        public async Task<bool> UpdateFileInfo(Guid userID, UserFileDTO fileDto)
        {
            var file = await GetFileEntityAsync(userID, fileDto.Id);
            return await _unityOfWork.FileRepository.UpdateAsync(_mapper.Map<UserFile<Guid>>(fileDto));
        }

        private async Task<UserFile<Guid>> GetFileEntityAsync(Guid userID, Guid id)
        {
            var file = await _unityOfWork.FileRepository.GetAsync(id);

            if (file.Owner != userID)
                throw new UnauthorizedAccessException("You don't have access to this file");

            return file;
        }
    }
}
