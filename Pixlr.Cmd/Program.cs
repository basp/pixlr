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
                new Actions.ScanRelative.Action(),
                path => Console.Write("."));
                
            var res = act.Execute(req);
            Console.WriteLine(res);
        }

        [ArgActionMethod]
        [ArgDescription("Scan relatively")]
        public static void Scanr(Actions.Scan.Request req)
        {
            var act = new Actions.ScanRelative.Action();
            var res = act.Execute(req);
            Console.WriteLine(res);
        }

        [ArgActionMethod]
        [ArgDescription("Scan absolutely")]
        public static void Scana(Actions.Scan.Request req)
        {
            var act = new Actions.ScanAbsolute.Action();
            var res = act.Execute(req);
            Console.WriteLine(res);
        }

        static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
