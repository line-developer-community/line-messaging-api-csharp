﻿using LineDC.Messaging.Messages;
using LineDC.Messaging.Messages.RichMenu;
using LineDC.Messaging.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LineDC.Messaging
{
    /// <summary>
    /// LINE Messaging API client, which handles request/response to LINE server.
    /// </summary>
    public class LineMessagingClient : ILineMessagingClient
    {
        protected const string DEFAULT_URI = "https://api.line.me/v2";
        protected const string DEFAULT_DATA_URI = "https://api-data.line.me/v2";
        protected static readonly HttpClient _client = new HttpClient();
        protected readonly JsonSerializerSettings _jsonSerializerSettings;
        protected readonly string _uri;
        protected readonly string _dataUri;
        protected readonly AuthenticationHeaderValue _authorization;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">Request URI</param>
        /// <param name="dataUri">Request URI for uploading and downloading media data</param>
        protected LineMessagingClient(string channelAccessToken, string uri, string dataUri)
        {
            _authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);
            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _jsonSerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
            _uri = uri;
            _dataUri = dataUri;
        }

        /// <summary>
        /// Instantiate <see cref="LineDC.Messaging.LineMessagingClient"/> by using "long tarm" access token.
        /// </summary>
        /// <param name="channelAccessToken">channel access token</param>
        /// <param name="uri">URI</param>
        /// <param name="dataUri">Request URI for uploading and downloading media data</param>
        /// <returns>Instance of <see cref="LineDC.Messaging.LineMessagingClient"/></returns>
        public static LineMessagingClient Create(string channelAccessToken, string uri = DEFAULT_URI, string dataUri = DEFAULT_DATA_URI)
        {
            return new LineMessagingClient(channelAccessToken, uri, dataUri);
        }

        /// <summary>
        /// Instantiate <see cref="LineDC.Messaging.LineMessagingClient"/> by using OAuth.
        /// <para>
        /// https://developers.line.me/en/docs/messaging-api/reference/#oauth
        /// </para>
        /// </summary>
        /// <param name="channelId">ChannelId</param>
        /// <param name="channelSecret">ChannelSecret</param>
        /// <param name="uri">URI</param>
        /// <param name="dataUri">Request URI for uploading and downloading media data</param>
        /// <returns>Instance of <see cref="LineDC.Messaging.LineMessagingClient"/></returns>
        public static async Task<LineMessagingClient> CreateAsync(string channelId, string channelSecret,
            string uri = DEFAULT_URI, string dataUri = DEFAULT_DATA_URI)
        {
            if (string.IsNullOrEmpty(channelId)) { throw new ArgumentNullException(nameof(channelId)); }
            if (string.IsNullOrEmpty(channelSecret)) { throw new ArgumentNullException(nameof(channelSecret)); }

            var oAuthClient = new OAuthClient(channelId, channelSecret, uri);
            var accessToken = await oAuthClient.IssueChannelAccessTokenAsync();
            return new LineMessagingClient(accessToken.AccessToken, uri, dataUri);
        }


        #region Message 
        // https://developers.line.me/en/docs/messaging-api/reference/#message

        /// <summary>
        /// Respond to events from users, groups, and rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        public virtual async Task ReplyMessageAsync(string replyToken, IList<ISendMessage> messages)
        {
            var json = JsonConvert.SerializeObject(new { replyToken, messages }, _jsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/reply", content);
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply Text messages. Up to 5 messages.</param>
        public virtual Task ReplyMessageAsync(string replyToken, params string[] messages)
        {
            return ReplyMessageAsync(replyToken, messages.Select(msg => new TextMessage(msg)).ToArray());
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual async Task ReplyMessageWithJsonAsync(string replyToken, params string[] messages)
        {
            var json =
$@"{{ 
    ""replyToken"" : ""{replyToken}"", 
    ""messages"" : [{string.Join(", ", messages)}]
}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/reply", content);
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// Note: Use of push messages are limited to certain plans.
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        public virtual async Task PushMessageAsync(string to, IList<ISendMessage> messages)
        {
            var json = JsonConvert.SerializeObject(new { to, messages }, _jsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/push", content);
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// Note: Use of push messages are limited to certain plans.
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual async Task PushMessageWithJsonAsync(string to, params string[] messages)
        {
            var json =
$@"{{ 
    ""to"" : ""{to}"", 
    ""messages"" : [{string.Join(", ", messages)}]
}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/push", content);
        }

        /// <summary>
        /// Send text messages to a user, group, or room at any time.
        /// Note: Use of push messages are limited to certain plans.
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        public virtual Task PushMessageAsync(string to, params string[] messages)
        {
            return PushMessageAsync(to, messages.Select(msg => new TextMessage(msg)).ToArray());
        }

        /// <summary>
        /// Send push messages to multiple users at any time.
        /// Only available for plans which support push messages. Messages cannot be sent to groups or rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-multicast-messages
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 150 users</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        public virtual async Task MultiCastMessageAsync(IList<string> to, IList<ISendMessage> messages)
        {
            var json = JsonConvert.SerializeObject(new { to, messages }, _jsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/multicast", content);
        }

        /// <summary>
        /// Send push messages to multiple users at any time.
        /// Only available for plans which support push messages. Messages cannot be sent to groups or rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-multicast-messages
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 150 users</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual async Task MultiCastMessageWithJsonAsync(IList<string> to, params string[] messages)
        {
            var json =
$@"{{ 
    ""to"" : [{string.Join(", ", to.Select(x => "\"" + x + "\""))}], 
    ""messages"" : [{string.Join(", ", messages)}] 
}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/message/multicast", content);
        }

        /// <summary>
        /// Send push text messages to multiple users at any time.
        /// Only available for plans which support push messages. Messages cannot be sent to groups or rooms
        /// https://developers.line.me/en/docs/messaging-api/reference/#send-multicast-messages
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 150 users</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        public virtual Task MultiCastMessageAsync(IList<string> to, params string[] messages)
        {
            return MultiCastMessageAsync(to, messages.Select(msg => new TextMessage(msg)).ToArray());
        }

        /// <summary>
        /// Retrieve image, video, and audio data sent by users as Stream
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as ContentStream</returns>
        public virtual async Task<HttpContent> GetContentAsync(string messageId)
        {
            var response = await SendAsync(HttpMethod.Get, $"{_dataUri}/bot/message/{messageId}/content", null);
            return response.Content;
        }

        /// <summary>
        /// Retrieve image, video, and audio data sent by users as byte array
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as byte array</returns>
        public virtual async Task<byte[]> GetContentBytesAsync(string messageId)
        {
            var response = await SendAsync(HttpMethod.Get, $"{_dataUri}/bot/message/{messageId}/content", null);
            return await response.Content.ReadAsByteArrayAsync();
        }
        #endregion

        #region Profile
        // https://developers.line.me/en/docs/messaging-api/reference/#profile

        /// <summary>
        /// Get user profile information.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-profile
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public virtual async Task<UserProfile> GetUserProfileAsync(string userId)
        {
            var content = await GetStringAsync($"{_uri}/bot/profile/{userId}");
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }
        #endregion

        #region Group
        // https://developers.line.me/en/docs/messaging-api/reference/#group

        /// <summary>
        /// Gets the user profile of a member of a group that the bot is in. This includes user profiles of users who have not added the bot as a friend or have blocked the bot.
        /// Use the group ID and user ID returned in the source object of webhook event objects. Do not use the LINE ID used in the LINE app. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-profile
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>User Profile</returns>
        public virtual async Task<UserProfile> GetGroupMemberProfileAsync(string groupId, string userId)
        {
            var content = await GetStringAsync($"{_uri}/bot/group/{groupId}/member/{userId}");
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }

        /// <summary>
        /// Gets the user IDs of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// Users who have not agreed to the Official Accounts Terms of Use are not included in memberIds. There is no fixed number of memberIds. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-user-ids
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        public virtual async Task<GroupMemberIdsResponse> GetGroupMemberIdsAsync(string groupId, string continuationToken)
        {
            var requestUrl = $"{_uri}/bot/group/{groupId}/members/ids";
            if (continuationToken != null)
            {
                requestUrl += $"?start={continuationToken}";
            }
            var content = await GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<GroupMemberIdsResponse>(content);
        }

        /// <summary>
        /// Gets the user profiles of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <returns>List of UserProfile</returns>
        public virtual async Task<IList<UserProfile>> GetGroupMemberProfilesAsync(string groupId)
        {
            var result = new List<UserProfile>();
            string continuationToken = null;
            do
            {
                var ids = await GetGroupMemberIdsAsync(groupId, continuationToken);

                var tasks = ids.MemberIds.Select(async userId => await GetGroupMemberProfileAsync(groupId, userId));
                var profiles = await Task.WhenAll(tasks.ToArray());

                result.AddRange(profiles);
                continuationToken = ids.Next;
            }
            while (continuationToken != null);
            return result;
        }

        /// <summary>
        /// Leave a group.
        /// Use the ID that is returned via webhook from the source group. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#leave-group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns></returns>
        public virtual async Task LeaveFromGroupAsync(string groupId)
        {
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/group/{groupId}/leave", null);
        }
        #endregion

        #region Room
        // https://developers.line.me/en/docs/messaging-api/reference/#room

        /// <summary>
        /// Gets the user profile of a member of a room that the bot is in. This includes user profiles of users who have not added the bot as a friend or have blocked the bot.
        /// Use the room ID and user ID returned in the source object of webhook event objects. Do not use the LINE ID used in the LINE app
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <param name="userId">Identifier of the user</param>
        /// <returns></returns>
        public virtual async Task<UserProfile> GetRoomMemberProfileAsync(string roomId, string userId)
        {
            var content = await GetStringAsync($"{_uri}/bot/room/{roomId}/member/{userId}");
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }

        /// <summary>
        /// Gets the user IDs of the members of a room that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the room ID returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-room-member-user-ids
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        public virtual async Task<GroupMemberIdsResponse> GetRoomMemberIdsAsync(string roomId, string continuationToken = null)
        {
            var requestUrl = $"{_uri}/bot/room/{roomId}/members/ids";
            if (continuationToken != null)
            {
                requestUrl += $"?start={continuationToken}";
            }
            var content = await GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<GroupMemberIdsResponse>(content);
        }

        /// <summary>
        /// Gets the user profiles of the members of a room that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the room ID returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <returns>List of UserProfiles</returns>
        public virtual async Task<IList<UserProfile>> GetRoomMemberProfilesAsync(string roomId)
        {
            var result = new List<UserProfile>();
            string continuationToken = null;
            do
            {
                var ids = await GetRoomMemberIdsAsync(roomId, continuationToken);

                var tasks = ids.MemberIds.Select(async userId => await GetRoomMemberProfileAsync(roomId, userId));
                var profiles = await Task.WhenAll(tasks.ToArray());

                result.AddRange(profiles);
                continuationToken = ids.Next;
            }
            while (continuationToken != null);
            return result;
        }

        /// <summary>
        /// Leave a room.
        /// Use the ID that is returned via webhook from the source room. 
        /// </summary>
        /// <param name="roomId">Room ID</param>
        public virtual async Task LeaveFromRoomAsync(string roomId)
        {
            await SendAsync(HttpMethod.Post, $"{_uri}/bot/room/{roomId}/leave", null);
        }
        #endregion

        #region Rich menu
        // https://developers.line.me/en/docs/messaging-api/reference/#rich-menu

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns>RichMenu</returns>
        public virtual async Task<RichMenu> GetRichMenuAsync(string richMenuId)
        {
            var json = await GetStringAsync($"{_uri}/bot/richmenu/{richMenuId}");
            return JsonConvert.DeserializeObject<ResponseRichMenu>(json);
        }

        /// <summary>
        /// Creates a rich menu. 
        /// Note: You must upload a rich menu image and link the rich menu to a user for the rich menu to be displayed.You can create up to 10 rich menus for one bot.
        /// The rich menu represented as a rich menu object.
        /// https://developers.line.me/en/docs/messaging-api/reference/#create-rich-menu
        /// </summary>
        /// <param name="richMenu">RichMenu</param>
        /// <returns>RichMenu Id</returns>
        public virtual async Task<string> CreateRichMenuAsync(RichMenu richMenu)
        {
            var json = JsonConvert.SerializeObject(richMenu, _jsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await SendAsync(HttpMethod.Post, $"{_uri}/bot/richmenu", content);
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(responseJson, new { richMenuId = "" }).richMenuId;
        }

        /// <summary>
        /// Deletes a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#delete-rich-menu
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        public virtual async Task DeleteRichMenuAsync(string richMenuId)
            => await SendAsync(HttpMethod.Delete, $"{_uri}/bot/richmenu/{richMenuId}", null);

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-id-of-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>RichMenu Id</returns>
        public virtual async Task<string> GetRichMenuIdOfUserAsync(string userId)
        {
            var json = await GetStringAsync($"{_uri}/bot/user/{userId}/richmenu");
            return JsonConvert.DeserializeAnonymousType(json, new { richMenuId = "" }).richMenuId;
        }

        /// <summary>
        /// Sets a default ritch menu
        /// </summary>
        /// <param name="richMenuId">
        /// ID of an uploaded rich menu
        /// </param>
        public virtual async Task SetDefaultRichMenuAsync(string richMenuId)
            => await SendAsync(HttpMethod.Post, $"{_uri}/bot/user/all/richmenu/{richMenuId}", null);

        /// <summary>
        /// Links a rich menu to a user.
        /// Note: Only one rich menu can be linked to a user at one time.
        /// https://developers.line.me/en/docs/messaging-api/reference/#link-rich-menu-to-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns></returns>
        public virtual async Task LinkRichMenuToUserAsync(string userId, string richMenuId)
            => await SendAsync(HttpMethod.Post, $"{_uri}/bot/user/{userId}/richmenu/{richMenuId}", null);

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#unlink-rich-menu-from-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        public virtual async Task UnLinkRichMenuFromUserAsync(string userId)
            => await SendAsync(HttpMethod.Delete, $"{_uri}/bot/user/{userId}/richmenu", null);

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#download-rich-menu-image
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns>Image as <see cref="HttpContent"/></returns>
        public virtual async Task<HttpContent> DownloadRichMenuImageAsync(string richMenuId)
        {
            var response = await SendAsync(HttpMethod.Get, $"{_dataUri}/bot/richmenu/{richMenuId}/content", null);
            return response.Content;
        }

        /// <summary>
        /// Uploads and attaches a jpeg image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.me/en/docs/messaging-api/reference/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Jpeg image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        public virtual Task UploadRichMenuJpegImageAsync(Stream stream, string richMenuId)
        {
            return UploadRichMenuImageAsync(stream, richMenuId, "image/jpeg");
        }

        /// <summary>
        /// Uploads and attaches a png image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.me/en/docs/messaging-api/reference/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Png image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        public virtual Task UploadRichMenuPngImageAsync(Stream stream, string richMenuId)
        {
            return UploadRichMenuImageAsync(stream, richMenuId, "image/png");
        }

        protected async Task UploadRichMenuImageAsync(Stream stream, string richMenuId, string mediaType)
        {
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            await SendAsync(HttpMethod.Post, $"{_dataUri}/bot/richmenu/{richMenuId}/content", content);
        }

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-list
        /// </summary>
        /// <returns>List of ResponseRichMenu</returns>
        public virtual async Task<IList<ResponseRichMenu>> GetRichMenuListAsync()
        {
            var response = await SendAsync(HttpMethod.Get, $"{_uri}/bot/richmenu/list", null, ensuresSuccess: false);
            var menus = new List<ResponseRichMenu>();
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return menus;
            }
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            dynamic result = JsonConvert.DeserializeObject(json);
            if (result == null) { return menus; }

            foreach (var dynamicObject in result.richmenus)
            {
                menus.Add(ResponseRichMenu.CreateFrom(dynamicObject));
            }
            return menus;
        }
        #endregion

        #region Account Link
        /// <summary>
        /// Issues a link token used for the account link feature.
        /// <para>https://developers.line.me/en/docs/messaging-api/linking-accounts</para>
        /// </summary>
        /// <param name="userId">
        /// User ID for the LINE account to be linked. Found in the source object of account link event objects. Do not use the LINE ID used in the LINE app.
        /// </param>
        /// <returns>
        /// Returns the status code 200 and a link token. Link tokens are valid for 10 minutes and can only be used once.
        /// Note: The validity period may change without notice.
        /// </returns>
        public virtual async Task<string> IssueLinkTokenAsync(string userId)
        {
            var response = await SendAsync(HttpMethod.Post, $"{_uri}/bot/user/{userId}/linkToken", null);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(content, new { linkToken = "" }).linkToken;
        }

        #endregion

        #region Number of sent messages
        /// <summary>
        /// Gets the number of messages sent with the /bot/message/reply endpoint.
        /// The number of messages retrieved by this operation does not include the number of messages sent from LINE@ Manager.
        /// </summary>
        /// <param name="date">
        /// - Date the messages were sent
        /// - Format: yyyyMMdd(Example: 20191231)
        /// - Timezone: UTC+9
        /// </param>
        /// <returns>
        /// <see cref="LineDC.Messaging.NumberOfSentMessages"/>
        /// </returns>
        public virtual async Task<NumberOfSentMessages> GetNumberOfSentReplyMessagesAsync(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/reply?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfSentMessages>(response);
        }

        /// <summary>
        /// Gets the number of messages sent with the /bot/message/push endpoint.
        /// The number of messages retrieved by this operation does not include the number of messages sent from LINE@ Manager.
        ///</summary>
        /// <param name="date">
        /// - Date the messages were sent
        /// - Format: yyyyMMdd(Example: 20191231)
        /// - Timezone: UTC+9
        /// </param>
        /// <returns>
        /// <see cref="LineDC.Messaging.NumberOfSentMessages"/>
        /// </returns>
        public virtual async Task<NumberOfSentMessages> GetNumberOfSentPushMessagesAsync(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/push?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfSentMessages>(response);
        }

        /// <summary>
        /// Gets the number of messages sent with the /bot/message/push endpoint.
        /// The number of messages retrieved by this operation does not include the number of messages sent from LINE@ Manager.
        /// </summary>
        /// <param name="date">
        /// - Date the messages were sent
        /// - Format: yyyyMMdd(Example: 20191231)
        /// - Timezone: UTC+9
        /// </param>
        /// <returns>
        /// <see cref="LineDC.Messaging.NumberOfSentMessages"/>
        /// </returns>
        public virtual async Task<NumberOfSentMessages> GetNumberOfSentMulticastMessagesAsync(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/multicast?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfSentMessages>(response);
        }
        #endregion

        protected virtual async Task<string> GetStringAsync(string requestUri)
        {
            var response = await SendAsync(HttpMethod.Get, requestUri, null);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> SendAsync(HttpMethod method, string requestUri, HttpContent content, bool ensuresSuccess = true)
        {
            var request = new HttpRequestMessage(method, requestUri);
            request.Headers.Authorization = _authorization;
            request.Content = content;
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            if (ensuresSuccess)
            {
                await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            }
            return response;
        }
    }
}
