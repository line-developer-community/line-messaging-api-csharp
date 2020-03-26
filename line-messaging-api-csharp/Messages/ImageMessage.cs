namespace LineDC.Messaging.Messages
{
    /// <summary>
    /// Image
    /// https://developers.line.me/en/docs/messaging-api/reference/#image
    /// </summary>
    public class ImageMessage : ISendMessage
    {
        public MessageType Type { get; } = MessageType.Image;

        /// <summary>
        /// These properties are used for the quick reply feature
        /// </summary>
        public QuickReply QuickReply { get; set; }

        /// <summary>
        /// When sending a message from the LINE Official Account, you can specify the sender in Message objects.
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// Image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 1024 x 1024
        /// Max: 1 MB
        /// </summary>
        public string OriginalContentUrl { get; }

        /// <summary>
        /// Preview image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 240 x 240
        /// Max: 1 MB
        /// </summary>
        public string PreviewImageUrl { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalContentUrl">
        /// Image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 1024 x 1024
        /// Max: 1 MB
        /// </param>
        /// <param name="previerImageUrl">
        /// Preview image URL (Max: 1000 characters)
        /// HTTPS
        /// JPEG
        /// Max: 240 x 240
        /// Max: 1 MB
        /// </param>
        /// <param name="quickReply">
        /// QuickReply
        /// </param>
        /// <param name="sender">
        /// Sender
        /// </param>
        public ImageMessage(string originalContentUrl, string previerImageUrl, QuickReply quickReply = null, Sender sender = null)
        {
            OriginalContentUrl = originalContentUrl;
            PreviewImageUrl = previerImageUrl;
            QuickReply = quickReply;
            Sender = sender;
        }
    }
}
