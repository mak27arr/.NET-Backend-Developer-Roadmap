namespace myCloudDAL.DAL.Entities.File
{
    public class PreviewFile<T> : BaseDiskFile<T> where T : struct
    {
        public UserFile<T> File { get; set; }

        public T FileId { get; set; }
    }
}
