namespace Pixlr.Cmd.Actions.Scan
{
    using PowerArgs;

    public class Request
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription("The URL to scan")]
        public string Url { get; set; }
        
        [ArgPosition(2)]
        [ArgDefaultValue("a")]
        [ArgDescription("The tag to scan for")]
        public string Tag { get; set; }

        [ArgPosition(3)]
        [ArgDefaultValue("href")]
        [ArgDescription("The attribute of the tag")]
        public string Attr { get; set; }

        [ArgPosition(4)]
        [ArgDefaultValue(@"\d+\.jpg")]
        [ArgDescription("The pattern of the value")]
        public string Pattern { get; set; }   
    }
}
