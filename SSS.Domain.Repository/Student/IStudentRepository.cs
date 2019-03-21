namespace SSS.Domain.Repository.Student
{
    public interface IStudentRepository
    {
        SSS.Domain.Core.Student.Student GetByName(string name);
    }
}