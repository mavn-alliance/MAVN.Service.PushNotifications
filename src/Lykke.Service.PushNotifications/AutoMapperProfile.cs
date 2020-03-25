using AutoMapper;
using Lykke.Service.PushNotifications.Client.Models.Requests;
using Lykke.Service.PushNotifications.Client.Models.Responses;
using Lykke.Service.PushNotifications.Domain;
using Lykke.Service.PushNotifications.Domain.Contracts;

namespace Lykke.Service.PushNotifications
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreatePushRegistrationRequestModel, PushNotificationRegistration>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.RegistrationDate, opt => opt.Ignore());

            CreateMap<PushNotificationRegistration, GetPushRegistrationResponseModel>();

            CreateMap<Domain.Enums.PushTokenInsertionResult, Client.Enums.PushTokenInsertionResult>();

            CreateMap<NotificationMessage, NotificationMessageResponseModel>();

            CreateMap(typeof(PaginatedList<>), typeof(PaginatedResponseModel<>));
        }
    }
}
