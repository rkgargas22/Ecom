using System.Xml.Serialization;
namespace Tmf.Ecom.Infrastructure.Services;

public class EcomRepository : IEcomRepository
{
    private readonly IHttpService _httpService;
    private readonly EcomOptions _ecomOptions;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
   
    public EcomRepository(IHttpService httpService, IOptions<EcomOptions> ecomOptions, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {       
        _httpService = httpService;
        _ecomOptions = ecomOptions.Value;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }
    public async Task<List<GenerateManifestResponse>> GenerateManifest(EcomGenerateManifestModel ecomGenerateManifestModel)
    {
        ecomGenerateManifestModel.UserName = _ecomOptions.Username;
        ecomGenerateManifestModel.Password = _ecomOptions.Password;

        var dict = new Dictionary<string, string>
        {
            { "username", ecomGenerateManifestModel.UserName },
            { "password", ecomGenerateManifestModel.Password },
            { "json_input", JsonSerializer.Serialize(ecomGenerateManifestModel.JsonInput) }
        };

        var result = await _httpService.PostAsync(_ecomOptions.Url.GenerateManifest, new FormUrlEncodedContent(dict));

        if (result == null)
        {
            return new List<GenerateManifestResponse>();
        }

        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        return JsonSerializer.Deserialize<List<GenerateManifestResponse>>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(EcomRescheduleOrCancelAppointmentModel ecomRescheduleOrCancelAppointment)
    {
        ecomRescheduleOrCancelAppointment.UserName = _ecomOptions.Username;
        ecomRescheduleOrCancelAppointment.Password = _ecomOptions.Password;

        var dict = new Dictionary<string, string>
        {
            { "username", ecomRescheduleOrCancelAppointment.UserName },
            { "password", ecomRescheduleOrCancelAppointment.Password },
            { "json_input", JsonSerializer.Serialize(ecomRescheduleOrCancelAppointment.JsonInput) }
        };

        var result = await _httpService.PostAsync(_ecomOptions.Url.RescheduleOrCancelRequest, new FormUrlEncodedContent(dict));

        if (result == null)
        {
            return new List<RescheduleOrCancelAppointmentResponse>();
        }

        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        return JsonSerializer.Deserialize<List<RescheduleOrCancelAppointmentResponse>>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<PullShipmentTrackResponse> PullShipmentTrack(EcomPullShipmentTrackModel ecomPullShipmentTrackModel)
    {
        ecomPullShipmentTrackModel.UserName = _ecomOptions.Username;
        ecomPullShipmentTrackModel.Password = _ecomOptions.Password;
        var result = await _httpService.GetAsync(_ecomOptions.Url.GetShipmentDetails + "?awb=" + ecomPullShipmentTrackModel.Awb + "&order=" + ecomPullShipmentTrackModel.Order + "&username=" + ecomPullShipmentTrackModel.UserName + "&password=" + ecomPullShipmentTrackModel.Password);

        if (result == null)
        {
            return new PullShipmentTrackResponse();
        }

        XmlSerializer serializer = new XmlSerializer(typeof(PullShipmentTrackResponse));
        PullShipmentTrackResponse ecomExpressObjects = (PullShipmentTrackResponse)serializer.Deserialize(result);

      

        return ecomExpressObjects ?? throw new ArgumentNullException();

    }

    public async Task<PushShipmentTrackResponse> PushShipmentTrack(EcomPushShipmentTrackModel ecomPushShipmentTrackModel)
    {
        throw new NotImplementedException();
        //SqlConnection con = new SqlConnection(_connectionStringsOptions.DefaultConnection);
        //SqlCommand cmd = new SqlCommand("sp_Employee_Add", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Emp_Name", employeeEntities.Emp_Name);
        //cmd.Parameters.AddWithValue("@City", employeeEntities.City);
        //cmd.Parameters.AddWithValue("@State", employeeEntities.State);
        //cmd.Parameters.AddWithValue("@Country", employeeEntities.Country);
        //cmd.Parameters.AddWithValue("@Department", employeeEntities.Department);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();
        //return ecomPushShipmentTrackModel;
    }
}
