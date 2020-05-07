using System.Collections.Generic;
using AutoMapper;
using MAVN.Common.Encryption;
using MAVN.Service.PushNotifications.MsSqlRepositories.Entities;
using Newtonsoft.Json;

namespace MAVN.Service.PushNotifications.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NotificationMessage, Domain.Contracts.NotificationMessage>()
                .ForMember(dest => dest.CustomPayload, opt => opt.MapFrom(src =>
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(src.CustomPayload)));
        }
    }
}
