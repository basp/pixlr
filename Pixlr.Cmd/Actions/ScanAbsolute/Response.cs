namespace Pixlr.Cmd.Actions.ScanAbsolute
{
    using System.Collections.Generic;

    public class Response : Actions.Scan.Response
    {
        public Response(IEnumerable<string> matches)
            : base(matches)
        {
            
        }
    }
}