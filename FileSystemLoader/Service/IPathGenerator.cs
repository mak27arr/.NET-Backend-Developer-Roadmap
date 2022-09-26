using myCloudDAL.DAL.Entities.File;

namespace FileSystemLoader.Service
{
    public interface IPathGenerator
    {
        string GeneratePath<T>(UserFile<T> fileInfo, string fileNamePrefix) where T : struct;
    }
}