using FluentValidation;
using SSS.Domain.CQRS.Trade.Command.Commands;
using System;

namespace SSS.Domain.CQRS.Trade.Validations
{
    public abstract class TradeValidation<T> : AbstractValidator<T> where T : TradeCommand
    {

        protected void ValidateId()
        {
            RuleFor(c => c.id).NotEqual(Guid.Empty);
        }

        protected void ValidateCoin()
        {
            RuleFor(c => c.coin).NotEmpty().WithMessage("请输入币对");
        }

        protected void ValidatePrice()
        {
            RuleFor(c => c.price).NotEmpty().WithMessage("请输入价格");
            RuleFor(c => c.price).GreaterThan(0).WithMessage("请输入正确价格，必须大于0");
        }

        protected void ValidateSize()
        {
            RuleFor(c => c.size).NotEmpty().WithMessage("请输入数量");
            RuleFor(c => c.size).GreaterThan(0).WithMessage("请输入正确数量，必须大于0");
        }

        protected void ValidateSide()
        {
            RuleFor(c => c.side).NotNull().WithMessage("请输入开单方向");
        }

        protected void ValidateTradeNo()
        {
            RuleFor(c => c.trade_no).NotEmpty().WithMessage("请输入交易单号");
        }
    }
}
