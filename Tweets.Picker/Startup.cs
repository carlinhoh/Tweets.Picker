using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using Swashbuckle.Swagger;
using Tweets.Picker.App.Services;
using Tweets.Picker.App.Services.Imp;
using Tweets.Picker.Infra.Data;
using Tweets.Picker.Infra.Data.Imp;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway.Imp;
using Tweets.Picker.Services;
using Tweets.Picker.Services.Imp;

namespace Tweets.Picker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Tweet Filter.",
                        Version = "v1",
                        Description = "An api that consumes the twitter api and fetches data with the given expression.",
                        Contact = new OpenApiContact
                        {
                            Name = "Carlos Luciano e Felipe Diniz",
                            Url = new Uri("https://github.com/carlinhoh")
                        }
                    });
            });
            services.AddMvc();

            services.AddHealthChecks();

            ConfigureDI(services, Configuration);
         
        }

        private void ConfigureDI(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITwitterApplicationService, TwitterApplicationService>();
            services.AddScoped<ITwitterServiceProvider, TwitterServiceProvider>();

            services.AddScoped<ITwitterService, TwitterService>();
            services.AddScoped<ITweetRepository, TweetRepository>();

            services.AddScoped<ITextApplicationService, TextApplicationService>();
            services.AddScoped<ITextService, TextService>();
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tweets Picker");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
