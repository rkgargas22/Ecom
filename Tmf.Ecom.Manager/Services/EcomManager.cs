namespace Tmf.Ecom.Manager.Services;

public class EcomManager : IEcomManager
{
    private readonly IEcomRepository _ecomRepository;
    private readonly EcomOptions _options;
    public EcomManager(IEcomRepository ecomRepository, IOptions<EcomOptions> options)
    {
        _ecomRepository = ecomRepository;
        _options = options.Value;
    }

    public async Task<List<GenerateManifestResponse>> GenerateManifest(GenerateManifestRequest generateManifestRequest)
    {
        EcomGenerateManifestModel ecomGenerateManifestModel = new EcomGenerateManifestModel();

        List<Infrastructure.Models.Activity> ActivityList = new List<Infrastructure.Models.Activity>();
        if (generateManifestRequest.Activites != null && generateManifestRequest.Activites.Count > 0)
        {
            foreach (var activity in generateManifestRequest.Activites)
            {
                Infrastructure.Models.Activity EcomActivity = new Infrastructure.Models.Activity
                {
                    Code = activity.Code,
                    DocumentRefNumber = activity.DocumentRefNumber,
                    Optional = activity.Optional,
                    Remarks = activity.Remarks,
                };
                ActivityList.Add(EcomActivity);
            }
        }

        Infrastructure.Models.AdditionalInformation EcomAdditionalInformation = new Infrastructure.Models.AdditionalInformation();
        if (generateManifestRequest.AdditionalInformation != null)
        {
            EcomAdditionalInformation.Timeslot = generateManifestRequest.AdditionalInformation.Timeslot;
            EcomAdditionalInformation.FormPrint = generateManifestRequest.AdditionalInformation.FormPrint;
            EcomAdditionalInformation.Date = generateManifestRequest.AdditionalInformation.Date;
        }


        EcomGenerateManifestJsonInput ecomGenerateManifestJsonInput = new EcomGenerateManifestJsonInput
        {
            Activites = generateManifestRequest.Activites != null && generateManifestRequest.Activites.Count > 0 ? ActivityList : null,
            AdditionalInformation = generateManifestRequest.AdditionalInformation != null ? EcomAdditionalInformation : null,
            OrderNumber = generateManifestRequest.OrderNumber,
            Product = _options.Product,
            Consignee = generateManifestRequest.Consignee,
            ConsigneeAddress1 = generateManifestRequest.ConsigneeAddress1,
            ConsigneeAddress2 = generateManifestRequest.ConsigneeAddress2,
            ConsigneeAddress3 = generateManifestRequest.ConsigneeAddress3,
            ConsigneeAddress4 = generateManifestRequest.ConsigneeAddress4,
            CollectableValue = generateManifestRequest.CollectableValue,
            DestinationCity = generateManifestRequest.DestinationCity,
            DropVendorCode = generateManifestRequest.DropVendorCode,
            ItemDescription = generateManifestRequest.ItemDescription,
            Mobile = generateManifestRequest.Mobile,
            Telephone = generateManifestRequest.Telephone,
            Pincode = generateManifestRequest.Pincode,
            State = generateManifestRequest.State,
            DropName = generateManifestRequest.DropName,
            DropAddressLine1 = generateManifestRequest.DropAddressLine1,
            DropAddressLine2 = generateManifestRequest.DropAddressLine2,
            DropAddressLine3 = generateManifestRequest.DropAddressLine3,
            DropAddressLine4 = generateManifestRequest.DropAddressLine4,
            DropMobile = generateManifestRequest.DropMobile,
            DropPincode = generateManifestRequest.DropPincode
        };

        List<EcomGenerateManifestJsonInput> ecomGenerateManifestJsonInputList = new List<EcomGenerateManifestJsonInput>
        {
            ecomGenerateManifestJsonInput
        };

        ecomGenerateManifestModel.JsonInput = ecomGenerateManifestJsonInputList;

        return await _ecomRepository.GenerateManifest(ecomGenerateManifestModel);
    }

    public async Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(RescheduleOrCancelAppointmentRequest rescheduleOrCancelAppointment)
    {
        EcomRescheduleOrCancelAppointmentModel ecomRescheduleOrCancelAppointment = new EcomRescheduleOrCancelAppointmentModel();

        Infrastructure.Models.ConsigneeAddress consigneeAddress = new Infrastructure.Models.ConsigneeAddress();
        if (rescheduleOrCancelAppointment.ConsigneeAddress != null)
        {
            consigneeAddress.CA1 = rescheduleOrCancelAppointment.ConsigneeAddress.CA1;
            consigneeAddress.CA2 = rescheduleOrCancelAppointment.ConsigneeAddress.CA2;
            consigneeAddress.CA3 = rescheduleOrCancelAppointment.ConsigneeAddress.CA3;
            consigneeAddress.CA4 = rescheduleOrCancelAppointment.ConsigneeAddress.CA4;
            consigneeAddress.Pincode = rescheduleOrCancelAppointment.ConsigneeAddress.Pincode;
        }

        EcomRescheduleOrCancelAppointmentJsonInput ecomRescheduleOrCancelAppointmentJsonInput = new EcomRescheduleOrCancelAppointmentJsonInput
        {
            Awb = rescheduleOrCancelAppointment.Awb,
            Comments = rescheduleOrCancelAppointment.Comments,
            ConsigneeAddress = consigneeAddress,
            Instruction = rescheduleOrCancelAppointment.Instruction,
            Mobile = rescheduleOrCancelAppointment.Mobile,
            ScheduledDeliveryDate = rescheduleOrCancelAppointment.ScheduledDeliveryDate,
            ScheduledDeliverySlot = rescheduleOrCancelAppointment.ScheduledDeliverySlot
        };

        List<EcomRescheduleOrCancelAppointmentJsonInput> ecomRescheduleOrCancelAppointmentJsonInputList = new List<EcomRescheduleOrCancelAppointmentJsonInput>
        {
            ecomRescheduleOrCancelAppointmentJsonInput
        };

        ecomRescheduleOrCancelAppointment.JsonInput = ecomRescheduleOrCancelAppointmentJsonInputList;

        return await _ecomRepository.RescheduleOrCancelAppointment(ecomRescheduleOrCancelAppointment);
    }

    public async Task<PullShipmentTrackResponse> PullShipmentTrack(PullShipmentStatusRequest pullShipmentStatus)
    {
        EcomPullShipmentTrackModel ecomPullShipmentTrackModel = new EcomPullShipmentTrackModel
        {
            Awb = pullShipmentStatus.Awb,
            Order = pullShipmentStatus.Order
        };

        return await _ecomRepository.PullShipmentTrack(ecomPullShipmentTrackModel);
    }

    public async Task<PushShipmentTrackResponse> PushShipmentTrack(PushShipmentStatusRequest pushShipmentStatus)
    {
        List<Infrastructure.Models.Documents> documentList = new List<Infrastructure.Models.Documents>();
        if(pushShipmentStatus.Document != null && pushShipmentStatus.Document.Count > 0)
        {
            foreach(var doc in pushShipmentStatus.Document)
            {
                Infrastructure.Models.Documents EcomDoc = new Infrastructure.Models.Documents
                {
                    ActivityCode = doc.ActivityCode,
                    Image = doc.Image
                };
                documentList.Add(EcomDoc);
            }
        }

        EcomPushShipmentTrackModel ecomPushShipmentTrackModel = new EcomPushShipmentTrackModel
        {
            AwbNumber = pushShipmentStatus.AwbNumber,
            AgentId = pushShipmentStatus.AgentId,
            AgentName = pushShipmentStatus.AgentName,
            Document = documentList,
            Latitude= pushShipmentStatus.Latitude,
            Longitude = pushShipmentStatus.Longitude,
            ReasonCodeDescription= pushShipmentStatus.ReasonCodeDescription,
            RescheduledDate = pushShipmentStatus.RescheduledDate,
            OrderId = pushShipmentStatus.OrderId,
            ReasonCodeNumber = pushShipmentStatus.ReasonCodeNumber,
            RescheduledTime = pushShipmentStatus.RescheduledTime,
            Timestamp = pushShipmentStatus.Timestamp,
            VendorCode = pushShipmentStatus.VendorCode
        };

        return await _ecomRepository.PushShipmentTrack(ecomPushShipmentTrackModel);
    }
}
