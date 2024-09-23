using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActivityValidator() 
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceErrorMessages.INVALID_NAME);
        }
    }
}
