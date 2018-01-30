namespace Pixlr.Cmd.Actions.ScanRelative
{
    using System.Linq;

    public class Action : IAction<Scan.Request, Response>
    {
        public Response Execute(Scan.Request request)
        {
            var scan = new Scan.Action();
            var res = scan.Execute(request);
            var url = request.Url.EndsWith("/") ? request.Url : request.Url + "/";
            var matches = res.Matches.Select(x => $"{url}{x}");
            return new Response(request.Url, matches);
        }
    }
}