using FluentValidation;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using System;

namespace SSS.Domain.CQRS.UserInfo.Validations
{
    public abstract class UserInfoValidation<T> : AbstractValidator<T> where T : UserInfoCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEmpty().WithMessage("请输入Id");
        }

        protected void ValidatePhone()
        {
            RuleFor(c => c.phone).NotEmpty().WithMessage("手机号不能为空");
        }

        protected void ValidatePassWord()
        {
            RuleFor(c => c.password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}
