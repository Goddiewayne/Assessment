namespace Zedcrest.Api.Models.User
{
    public record CreateUserResponseDto
    {
        public long Id { get; init; }
        public string UniqueReference { get; init; }
        public string Name { get; init; }
        public string Username { get; init; }
        public string EmailAddress { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime? DateCreated { get; init; }
    }
}
