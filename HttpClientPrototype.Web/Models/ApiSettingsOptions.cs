namespace HttpClientPrototype.Web.Models;

public class ApiSettingsOptions
{
    public string? AgentName { get; set; }
    public string BaseUrl { get; set; }
    public ApiSettingsAuthTypeEnum AuthType { get; set; }
    public string? Token { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}