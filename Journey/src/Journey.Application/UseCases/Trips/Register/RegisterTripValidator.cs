using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceErrorMessages.INVALID_NAME);

            RuleFor(r => r.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.INVALID_START_DATE);

            RuleFor(r => r).Must(r => r.EndDate >= r.StartDate)
                .WithMessage(ResourceErrorMessages.INVALID_END_DATE);

        }
    }
}
