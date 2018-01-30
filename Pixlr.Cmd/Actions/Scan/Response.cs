namespace Pixlr.Cmd.Actions.Scan
{
    using System;
    using System.Collections.Generic;

    public class Response
    {
        public Response(IEnumerable<string> matches)
        {
            this.Matches = matches;
        }

        public virtual IEnumerable<string> Matches { get; }

        public override string ToString() =>
            string.Join(Environment.NewLine, Matches);
    }
}
