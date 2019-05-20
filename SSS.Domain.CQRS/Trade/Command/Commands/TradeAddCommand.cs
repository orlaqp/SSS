using SSS.Domain.CQRS.Trade.Validations;
using SSS.Domain.Trade.Dto;

namespace SSS.Domain.CQRS.Trade.Command.Commands
{
    public class TradeAddCommand : TradeCommand
    {
        public TradeAddCommand(TradeInputDto input)
        {
            this.id = input.id;
            this.coin = input.coin;
        }

        public override bool IsValid()
        {
            ValidationResult = new TradeAddValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
