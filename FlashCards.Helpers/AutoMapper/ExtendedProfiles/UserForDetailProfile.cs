﻿using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class UserForDetailProfile : Profile
    {
        public UserForDetailProfile()
        {
            CreateMap<User, UserForDetail>()
                .ForMember(dest => dest.DisplayName, options =>
                    options.MapFrom(src =>
                        src.UserInfo.DisplayName))
                .ForMember(dest => dest.PhotoUrl, options =>
                    options.MapFrom(src =>
                        src.UserInfo.PhotoUrl))
                .ForMember(dest => dest.FirstName, options =>
                    options.MapFrom(src =>
                        src.UserInfo.FirstName))
                .ForMember(dest => dest.LastName, options =>
                    options.MapFrom(src =>
                        src.UserInfo.LastName))
                .ForMember(dest => dest.City, options =>
                    options.MapFrom(src =>
                        src.UserInfo.City))
                .ForMember(dest => dest.Country, options =>
                    options.MapFrom(src =>
                        src.UserInfo.Country))
                .ForMember(dest => dest.DateCreated, options =>
                    options.MapFrom(src =>
                        src.UserInfo.DateCreated))
                .ForMember(dest => dest.AccountId, options =>
                    options.MapFrom(src =>
                        src.UserInfoId))
                .ForMember(dest => dest.Role, options =>
                    options.MapFrom(src =>
                        src.Role))
                .ForMember(dest => dest.NumberOfWordsInLearningSession, options =>
                    options.MapFrom(src =>
                        src.UserInfo.NumberOfWordsInLearningSession))
                .ForMember(dest => dest.NumberOfWordsInReviewSession, options =>
                    options.MapFrom(src =>
                        src.UserInfo.NumberOfWordsInReviewSession));
        }
    }
}
