namespace Pixlr.Cmd.Actions.Download
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Flurl.Http;

    public class Action : IAction<Request, Response>
    {
        private readonly IAction<Scan.Request, Scan.Response> scan;
        private readonly System.Action<string> onProgress;

        public Action(IAction<Scan.Request, Scan.Response> scan)
            : this(scan, path => { })
        {
        }

        public Action(
            IAction<Scan.Request, Scan.Response> scan,
            System.Action<string> onProgress)
        {
            this.scan = scan;
            this.onProgress = onProgress;
        }

        public Response Execute(Request request)
        {
            var urls = this.Scan(request);
            var files = new List<string>();
            foreach (var url in urls)
            {
                var path = url.DownloadFileAsync(request.OutputDirectory).Result;
                files.Add(path);
                this.onProgress(path);
            }

            return new Response { DownloadedFiles = files };
        }

        private IEnumerable<string> Scan(Request request) =>
            this.scan.Execute(request).Matches;
    }
}