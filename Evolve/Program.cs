namespace Evolve
{
    using System;
    using Nancy.Hosting.Self;
    class Program
    {
        static void Main(string[] args)
        {
            var uri =
                new Uri(System.Configuration.ConfigurationManager.AppSettings["HTTPBaseAddress"]);

            using (var host = new NancyHost(uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
