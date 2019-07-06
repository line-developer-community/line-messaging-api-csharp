namespace LineDC.Messaging.Messages
{
    /// <summary>
    /// Image size. 
    /// </summary>
    public class ImageSize
    {
        /// <summary>
        /// Default rich menu size
        /// </summary>
        public static ImageSize RichMenuLong { get; } = new ImageSize(2500, 1686);
        
        /// <summary>
        /// Half rich menu size.
        /// </summary>
        public static ImageSize RichMenuShort { get; } = new ImageSize(2500, 843);

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; }

        public ImageSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
    
