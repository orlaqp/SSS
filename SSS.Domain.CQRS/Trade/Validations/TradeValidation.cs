using FluentValidation;
using SSS.Domain.CQRS.Trade.Command.Commands;

namespace SSS.Domain.CQRS.Trade.Validations
{
    public abstract class TradeValidation<T> : AbstractValidator<T> where T : TradeCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEmpty().WithMessage("请输入Id");
        }

        protected void ValidateCoin()
        {
            RuleFor(c => c.coin).NotEmpty().WithMessage("请输入币对");
        }

        protected void ValidateFristPrice()
        {
            RuleFor(c => c.first_price).NotEmpty().WithMessage("请输入开单价格");
            RuleFor(c => c.first_price).GreaterThan(0).WithMessage("请输入正确开单价格，必须大于0");
        }

        protected void ValidateLastPrice()
        {
            RuleFor(c => c.last_price).NotEmpty().WithMessage("请输入平单价格");
            RuleFor(c => c.last_price).GreaterThan(0).WithMessage("请输入平单正确价格，必须大于0");
        }

        protected void ValidateSize()
        {
            RuleFor(c => c.size).NotEmpty().WithMessage("请输入数量");
            RuleFor(c => c.size).GreaterThan(0).WithMessage("请输入正确数量，必须大于0");
        }

        protected void ValidateSide()
        {
            RuleFor(c => c.side).NotEmpty().WithMessage("请输入开单方向");
        }

        protected void ValidateLastTradeNo()
        {
            RuleFor(c => c.last_trade_no).NotEmpty().WithMessage("请输入平单交易单号");
        }
        protected void ValidateFirstTradeNo()
        {
            RuleFor(c => c.first_trade_no).NotEmpty().WithMessage("请输入开单交易单号");
        }
    }
}
