using System.Threading.Tasks;

namespace LineDC.Messaging.Webhooks
{
    public interface IWebhookApplication
    {
        Task RunAsync(string xLineSignature, string requestBody);
    }
}
