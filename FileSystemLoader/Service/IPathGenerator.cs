using myCloudDAL.DAL.Entities.File;

namespace FileSystemLoader.Service
{
    public interface IPathGenerator
    {
        string GeneratePath<T>(UserFile<T> fileInfo) where T : struct;
    }
}