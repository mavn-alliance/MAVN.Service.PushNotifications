using AutoMapper;
using MAVN.Service.PushNotifications.Client.Models.Requests;
using MAVN.Service.PushNotifications.Client.Models.Responses;
using MAVN.Service.PushNotifications.Domain;
using MAVN.Service.PushNotifications.Domain.Contracts;

namespace MAVN.Service.PushNotifications
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
