using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace alipan;

public class UserInfo(HttpClient httpClient)
{
    public async Task<User> GetUserInfoAsync(CancellationToken token = default)
    {
        return await httpClient.GetFromJsonAsync("/oauth/users/info", Context.Default.User, token)
                   .ConfigureAwait(false) ??
               new User();
    }

    public async Task<Drive> GetDriveInfoAsync(CancellationToken token = default)
    {
        return await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/user/getDriveInfo", Context.Default.Drive, token)
            .ConfigureAwait(false);
    }

    public async Task<Space> GetSpaceInfoAsync(CancellationToken token = default)
    {
        var info = await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/user/getSpaceInfo", Context.Default.SpaceInfo, token)
            .ConfigureAwait(false);
        return info.Space;
    }
}

public static class Ensure
{
    private static async Task<T> EnsureJson<T>(this HttpResponseMessage response, JsonTypeInfo<T> typeInfo,
        CancellationToken? token = default)
    {
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync(typeInfo);
        if (result == null)
        {
            throw new Exception(typeof(T) + " is null");
        }

        return result;
    }


    public static async Task<TR> Request<TR, TB>(this HttpClient httpClient, HttpMethod method, string url, TB body,
        JsonTypeInfo<TB> typeInfo,
        JsonTypeInfo<TR> resultTypeInfo, CancellationToken token = default)
    {
        var request = new HttpRequestMessage(method, url);
        request.Content = new StringContent(JsonSerializer.Serialize(body, typeInfo));
        var response = await httpClient.SendAsync(request, token).ConfigureAwait(false);
        return await response.EnsureJson(resultTypeInfo, token);
    }

    public static async Task<T> Request<T>(this HttpClient httpClient, HttpMethod method, string url,
        JsonTypeInfo<T> typeInfo,
        CancellationToken token = default)
    {
        var response = await httpClient.SendAsync(new HttpRequestMessage(method, url), token).ConfigureAwait(false);
        return await response.EnsureJson(typeInfo, token);
    }
}