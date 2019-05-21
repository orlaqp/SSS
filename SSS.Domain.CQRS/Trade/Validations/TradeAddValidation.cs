using SSS.Domain.CQRS.Trade.Command.Commands;

namespace SSS.Domain.CQRS.Trade.Validations
{
    public class TradeAddValidation : TradeValidation<TradeAddCommand>
    {
        public TradeAddValidation()
        {
            ValidateId();
            ValidateCoin();
            ValidateFristPrice();
            ValidateSize();
            ValidateSide();
            ValidateFirstTradeNo();
        }
    }
}
