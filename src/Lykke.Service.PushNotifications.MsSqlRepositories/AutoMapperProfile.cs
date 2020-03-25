using System.Collections.Generic;
using AutoMapper;
using Falcon.Common.Encryption;
using Lykke.Service.PushNotifications.MsSqlRepositories.Entities;
using Newtonsoft.Json;

namespace Lykke.Service.PushNotifications.MsSqlRepositories
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
