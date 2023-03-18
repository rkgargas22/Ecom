namespace Tmf.Ecom.Api.Validations;

public class PullShipmentStatusValidator : AbstractValidator<PullShipmentStatusRequest>
{
    public PullShipmentStatusValidator()
    {
        RuleFor(x => x.Awb).NotEmpty().WithMessage(ValidationMessages.Awb);
        RuleFor(x => x.Order).NotEmpty().WithMessage(ValidationMessages.OrderNumber);
    }
}
