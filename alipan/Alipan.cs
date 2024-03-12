using System.Net.Http.Headers;

namespace alipan;

public class Alipan
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://open.aliyundrive.com")
    };

    public string AccessToken
    {
        set => _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
    }

    public UserInfo User { get; }
    
    public Driver Driver { get; }

    public Alipan()
    {
        User = new UserInfo(_httpClient);
        Driver = new Driver(_httpClient);
    }
}