namespace myCloudDAL.DAL.Entities.File
{
    internal abstract class BaseDiskFile<T>
    {
        public T Id { get; set; }
        public string FilePath { get; set; }
    }
}
