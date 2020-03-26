using LineDC.Messaging.Messages.Imagemap;
using System;
using System.Collections.Generic;

namespace LineDC.Messaging.Messages
{
    /// <summary>
    /// Imagemaps are images with one or more links. You can assign one link for the entire image or multiple links which correspond to different regions of the image.
    /// https://developers.line.me/en/docs/messaging-api/reference/#imagemap-message
    /// </summary>
    public class ImagemapMessage : ISendMessage
    {
        public MessageType Type { get; } = MessageType.Imagemap;

        /// <summary>
        /// These properties are used for the quick reply feature
        /// </summary>
        public QuickReply QuickReply { get; set; }

        /// <summary>
        /// When sending a message from the LINE Official Account, you can specify the sender in Message objects.
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// Base URL of image (Max: 1000 characters)
        /// HTTPS
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Alternative text
        /// Max: 400 characters
        /// </summary>
        public string AltText { get; }

        /// <summary>
        /// Width of base image (set to 1040px）
        /// Height of base image（set to the height that corresponds to a width of 1040px）
        /// </summary>
        public ImageSize BaseSize { get; }

        /// <summary>
        /// Video to play on imagemap
        /// </summary>
        public Video Video { get; }

        /// <summary>
        /// Action when tapped.
        /// Max: 50
        /// </summary>
        public IList<IImagemapAction> Actions { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">
        /// Base URL of image (Max: 1000 characters)
        /// HTTPS
        /// </param>
        /// <param name="altText">
        /// Alternative text
        /// Max: 400 characters
        /// </param>
        /// <param name="baseSize">
        /// Width of base image (set to 1040px）
        /// Height of base image（set to the height that corresponds to a width of 1040px）
        /// </param>
        /// <param name="actions">
        /// Action when tapped.
        /// Max: 50
        /// </param>
        /// <param name="quickReply">
        /// QuickReply
        /// </param>
        /// <param name="video">
        /// Video to play on imagemap
        /// </param>
        /// <param name="sender">
        /// Sender
        /// </param>
        public ImagemapMessage(string baseUrl, string altText, ImageSize baseSize, IList<IImagemapAction> actions, QuickReply quickReply = null, Video video = null, Sender sender = null)
        {
            BaseUrl = baseUrl;
            AltText = altText.Substring(0, Math.Min(altText.Length, 400)); ;
            BaseSize = baseSize;
            Actions = actions;
            QuickReply = quickReply;
            Video = video;
            Sender = sender;
        }
    }
}
