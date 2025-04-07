using AutoMapper;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Mapper
{
    public class Mapping : Profile
    {


        public Mapping()
        {
            CreateMap<BookResponse, Book>().ReverseMap().
            ForMember(dest => dest.titleM, opt => opt.MapFrom(src => src.title));
            CreateMap<RoleResponse, Role>().ReverseMap();
            CreateMap<RoleResponse, UserRole>().ReverseMap();
            CreateMap<UserSignUp, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();

        }
    }
}
