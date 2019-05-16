namespace SSS.Domain.Seedwork.Model
{
    public class Pages<ListEntity> where ListEntity : class
    {
        public ListEntity data { set; get; }

        public int count { set; get; }

        public Pages(ListEntity data, int count)
        {
            this.data = data;
            this.count = count;
        }
    }
}
