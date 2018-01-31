namespace Pixlr.Cmd.Actions.Download
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Response
    {
        public IEnumerable<string> DownloadedFiles { get; set; }

        public override string ToString() =>
            string.Join(Environment.NewLine, this.DownloadedFiles);
    }
}
