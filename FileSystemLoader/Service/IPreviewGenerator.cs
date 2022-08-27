using myCloudDAL.DAL.Entities.File;

namespace FileSystemLoader.Service
{
    public interface IPreviewGenerator
    {
        PreviewFile<T> Generate<T>(UserFile<T> fileInfo) where T : struct;
    }
}