using LineDC.Messaging.Webhooks.Events;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace LineDC.Messaging.Webhooks
{
    public class Webhook
    {
        public string Destination { get; }
        public IEnumerable<WebhookEvent> Events { get; }

        public Webhook(string destination, IEnumerable<WebhookEvent> events)
        {
            Destination = destination;
            Events = events;
        }

        /// <summary>
        /// Parse a webhook request
        /// </summary>
        /// <param name="requestBody">request body</param>
        /// <returns>Webhook object</returns>
        public static Webhook Parse(string requestBody)
        {
            dynamic json = JsonConvert.DeserializeObject(requestBody);
            return new Webhook(
                (string)json.destination ?? string.Empty,
                ParseEvents(json.events));
        }
        private static IEnumerable<WebhookEvent> ParseEvents(dynamic events)
        {
            foreach (var ev in events)
            {
                var webhookEvent = WebhookEvent.CreateFrom(ev);
                if (webhookEvent == null) { continue; }
                yield return webhookEvent;
            }
        }
    }
}
