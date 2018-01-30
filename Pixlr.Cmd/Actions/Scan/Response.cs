namespace Pixlr.Cmd.Actions.Scan
{
    using System;
    using System.Collections.Generic;

    public class Response
    {
        public IEnumerable<string> Matches { get; set; }

        public override string ToString() =>
            string.Join(Environment.NewLine, Matches);
    }
}
