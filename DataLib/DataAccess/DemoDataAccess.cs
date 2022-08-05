using DataLib.Models;

namespace DataLib.DataAccess
{
    public class DemoDataAccess : IDataAccess
    {
        private List<FileModel> files = new();

        public DemoDataAccess()
        {
            files.Add(new FileModel { Id = 1, Name = "Tim", Path = "C:\\1.jpg" });
            files.Add(new FileModel { Id = 2, Name = "Sue", Path = "C:\\2.jpg" });
        }

        public List<FileModel> GetFiles() => files;

        public FileModel InsertFile(string name, string path)
        {
            var p = new FileModel() { Name = name, Path = path };
            p.Id = files.Max(x => x.Id) + 1;
            files.Add(p);
            return p;
        }
    }
}
