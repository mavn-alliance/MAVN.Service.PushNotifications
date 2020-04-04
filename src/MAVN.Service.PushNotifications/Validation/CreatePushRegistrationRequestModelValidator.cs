using FluentValidation;
using JetBrains.Annotations;
using MAVN.Service.PushNotifications.Client.Models.Requests;

namespace MAVN.Service.PushNotifications.Validation
{
    [UsedImplicitly]
    public class CreatePushRegistrationRequestModelValidator : AbstractValidator<CreatePushRegistrationRequestModel>
    {
        public CreatePushRegistrationRequestModelValidator()
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
