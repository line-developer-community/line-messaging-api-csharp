using LineDC.Messaging.Webhooks;
using Microsoft.Extensions.Logging;

namespace LineDC.Messaging
{
    public interface ILoggableWebhookApplication : IWebhookApplication
    {
        ILogger Logger { get; set; }
    }
}
