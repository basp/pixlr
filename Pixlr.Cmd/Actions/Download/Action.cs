namespace Pixlr.Cmd.Actions.Download
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public class Action : IAction<Request, Response>
    {
        private readonly ConcurrentDictionary<string, bool> status =
            new ConcurrentDictionary<string, bool>();

        public Response Execute(Request request)
        {
            var scan = new Scan.Action();
            var files = scan.Execute(new Scan.Request
            {

            });

            throw new NotImplementedException();
        }
    }
}