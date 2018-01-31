namespace Pixlr.Cmd.Actions.Download
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Response
    {
        public IEnumerable<string> Files { get; set; }

        public override string ToString() =>
            string.Join(Environment.NewLine, this.Files);
    }
}
