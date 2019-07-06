using System.Collections.Generic;

namespace LineDC.Messaging.Configurations
{
    public class LineBotSettings
    {
        public string ChannelSecret { get; set; }
        public string ChannelAccessToken { get; set; }
        public string BotUserId { get; set; }
        public IList<string> DebugUsers { get; set; }
    }
}
