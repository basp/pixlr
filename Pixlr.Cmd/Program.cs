namespace Pixlr.Cmd
{
    using System;
    using PowerArgs;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class Program
    {
        [HelpHook]
        public static bool Help { get; set; }

        [ArgActionMethod]
        [ArgDescription("Download matches")]
        public static void Download(Actions.Download.Request req)
        {
            var act = new Actions.Download.Action(
                new Actions.Scan.Action(),
                path => Console.Write("."));

            var res = act.Execute(req);
            Console.WriteLine();
            Console.WriteLine(res);
        }

        [ArgActionMethod]
        [ArgDescription("Scan without downloading")]
        public static void Scan(Actions.Scan.Request req)
        {
            var act = new Actions.Scan.Action();
            var res = act.Execute(req);
            Console.WriteLine(res);
        }

        private static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
