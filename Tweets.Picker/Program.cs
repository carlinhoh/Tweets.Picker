using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway.Imp;
using Tweets.Picker.Services;
using Tweets.Picker.Services.Imp;

namespace Tweets.Picker
{
    public class Program
    {
        public static void Main(string[] args) =>
           BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
