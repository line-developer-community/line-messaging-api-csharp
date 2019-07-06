using LineDC.Messaging.Messages.Templates;

namespace LineDC.Messaging.Messages.Imagemap
{
    public interface IImagemapAction
    {
        ImagemapActionType Type { get; }
        ImageArea Area { get; }
    }    
}
