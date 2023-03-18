namespace Tmf.Ecom.Api.Validations;

public class RescheduleOrCancelAppointmentValidator : AbstractValidator<RescheduleOrCancelAppointmentRequest>
{
    public RescheduleOrCancelAppointmentValidator()
    {
        RuleFor(x => x.Awb).NotEmpty().WithMessage(ValidationMessages.Awb);
        RuleFor(x => x.Mobile).NotEmpty().WithMessage(ValidationMessages.Mobile);
        RuleFor(x => x.ScheduledDeliverySlot).NotEmpty().WithMessage(ValidationMessages.ScheduledDeliverySlot);
        RuleFor(x => x.ScheduledDeliveryDate).NotEmpty().WithMessage(ValidationMessages.ScheduledDeliveryDate);
        RuleFor(x => x.Comments).NotEmpty().WithMessage(ValidationMessages.Comments);
    }
}
