using FluentValidation;
using MAVN.Service.PushNotifications.Domain;

namespace MAVN.Service.PushNotifications.DomainServices.Validation
{
    public class RegisterForPushNotificationsValidator : AbstractValidator<PushNotificationRegistration>
    {
        public RegisterForPushNotificationsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer Id cannot be empty")
                .Length(1, 50);

            RuleFor(x => x.PushRegistrationToken)
                .NotNull()
                .NotEmpty()
                .WithMessage("Push Registration Token cannot be empty")
                .Length(1, 255);
        }
    }
}
