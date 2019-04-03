using SSS.Domain.CQRS.Student.Validations;
using System;

namespace SSS.Domain.CQRS.Student.Command.Commands
{
    public class StudentUpdateCommand : StudentCommand
    {
        public StudentUpdateCommand(Guid id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }

        public override bool IsValid()
        {
            ValidationResult = new StudentUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
