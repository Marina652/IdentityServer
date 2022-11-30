using AutoMapper;
using User.Application.DTOs;

namespace User.Application.MappingProfiles;

public class AccountProfiles : Profile
{
    public AccountProfiles()
    {
        CreateMap<NewAccountModel, Entities.User>().ReverseMap();
    }
}
