﻿using System;

namespace LineDC.Messaging.Messages.Flex
{
    public class BubbleContainerFlexMessage : FlexMessage
    {
        /// <summary>
        /// Sets a Bubble container to the FlexMessage object.
        /// </summary>
        /// <param name="bubbleContainer"></param>
        /// <returns>Flex Message</returns>
        public BubbleContainerFlexMessage SetBubbleContainer(BubbleContainer bubbleContainer)
        {
            Contents = bubbleContainer ?? throw new ArgumentNullException(nameof(bubbleContainer));
            return this;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="altText">alt text</param>
        public BubbleContainerFlexMessage(string altText) : base(altText)
        {

        }

        /// <summary>
        /// Sets a QuickReply object to the FlexMessage object.
        /// </summary>
        /// <param name="quickReply"></param>
        /// <returns>Flex Message</returns>
        public BubbleContainerFlexMessage SetQuickReply(QuickReply quickReply)
        {
            QuickReply = quickReply;
            return this;
        }

        /// <summary>
        /// Sets a Sender object to the FlexMessage object.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>Flex Message</returns>
        public BubbleContainerFlexMessage SetSender(Sender sender)
        {
            Sender = sender;
            return this;
        }
    }
}
