namespace Pixlr.Cmd.Actions.Scan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Flurl.Http;

    public class Action : IAction<Request, Response>
    {
        public Response Execute(Request args)
        {
            var matches = Scan(args.Url, args.Tag, args.Attr, args.Value);
            return new Response(matches);
        }

        private static IEnumerable<string> Scan(
            string url,
            string tag,
            string attr,
            string value)
        {
            var html = url.GetStringAsync().Result;
            var pattern = $@"<{tag}.*{attr}=""({value})""";
            var matches = Regex.Matches(html, pattern);
            return AsEnumerable(matches).Select(x => x.Groups[1].Value);
        }

        private static IEnumerable<Match> AsEnumerable(MatchCollection matches)
        {
            foreach (var match in matches)
            {
                yield return (Match)match;
            }
        }
    }
}
