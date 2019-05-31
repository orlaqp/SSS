using FluentValidation;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using System;

namespace SSS.Domain.CQRS.UserInfo.Validations
{
    public abstract class UserInfoValidation<T> : AbstractValidator<T> where T : UserInfoCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEmpty().WithMessage("������Id");
        }

        protected void ValidatePhone()
        {
            RuleFor(c => c.phone).NotEmpty().WithMessage("�ֻ��Ų���Ϊ��");
        }

        protected void ValidatePassWord()
        {
            RuleFor(c => c.password).NotEmpty().WithMessage("���벻��Ϊ��");
        }
    }
}
