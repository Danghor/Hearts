using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HeartsServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
        }
    }
}
