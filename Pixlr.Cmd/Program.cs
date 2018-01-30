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
        public static void Scan(Actions.Scan.Request req)
        {
            var act = new Actions.Scan.Action();
            var res = act.Execute(req);
            Console.WriteLine(res.ToString());
        }

        static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
