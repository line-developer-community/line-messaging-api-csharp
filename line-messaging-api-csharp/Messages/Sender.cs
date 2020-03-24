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
        public string Name { get; set; }

        /// <summary>
        /// URL of the image to display as an icon when sending a message. (Max: 1000 characters)
        /// HTTPS
        /// </summary>
        public string IconUrl { get; set; }
    }
}
