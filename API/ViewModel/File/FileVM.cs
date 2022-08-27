namespace API.ViewModel.File
{
    public class FileVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public Guid PreviewFileId { get; set; }
    }
}
