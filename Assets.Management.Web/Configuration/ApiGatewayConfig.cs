namespace Assets.Management.Web.Configuration;
public class ApiGatewayConfig
{
    public string ApiGateway => "ApiGateway";

    public string PatientsUriOcelot { get; set; } = string.Empty;
    public string PatientsUriYarp { get; set; } = string.Empty;

    public string PatientsWithoutGateway { get; set; } = string.Empty;

}