namespace Pixlr.Cmd.Actions.ScanRelative
{
    using System.Collections.Generic;

    public class Response : Scan.Response
    {
        public Response(string url, IEnumerable<string> matches)
            : base(matches)
        {
        }
    }
}