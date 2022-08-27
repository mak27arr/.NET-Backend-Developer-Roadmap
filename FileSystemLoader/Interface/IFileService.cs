using DTO.File;

namespace FileSystemLoader.Interface
{
    public interface IFileService
    {
        Task<IQueryable<UserFileDTO>> GetFilesInfo(Guid userID);
        Task<UserFileDTO> GetFileInfo(Guid userID, Guid id);
        Task<bool> UpdateFileInfo(Guid userID, UserFileDTO fileDto);
        Task<IQueryable<PreviewFileDTO>> GetPreviewFiles(Guid userID);
        Task<MemoryStream> GetPreviewFile(Guid userID, Guid id);
        Task<MemoryStream> GetFile(Guid userID, Guid id);
        Task<IQueryable<PreviewFileDTO>> FindPreviewFiles(Guid userID, Predicate<PreviewFileDTO> filter);
        Task<Guid> CreateFileAsync(Guid userID, UserFileDTO fileInfo, MemoryStream file, bool createPreview);
        Task<bool> DeleteFile(Guid userID, Guid id);
    }
}
