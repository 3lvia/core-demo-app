using System;
using System.Linq;
using k8s;

namespace core_demo_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting core-demo-app");
            var config = KubernetesClientConfiguration.InClusterConfig();
            var client = new Kubernetes(config);
            while (true)
            {
                var name = "";
                for (int i = 0; i < 1000; i++)
                {

                    var result = client.CoreV1.ListNamespacedConfigMapWithHttpMessagesAsync("vault").Result;
                    result.Response.EnsureSuccessStatusCode();
                    name = result.Body.Items
                        .Select(cm => cm.Metadata.Name)
                        .FirstOrDefault();
                    System.Threading.Thread.Sleep(10);
                }

                Console.WriteLine(DateTime.Now.ToString("u") + ": " + name);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
