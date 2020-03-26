using System;

namespace LineDC.Messaging.Messages
{
    /// <summary>
    /// Text
    /// https://developers.line.me/en/docs/messaging-api/reference/#text
    /// </summary>
    public class TextMessage : ISendMessage
    {        
        public MessageType Type { get; } = MessageType.Text;

        /// <summary>
        /// These properties are used for the quick reply feature
        /// </summary>
        public QuickReply QuickReply { get; set; }

        /// <summary>
        /// When sending a message from the LINE Official Account, you can specify the sender in Message objects.
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// Message text
        /// Max: 2000 characters
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">
        /// Message text
        /// Max: 2000 characters
        /// </param>
        /// <param name="quickReply">
        /// QuickReply
        /// </param>
        /// <param name="sender">
        /// Sender
        /// </param>
        public TextMessage(string text, QuickReply quickReply = null, Sender sender = null)
        {
            Text = text.Substring(0, Math.Min(text.Length, 2000));
            QuickReply = quickReply;
            Sender = sender;
        }
    }
}
