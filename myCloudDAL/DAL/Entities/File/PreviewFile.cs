namespace myCloudDAL.DAL.Entities.File
{
    internal class PreviewFile<T> : BaseDiskFile<T> where T : struct
    {
        public UserFile<T> File { get; set; }

        public UserFile<T> FileId { get; set; }
    }
}
