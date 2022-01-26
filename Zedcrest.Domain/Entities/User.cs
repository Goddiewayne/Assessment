

namespace Zedcrest.Domain.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public string UniqueReference { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime?  DateCreated { get; set; }
        public List<FileUploadInfo> FileUploads { get; set; }
    }
}
