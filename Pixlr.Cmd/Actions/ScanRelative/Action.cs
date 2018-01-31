namespace Pixlr.Cmd.Actions.ScanRelative
{
    using System;
    using System.Linq;

    public class Action : IAction<Scan.Request, Scan.Response>
    {
        public Scan.Response Execute(Scan.Request request)
        {
            var scan = new Scan.Action();
            var res = scan.Execute(request);
            var uri = new Uri(request.Url);
            var url = uri.Normalize();
            var matches = res.Matches.Select(x => $"{url}{x}");
            return new Response(request.Url, matches);
        }
    }
}