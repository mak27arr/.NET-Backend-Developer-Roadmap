namespace DTO.File
{
    public class UserFileDTO
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public Guid PreviewFileId { get; set; }
    }
}
