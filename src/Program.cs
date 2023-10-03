using System;

namespace core_demo_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting core-demo-app");
            Console.WriteLine($"Instrumentation key: {Elvia.Configuration.HashiVault.HashiVault.EnsureHasValue("core/kv/appinsights/core/instrumentation-key")}");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
