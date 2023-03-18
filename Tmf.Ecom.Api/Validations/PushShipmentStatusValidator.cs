namespace Tmf.Ecom.Api.Validations;

public class PushShipmentStatusValidator : AbstractValidator<PushShipmentStatusRequest>
{
    public PushShipmentStatusValidator()
    {
        RuleFor(x => x.AwbNumber).NotEmpty().WithMessage(ValidationMessages.Awb);
        RuleFor(x => x.AgentId).NotEmpty().WithMessage(ValidationMessages.AgentId);
        RuleFor(x => x.AgentName).NotEmpty().WithMessage(ValidationMessages.AgentName);
        RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationMessages.OrderId);
        RuleFor(x => x.VendorCode).NotEmpty().WithMessage(ValidationMessages.VendorCode);
        RuleFor(x => x.Timestamp).NotEmpty().WithMessage(ValidationMessages.Timestamp);
        RuleFor(x => x.Latitude).NotEmpty().WithMessage(ValidationMessages.Latitude);
        RuleFor(x => x.Longitude).NotEmpty().WithMessage(ValidationMessages.Longitude);
    }
}
