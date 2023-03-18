namespace Tmf.Ecom.Api.Validations;

public class GenerateManifestValidator : AbstractValidator<GenerateManifestRequest>
{
    public GenerateManifestValidator()
    {
        RuleFor(x => x.OrderNumber).NotEmpty().WithMessage(ValidationMessages.OrderNumber);
        RuleFor(x => x.ItemDescription).NotEmpty().WithMessage(ValidationMessages.ItemDescription);
        RuleFor(x => x.CollectableValue).NotEmpty().WithMessage(ValidationMessages.CollectableValue);

        RuleFor(x => x.Consignee).NotEmpty().WithMessage(ValidationMessages.Consignee);
        RuleFor(x => x.ConsigneeAddress1).NotEmpty().WithMessage(ValidationMessages.ConsigneeAddress1);
        RuleFor(x => x.DestinationCity).NotEmpty().WithMessage(ValidationMessages.DestinationCity);
        RuleFor(x => x.Pincode).NotEmpty().WithMessage(ValidationMessages.Pincode);
        RuleFor(x => x.State).NotEmpty().WithMessage(ValidationMessages.State);
        RuleFor(x => x.Mobile).NotEmpty().WithMessage(ValidationMessages.Mobile);

        RuleFor(x => x.DropVendorCode).NotEmpty().WithMessage(ValidationMessages.DropVendorCode);
        RuleFor(x => x.DropName).NotEmpty().WithMessage(ValidationMessages.DropName);
        RuleFor(x => x.DropAddressLine1).NotEmpty().WithMessage(ValidationMessages.DropAddressLine1);
        RuleFor(x => x.DropPincode).NotEmpty().WithMessage(ValidationMessages.DropPincode);
        RuleFor(x => x.DropMobile).NotEmpty().WithMessage(ValidationMessages.DropMobile);
    }
}
