namespace Tmf.Ecom.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EcomController : ControllerBase
{
    private readonly IValidator<GenerateManifestRequest> _generateManifestRequestValidator;
    private readonly IValidator<RescheduleOrCancelAppointmentRequest> _rescheduleOrCancelAppointmentRequestValidator;
    private readonly IValidator<PushShipmentStatusRequest> _pushShipmentStatusRequest;
    private readonly IValidator<PullShipmentStatusRequest> _pullShipmentStatusRequest;
    private readonly IEcomManager _ecomManager;
	public EcomController(IEcomManager ecomManager, IValidator<GenerateManifestRequest> generateManifestRequestValidator, IValidator<RescheduleOrCancelAppointmentRequest> rescheduleOrCancelAppointmentRequestValidator, IValidator<PushShipmentStatusRequest> pushShipmentStatusRequest, IValidator<PullShipmentStatusRequest> pullShipmentStatusRequest)
	{
		_ecomManager= ecomManager;
        _generateManifestRequestValidator = generateManifestRequestValidator;
        _rescheduleOrCancelAppointmentRequestValidator = rescheduleOrCancelAppointmentRequestValidator;
        _pushShipmentStatusRequest = pushShipmentStatusRequest;
        _pullShipmentStatusRequest = pullShipmentStatusRequest;

	}


    [HttpPost]
    [Route("GenerateManifest")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<GenerateManifestResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GenerateManifest([FromBody] GenerateManifestRequest generateManifestRequest)
    {
        ValidationResult result = await _generateManifestRequestValidator.ValidateAsync(generateManifestRequest);
      
        if (!result.IsValid)
        {           
            return BadRequest(new ErrorMessage { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m=>m.ErrorMessage) });
        }
        var data = await _ecomManager.GenerateManifest(generateManifestRequest);

        if(data != null && data.Count > 0 && data[0].AwbNumber == 0)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.ErrorInRequest, Error = data });
        }
        return CreatedAtAction(nameof(GenerateManifest), new { AwbNumber = data![0].AwbNumber }, data);
    }

    [HttpPost]
    [Route("RescheduleOrCancelAppointment")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<RescheduleOrCancelAppointmentResponse>) ,StatusCodes.Status201Created)]
    public async Task<IActionResult> RescheduleOrCancelAppointment([FromBody] RescheduleOrCancelAppointmentRequest rescheduleOrCancelAppointment)
    {
        ValidationResult result = await _rescheduleOrCancelAppointmentRequestValidator.ValidateAsync(rescheduleOrCancelAppointment);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        var data = await _ecomManager.RescheduleOrCancelAppointment(rescheduleOrCancelAppointment);
        if (data != null && data.Count > 0 && !data[0].Status)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.ErrorInRequest, Error = data });
        }
        return CreatedAtAction(nameof(RescheduleOrCancelAppointment), new { AwbNumber = data[0].Awb }, data);
    }

    [HttpPost]
    [Route("PushShipmentStatus")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PushShipmentTrackResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> PushShipmentStatus(PushShipmentStatusRequest pushShipmentStatusRequest)
    {
        ValidationResult result = await _pushShipmentStatusRequest.ValidateAsync(pushShipmentStatusRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        var data = await _ecomManager.PushShipmentTrack(pushShipmentStatusRequest);
        if (data != null && !data.Status)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.ErrorInRequest, Error = data });
        }
        return CreatedAtAction(nameof(PushShipmentStatus), new { Status = data.Status }, data);
    }

    [HttpGet]
    [Route("PullShipmentStatus")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PullShipmentTrackResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PullShipmentStatus([FromQuery] PullShipmentStatusRequest pullShipmentStatusRequest)
    {
        ValidationResult result = await _pullShipmentStatusRequest.ValidateAsync(pullShipmentStatusRequest);
        if (!result.IsValid)
        {
            return BadRequest(new ErrorMessage { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        var data = await _ecomManager.PullShipmentTrack(pullShipmentStatusRequest);
        return Ok(data);
    }
}
