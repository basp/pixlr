using PowerArgs;

namespace Pixlr.Cmd.Actions.Download
{
    public class Request : Scan.Request
    {
        [ArgDescription("The output directory")]
        [ArgRequired]
        [ArgShortcut("-o")]
        public string OutputDirectory { get; set; }
    }
}