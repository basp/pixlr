namespace Pixlr.Cmd.Actions.ScanAbsolute
{
    using System.Linq;

    public class Action : IAction<Scan.Request, Scan.Response>
    {
        public Scan.Response Execute(Scan.Request request)
        {
            var scan = new Scan.Action();
            var res = scan.Execute(request);
            return new Response(res.Matches);
        }
    }
}