using FluentValidation;
using JetBrains.Annotations;
using MAVN.Service.PushNotifications.Client.Models.Requests;

namespace MAVN.Service.PushNotifications.Validation
{
    [UsedImplicitly]
    public class PaginatedRequestModelValidation<T> : AbstractValidator<T> where T : PaginatedRequestModel
    {
        public PaginatedRequestModelValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CurrentPage)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Current page can't be less than 1")
                .LessThanOrEqualTo(10000)
                .WithMessage("Current page can't be more than 10 000");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page Size can't be less than 1")
                .LessThanOrEqualTo(500)
                .WithMessage("Page Size cannot exceed more then 500");
        }
    }
}
