using System.Collections.Generic;

namespace LineDC.Messaging.Messages.Flex
{
    public class CarouselContainer : IFlexContainer
    {
        public FlexContainerType Type => FlexContainerType.Carousel;

        /// <summary>
        /// Array of bubble containers. Max: 10 bubbles
        /// <para>(Required)</para>
        /// </summary>
        public IList<BubbleContainer> Contents { get; set; } = new List<BubbleContainer>();

        public CarouselContainer()
        {
        }
    }
}
