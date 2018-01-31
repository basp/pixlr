using PowerArgs;

namespace Pixlr.Cmd.Actions.Download
{
    public enum ScanMode
    {
        Relative = 1,
        Absolute = 2,
    }

    public class Request : Scan.Request
    {
        [ArgDescription("The output directory")]
        [ArgRequired]
        public string OutputDirectory { get; set; }

        [ArgDescription("Whether to treat paths as absolute or relative")]
        [ArgDefaultValue(ScanMode.Relative)]
        public ScanMode ScanMode { get; set; }
    }
}