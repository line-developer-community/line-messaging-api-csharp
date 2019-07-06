using LineDC.Messaging.Configurations;
using LineDC.Messaging.Webhooks;
using LineDC.Messaging.Webhooks.Events;
using LineDC.Messaging.Webhooks.Messages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LineDC.Messaging
{
    public class LineBotApp : WebhookApplication, ILoggableWebhookApplication
    {
        private readonly LineBotSettings settings;

        public ILogger Logger { get; set; }

        public LineBotApp(ILineMessagingClient client, LineBotSettings settings)
            : base(client, settings.ChannelSecret, settings.BotUserId)
        {
            this.settings = settings;
        }

        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            Logger?.LogTrace("OnMessageAsync => {Source: {Type: {0}, Id: {1}}", ev.Source.Type, ev.Source.Id);
            switch (ev.Message)
            {
                case TextEventMessage textMessage:
                    await Client.ReplyMessageAsync(ev.ReplyToken, textMessage.Text);
                    break;
                case MediaEventMessage mediaMessage:
                    await Client.ReplyMessageAsync(ev.ReplyToken, $"contentProvider: {mediaMessage.ContentProvider}");
                    break;
                case FileEventMessage fileMessage:
                    await Client.ReplyMessageAsync(ev.ReplyToken, $"filename: {fileMessage.FileName}");
                    break;
                case LocationEventMessage locationMessage:
                    await Client.ReplyMessageAsync(ev.ReplyToken, $"{locationMessage.Title}({locationMessage.Latitude}, {locationMessage.Longitude})");
                    break;
                case StickerEventMessage stickerMessage:
                    await Client.ReplyMessageAsync(ev.ReplyToken, $"sticker id: {stickerMessage.PackageId}-{stickerMessage.StickerId}");
                    break;
            }

        }

    }
}
