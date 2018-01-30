namespace Pixlr.Cmd.Actions.Scan
{
    using PowerArgs;

    public class Request
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription("The URL to scan")]
        public string Url { get; set; }
        
        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription("The tag to scan for")]
        public string Tag { get; set; }

        [ArgPosition(3)]
        [ArgDescription("The attribute of the tag")]
        public string Attr { get; set; }

        [ArgPosition(4)]
        [ArgDescription("The value of the attribute")]
        public string Value { get; set; }   
    }
}
