namespace Tmf.Ecom.Infrastructure.Interfaces;

public interface IEcomRepository
{
    Task<List<GenerateManifestResponse>> GenerateManifest(EcomGenerateManifestModel ecomGenerateManifestModel);
    Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(EcomRescheduleOrCancelAppointmentModel ecomRescheduleOrCancelAppointment);
    Task<PushShipmentTrackResponse> PushShipmentTrack(EcomPushShipmentTrackModel ecomPushShipmentTrackModel);
    Task<PullShipmentTrackResponse> PullShipmentTrack(EcomPullShipmentTrackModel ecomPullShipmentTrackModel);
}
