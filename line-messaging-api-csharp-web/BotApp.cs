using LineDC.Messaging;
using LineDC.Messaging.Webhooks;
using LineDC.Messaging.Webhooks.Events;
using LineDC.Messaging.Webhooks.Messages;

class BotApp : WebhookApplication
{
    public BotApp(ILineMessagingClient client, string channelSecret, string? botUserId = null)
        : base(client, channelSecret, botUserId)
    {
    }
    protected override async Task OnMessageAsync(MessageEvent ev)
    {
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