using DataLib.Models;

namespace DataLib.DataAccess
{
    public interface IDataAccess
    {
        List<FileModel> GetFiles();
        FileModel InsertFile(string name, string path);
    }
}