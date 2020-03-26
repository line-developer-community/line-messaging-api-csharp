using System;

namespace LineDC.Messaging.Messages
{
    /// <summary>
    /// When sending a message from the LINE Official Account, you can specify the sender in Message objects.
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// Display name. Certain words such as LINE may not be used. (Max: 20 characters)
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// URL of the image to display as an icon when sending a message. (Max: 1000 characters)
        /// HTTPS
        /// </summary>
        public string IconUrl { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// Display name. Certain words such as LINE may not be used. (Max: 20 characters)
        /// </param>
        /// <param name="iconUrl">
        /// URL of the image to display as an icon when sending a message. (Max: 1000 characters)
        /// HTTPS
        /// </param>
        public Sender(string name, string iconUrl)
        {
            Name = name.Substring(0, Math.Min(name.Length, 20));
            IconUrl = iconUrl;
        }
    }
}
