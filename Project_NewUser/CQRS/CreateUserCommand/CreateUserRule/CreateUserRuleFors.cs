using FluentValidation;
using Project_NewUser.Models;

namespace Project_NewUser.CQRS.CreateUserCommand.CreateUserRule
{
    public class CreateUserRuleFors : AbstractValidator<User>
    {
        public CreateUserRuleFors()
        {
            RuleFor(a => a.Id)
                .NotNull();
            RuleFor(a => a.Name)
                .NotNull();
        }
    }
}
