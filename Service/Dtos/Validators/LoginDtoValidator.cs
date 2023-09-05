using FluentValidation;
using Service.Dtos.Auth;

namespace Service.Dtos.Validators
{
    public class LoginDtoValidator : DtoValidatorBase<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty");
        }
    }
}
