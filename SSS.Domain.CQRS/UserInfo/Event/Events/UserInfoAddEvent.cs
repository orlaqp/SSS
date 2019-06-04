namespace SSS.Domain.CQRS.UserInfo.Event.Events
{
    public class UserInfoAddEvent : Seedwork.Events.Event
    {
        public string id { set; get; }

        public UserInfoAddEvent(Domain.UserInfo.UserInfo model)
        {
            this.AggregateId = model.Id;
            this.id = model.Id;
        }
    }
}
