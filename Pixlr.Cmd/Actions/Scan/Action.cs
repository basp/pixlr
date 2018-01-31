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
            var matches = Scan(args.Url, args.Tag, args.Attr, args.Pattern);
            return new Response(matches);
        }

        private static IEnumerable<string> Scan(
            string url,
            string tag,
            string attr,
            string pattern)
        {
            var html = url.GetStringAsync().Result;
            pattern = $@"<{tag}.*{attr}=""({pattern})""";
            var matches = Regex.Matches(html, pattern);
            return AsEnumerable(matches)
                .Select(x => GetAbsoluteUrl(url, x.Groups[1].Value));
        }

        private static string GetAbsoluteUrl(string baseUrl, string imgUrl) =>
            Uri.IsWellFormedUriString(imgUrl, UriKind.Absolute)
                ? imgUrl
                : $"{baseUrl}{imgUrl}";

        private static IEnumerable<Match> AsEnumerable(MatchCollection matches)
        {
            foreach (var match in matches)
            {
                yield return (Match)match;
            }
        }
    }
}
