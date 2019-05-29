using FluentValidation;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using System;

namespace SSS.Domain.CQRS.UserInfo.Validations
{
    public abstract class UserInfoValidation<T> : AbstractValidator<T> where T : UserInfoCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEqual(Guid.Empty);
        }
    }
}
