using SSS.Domain.CQRS.Trade.Validations;
using SSS.Domain.Trade.Dto;

namespace SSS.Domain.CQRS.Trade.Command.Commands
{

    public class TradeNullCommand : TradeCommand
    {
        public TradeNullCommand(TradeInputDto input)
        {
            this.last_price = input.first_price;
        }

        public override bool IsValid()
        {
            ValidationResult = new TradeNullValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
