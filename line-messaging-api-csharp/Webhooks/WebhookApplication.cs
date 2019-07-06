using LineDC.Messaging.Exceptions;
using LineDC.Messaging.Webhooks.Events;
using System.Threading.Tasks;

namespace LineDC.Messaging.Webhooks
{
    /// <summary>
    /// Inherit this class to implement LINE Bot. Then override each event handler.
    /// </summary>
    public abstract class WebhookApplication : IWebhookApplication
    {
        protected ILineMessagingClient Client { get; }
        protected string BotUserId { get; }
        protected string ChannelSecret { get; }

        public WebhookApplication(ILineMessagingClient client, string channelSecret, string botUserId = null)
        {
            Client = client;
            BotUserId = botUserId;
            ChannelSecret = channelSecret;
        }

        public async Task RunAsync(string xLineSignature, string requestBody)
        {
            if (!XLineSignature.Verify(ChannelSecret, xLineSignature, requestBody))
            {
                throw new InvalidSignatureException("Signature validation faild.");
            }
            var webhook = Webhook.Parse(requestBody);
            if (!string.IsNullOrEmpty(BotUserId) && (webhook.Destination != BotUserId))
            {
                throw new UserIdMismatchException("Bot user ID does not match.");
            }
            await RunAsync(webhook);
        }

        protected async Task RunAsync(Webhook webhook)
        {
            foreach (var ev in webhook.Events)
            {
                switch (ev)
                {
                    case MessageEvent message:
                        await OnMessageAsync(message).ConfigureAwait(false);
                        break;
                    case JoinEvent join:
                        await OnJoinAsync(join).ConfigureAwait(false);
                        break;
                    case LeaveEvent leave:
                        await OnLeaveAsync(leave).ConfigureAwait(false);
                        break;
                    case FollowEvent follow:
                        await OnFollowAsync(follow).ConfigureAwait(false);
                        break;
                    case UnfollowEvent unFollow:
                        await OnUnfollowAsync(unFollow).ConfigureAwait(false);
                        break;
                    case PostbackEvent postback:
                        await OnPostbackAsync(postback).ConfigureAwait(false);
                        break;
                    case BeaconEvent beacon:
                        await OnBeaconAsync(beacon).ConfigureAwait(false);
                        break;
                    case AccountLinkEvent accountLink:
                        await OnAccountLinkAsync(accountLink).ConfigureAwait(false);
                        break;
                    case MemberJoinEvent memberJoin:
                        await OnMemberJoinAsync(memberJoin).ConfigureAwait(false);
                        break;
                    case MemberLeaveEvent memberLeave:
                        await OnMemberLeaveAsync(memberLeave).ConfigureAwait(false);
                        break;
                    case DeviceLinkEvent deviceLink:
                        await OnDeviceLinkAsync(deviceLink).ConfigureAwait(false);
                        break;
                    case DeviceUnlinkEvent deviceUnlink:
                        await OnDeviceUnlinkAsync(deviceUnlink).ConfigureAwait(false);
                        break;

                }
            }
        }

        protected virtual Task OnMessageAsync(MessageEvent ev) => Task.CompletedTask;

        protected virtual Task OnJoinAsync(JoinEvent ev) => Task.CompletedTask;

        protected virtual Task OnLeaveAsync(LeaveEvent ev) => Task.CompletedTask;

        protected virtual Task OnFollowAsync(FollowEvent ev) => Task.CompletedTask;

        protected virtual Task OnUnfollowAsync(UnfollowEvent ev) => Task.CompletedTask;

        protected virtual Task OnBeaconAsync(BeaconEvent ev) => Task.CompletedTask;

        protected virtual Task OnPostbackAsync(PostbackEvent ev) => Task.CompletedTask;

        protected virtual Task OnAccountLinkAsync(AccountLinkEvent ev) => Task.CompletedTask;

        protected virtual Task OnMemberJoinAsync(MemberJoinEvent ev) => Task.CompletedTask;

        protected virtual Task OnMemberLeaveAsync(MemberLeaveEvent ev) => Task.CompletedTask;

        protected virtual Task OnDeviceLinkAsync(DeviceLinkEvent ev) => Task.CompletedTask;

        protected virtual Task OnDeviceUnlinkAsync(DeviceUnlinkEvent ev) => Task.CompletedTask;
    }
}
