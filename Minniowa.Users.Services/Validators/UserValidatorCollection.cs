using FluentValidation;
using Minniowa.Users.Core.Entities;

namespace Minniowa.Users.Services.Validators
{
    public class UserValidatorCollection : AbstractValidator<User>
    {
        public UserValidatorCollection()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
