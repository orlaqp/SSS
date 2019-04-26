﻿using FluentValidation; 
using System;
using SSS.Domain.CQRS.Student.Command.Commands;

namespace SSS.Domain.CQRS.Student.Validations
{
    public abstract class StudentValidation<T> : AbstractValidator<T> where T : StudentCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage("请输入名字");
        }

        protected void ValidateAge()
        {
            RuleFor(c => c.age).NotEmpty().WithMessage("请输入年龄");
        }
    }
}