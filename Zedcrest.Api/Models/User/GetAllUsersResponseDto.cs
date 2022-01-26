namespace Zedcrest.Api.Models.User
{
    public record GetAllUsersResponseDto
    {
        public long Id { get; set; }
        public string UniqueReference { get; init; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateCreated { get; init; }
    }
}
