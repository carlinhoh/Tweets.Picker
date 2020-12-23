
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweets.Picker.Infra.Data;
using Tweets.Picker.Infra.Data.Imp;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway.Imp;

namespace Tweets.Picker.Infra.CrossCutting.DI
{
    public class DIFactory
    {
        public static void ConfigureDI(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITwitterServiceProvider, TwitterServiceProvider>();
            services.AddScoped<ITweetRepository, TweetRepository>();
        }
    }
}
