namespace myCloudDAL.DAL.Entities.File
{
    public class UserFile<T> : BaseDiskFile<T> where T : struct
    {
        public Guid Owner { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public PreviewFile<T> PreviewFile { get; set; }
        public T PreviewFileId { get; set; }
    }
}
