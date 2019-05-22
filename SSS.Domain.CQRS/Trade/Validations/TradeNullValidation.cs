using SSS.Domain.CQRS.Trade.Command.Commands;

namespace SSS.Domain.CQRS.Trade.Validations
{
    public class TradeNullValidation : TradeValidation<TradeNullCommand>
    {
        public TradeNullValidation()
        { 
        }
    }
}
