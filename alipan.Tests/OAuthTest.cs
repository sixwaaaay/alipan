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

using JetBrains.Annotations;
using Xunit.Abstractions;

namespace alipan.Tests;

[TestSubject(typeof(OAuth))]
public class OAuthTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GenerateUrl_ReturnsCorrectUrl_WhenAllFieldsAreSet()
    {
        // Arrange
        var oauth = new OAuth
        {
            ClientId = "testClientId",
            RedirectUri = "testRedirectUri",
            State = "testState",
        };

        // Act
        var result = oauth.GenerateUrl();
        // Assert

        const string expected = "https://www.alipan.com/o/oauth/authorize?" +
                                "client_id=testClientId&redirect_uri=testRedirectUri&scope=user:base&response_type=code&state=testState";

        testOutputHelper.WriteLine(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GenerateUrl_ReturnsCorrectUrl_WhenOptionalFieldsAreNotSet()
    {
        // Arrange
        var oauth = new OAuth
        {
            ClientId = "testClientId",
            RedirectUri = "testRedirectUri",
            Scope = "user:base,file:all:read"
        };

        // Act
        var result = oauth.GenerateUrl();

        // Assert

        const string expected = "https://www.alipan.com/o/oauth/authorize?" +
                                "client_id=testClientId&redirect_uri=testRedirectUri&scope=user:base,file:all:read&response_type=code";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GenerateUrl_EncodesSpecialCharactersInFields()
    {
        // Arrange
        var oauth = new OAuth
        {
            ClientId = "testClientId",
            RedirectUri = "http://example.com/redirect?param=value",
            State = "state with spaces",
            ReLogin = true
        };

        // Act
        var result = oauth.GenerateUrl();

        // Assert
        testOutputHelper.WriteLine(result);
        const string expected = "https://www.alipan.com/o/oauth/authorize?" +
                                "client_id=testClientId&redirect_uri=http%3A%2F%2Fexample.com%2Fredirect%3Fparam%3Dvalue&scope=user:base&response_type=code&state=state%20with%20spaces&relogin=true";
        Assert.Equal(expected, result);
    }
}