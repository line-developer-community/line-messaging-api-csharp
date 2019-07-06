namespace LineDC.Messaging.Messages.Actions
{
    public interface ITemplateAction
    {
        TemplateActionType Type { get; }
        string Label { get; }
    }
}
