namespace Tmf.Ecom.Manager.Interfaces;

public interface IEcomManager
{
    Task<List<GenerateManifestResponse>> GenerateManifest(GenerateManifestRequest generateManifestRequest);

    Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(RescheduleOrCancelAppointmentRequest rescheduleOrCancelAppointment);

    Task<PushShipmentTrackResponse> PushShipmentTrack(PushShipmentStatusRequest pushShipmentStatus);

    Task<PullShipmentTrackResponse> PullShipmentTrack(PullShipmentStatusRequest pullShipmentStatus);
}
