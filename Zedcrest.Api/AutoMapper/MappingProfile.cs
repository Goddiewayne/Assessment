namespace Zedcrest.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserRequestDto>().ForMember(model => model.FileUploads, option => option.Ignore());
            CreateMap<CreateUserRequestDto, User>().ForMember(model => model.FileUploads, option => option.Ignore());
            CreateMap<User, CreateUserResponseDto>();
            CreateMap<CreateUserResponseDto, User>();
            CreateMap<User, FindUserResponseDto>();//.ForMember(model => model.FileUploads, option => option.Ignore()); ;
            CreateMap<FindUserResponseDto, User>();//.ForMember(model => model.FileUploads, option => option.Ignore()); ;
            CreateMap<List<User>, GetAllUsersResponseDto>();

            
        }
    }
}
