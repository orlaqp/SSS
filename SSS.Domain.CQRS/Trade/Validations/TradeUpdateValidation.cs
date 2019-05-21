using SSS.Domain.CQRS.Trade.Command.Commands;

namespace SSS.Domain.CQRS.Trade.Validations
{
    public class TradeUpdateValidation : TradeValidation<TradeUpdateCommand>
    {
        public TradeUpdateValidation()
        {
            ValidateFirstTradeNo();
            ValidateLastPrice(); 
            ValidateLastTradeNo();
        }
    }
}
