using Serilog;
using System;

namespace FodySerilogErrorSample
{
    class Program
    {
        static Program()
        {
            SetupLoggerConfig();
        }

        static void Main(string[] args)
        {
            //Success
            var normal = new NormalClass();

            //Success
            StaticClassSuccess.WriteLog();

            //Exception!
            var failCall = StaticClassFail.PropertyForCallConstructor;

            Console.Read();
        }

        private static void SetupLoggerConfig()
        {
            string template = "| {Timestamp:HH:mm:ss.fff} | {Message:j} | {SourceContext} | {MethodName} | {LineNumber} L|{NewLine}{Exception}";

            Serilog.Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console(outputTemplate: template)
                            .CreateLogger();
        }
    }

    class NormalClass
    {
        public NormalClass()
        {
            //Success
            Anotar.Serilog.LogTo.Information("Normal Constructor");
        }
    }

    static class StaticClassFail
    {
        public static string PropertyForCallConstructor { get; set; }

        static StaticClassFail()
        {
            //Success
            Serilog.Log.Information("Static Constructor from Plane-Serilog");
            //Exception!
            Anotar.Serilog.LogTo.Information("Static Constructor from Fody-Serilog");
        }
    }

    static class StaticClassSuccess
    {
        public static void WriteLog()
        //Success
            => Anotar.Serilog.LogTo.Information("Static Method");
    }
}
