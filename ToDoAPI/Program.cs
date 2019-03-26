using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ToDoAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}