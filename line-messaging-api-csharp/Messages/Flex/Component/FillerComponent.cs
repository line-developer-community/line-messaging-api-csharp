namespace LineDC.Messaging.Messages.Flex
{
    /// <summary>
    /// This is an invisible component to fill extra space between components.
    /// </summary>
    public class FillerComponent : IFlexComponent
    {

        public FlexComponentType Type => FlexComponentType.Filler;
    }
}
