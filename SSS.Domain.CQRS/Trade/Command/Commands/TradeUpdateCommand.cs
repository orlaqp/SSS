using SSS.Domain.CQRS.Trade.Validations;
using SSS.Domain.Trade.Dto;

namespace SSS.Domain.CQRS.Trade.Command.Commands
{
    public class TradeUpdateCommand : TradeCommand
    {
        public TradeUpdateCommand(TradeInputDto input)
        {
            this.first_trade_no = input.first_trade_no;
            this.last_trade_no = input.last_trade_no;
            this.last_trade_status = input.last_trade_status;
            this.last_time = input.last_time;
            this.last_price = input.last_price;
        }

        public override bool IsValid()
        {
            ValidationResult = new TradeUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
