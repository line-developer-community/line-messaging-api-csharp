using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LineDC.Messaging.OAuth
{
    /// <summary>
    /// HttpClient for OAuth
    /// https://developers.line.me/en/docs/messaging-api/reference/#oauth
    /// </summary>
    internal class OAuthClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _channelId;
        private readonly string _channelAccessToken;
        private readonly string _uri;

        public OAuthClient(string channelId, string channelAccessToken, string uri)
        {
            _channelId = channelId;
            _channelAccessToken = channelAccessToken;
            _uri = uri;
        }

        /// <summary>
        /// Issues a short-lived channel access token. 
        /// Up to 30 tokens can be issued. If the maximum is exceeded, existing channel access tokens will be revoked in the order of when they were first issued.
        /// https://developers.line.me/en/docs/messaging-api/reference/#oauth
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="channelId">ChannelId</param>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">URI</param>
        /// <returns>ChannelAccessToken</returns>
        public async Task<ChannelAccessToken> IssueChannelAccessTokenAsync()
        {
            HttpResponseMessage response = await _client.PostAsync($"{_uri}/oauth/accessToken",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = _channelId,
                    ["client_secret"] = _channelAccessToken
                })).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChannelAccessToken>(json,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });
        }

        /// <summary>
        /// Revokes a channel access token.
        /// https://developers.line.me/en/docs/messaging-api/reference/#revoke-channel-access-token
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">URI</param>
        public async Task RevokeChannelAccessTokenAsync()
        {
            HttpResponseMessage response = await _client.PostAsync($"{_uri}/oauth/revoke",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["access_token"] = _channelAccessToken
                })).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }
    }
}
