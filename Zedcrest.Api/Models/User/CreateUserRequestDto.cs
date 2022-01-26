namespace Zedcrest.Api.Models.User
{
    public class CreateUserRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} should have minimum 2 characters")]
        public string Name { get; init; }
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Username)} should have minimum 2 characters")]
        public string Username { get; init; }
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(EmailAddress)} should have minimum 5 characters")]
        public string EmailAddress { get; init; }
        [Required]
        [MinLength(11, ErrorMessage = $"{nameof(PhoneNumber)} should have minimum 11 characters")]
        public string PhoneNumber { get; init; }
        public List<IFormFile> FileUploads { get; init; }
    }
}
