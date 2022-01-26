namespace Zedcrest.Api.Models.FileUpload
{
    public class FileUploadResponseDto
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
