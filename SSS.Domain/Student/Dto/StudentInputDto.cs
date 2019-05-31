using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.Student.Dto
{
    public class StudentInputDto : InputDtoBase
    {
        public string name { set; get; }
        public int age { set; get; }
    }
}
