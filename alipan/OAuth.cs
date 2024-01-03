/*
 * Copyright (c) 2024 sixwaaaay.
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE*2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System.Text.Encodings.Web;

namespace alipan;

public class OAuth
{
    string url { get; init; } = "https://www.alipan.com/o/oauth/authorize";

    public string ClientId { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string Scope { get; init; } = "user:base";
    private string ResponseType => "code";
    public string? State { get; init; }
    public bool ReLogin { get; init; }

    /*
     * generate url
     *
     */
    public string GenerateUrl()
    {
        var query = new Dictionary<string, string>
        {
            ["client_id"] = ClientId,
            ["redirect_uri"] = UrlEncoder.Default.Encode(RedirectUri),
            ["scope"] = Scope,
            ["response_type"] = ResponseType,
        };
        if (!string.IsNullOrEmpty(State))
        {
            query["state"] = State;
        }

        if (ReLogin)
        {
            query["relogin"] = "true";
        }

        var uriBuilder = new UriBuilder(url)
        {
            Query = string.Join("&", query.Select(kvp => $"{kvp.Key}={kvp.Value}"))
        };
        return uriBuilder.Uri.AbsoluteUri;
    }
}