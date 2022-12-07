﻿using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Games, GamesToReturnDto>()
                .ForMember(b =>b.ConsoleDevice, x => x.MapFrom(c => c.ConsoleDevice.Name))
                .ForMember(b => b.Genre, x => x.MapFrom(c => c.Genre.Name))
                .ForMember(b => b.PictureUrl, x=> x.MapFrom<GameUrlResolver>());
        }
    }
}