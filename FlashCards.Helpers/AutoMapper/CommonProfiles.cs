using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToServer;

namespace FlashCards.Helpers.AutoMapper
{
    public class CommonProfiles : Profile
    {
        public CommonProfiles()
        {
            CreateMap<UserForRegister, User>();
            CreateMap<UserForRegister, UserInfo>();
        }
    }
}
