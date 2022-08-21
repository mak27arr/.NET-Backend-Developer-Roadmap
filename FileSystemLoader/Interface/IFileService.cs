using DTO.File;

namespace FileSystemLoader.Interface
{
    internal interface IFileService
    {
        List<PreviewFileDTO> GetPreviewFiles();
        MemoryStream GetPreviewFile(Guid id);
        List<UserFileDTO> GetFiles();
        MemoryStream GetFile(Guid id);
        List<PreviewFileDTO> FindPreviewFiles(Predicate<PreviewFileDTO> filter);
        Guid CreateFile(UserFileDTO fileInfo, MemoryStream file, bool createPreview);
        bool DeleteFile(Guid id);
    }
}
