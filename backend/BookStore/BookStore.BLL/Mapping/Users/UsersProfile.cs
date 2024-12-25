using AutoMapper;
using BookStore.BLL.Dto.UserDto;
using BookStore.DAL.Entities;

namespace BookStore.BLL.Mapping.Users
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForPath(x => x.UserName, conf => conf.MapFrom(x => x.nickname))
                .ForSourceMember(x => x.password, opt => opt.DoNotValidate())
                .ForMember(x => x.AccessTokenIds, opt => opt.Ignore())
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForPath(x => x.Name, conf => conf.MapFrom(x => x.name))
                .ForPath(x => x.Surename, conf => conf.MapFrom(x => x.surename))
                .ForPath(x => x.BirthDate, conf => conf.MapFrom(x => DateOnly.Parse(x.birthday)))
                .ForPath(x => x.Address, conf => conf.MapFrom(x => x.address));

            CreateMap<User, RegisterUserDto>()
                .ForPath(x => x.nickname, conf => conf.MapFrom(x => x.UserName))
                .ForSourceMember(x => x.PasswordHash, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.AccessTokenIds, opt => opt.DoNotValidate())
                .ForMember(x => x.password, opt => opt.Ignore())
                .ForPath(x => x.name, conf => conf.MapFrom(x => x.Name))
                .ForPath(x => x.surename, conf => conf.MapFrom(x => x.Surename))
                .ForPath(x => x.birthday, conf => conf.MapFrom(x => x.BirthDate.ToShortDateString()))
                .ForPath(x => x.address, conf => conf.MapFrom(x => x.Address));

            CreateMap<User, AuthResponseDto>()
                .ForPath(x => x.nickname, conf => conf.MapFrom(x => x.UserName))
                .ForPath(x => x.surename, conf => conf.MapFrom(x => x.Surename))
                .ForPath(x => x.name, conf => conf.MapFrom(x => x.Name))
                .ForPath(x => x.address, conf => conf.MapFrom(x => x.Address))
                .ForPath(x => x.birthday, conf => conf.MapFrom(x => x.BirthDate.ToShortDateString()));
                
                            
        }
    }
}
