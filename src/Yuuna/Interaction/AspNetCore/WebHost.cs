// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Interaction.AspNetCore
{
    using System.Threading.Tasks; 
    using Yuuna.Contracts.Interaction;
    using Yuuna.TextSegmention;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;

    public sealed class WebHost 
    {
        public static async Task RunAsync()
        {
            await Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://0.0.0.0:5000")
                        .UseStartup<Startup>();
                })
                .Build()
                .RunAsync();
        }
         

       private class Startup
        {

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                var canResponses = new Response[]
                {
                    (Moods.Sad, "我不清楚你想做什麼 OvQ"),
                    (Moods.Sad, "我不懂你想幹嘛 QAQ"),
                    (Moods.Sad, "我不知道你想幹嘛 OHQ"),
                };
                services.AddSingleton(new Actor(new JiebaTextSegmenter(), canResponses, default, ModuleManager.Instance.Modules));

                services.AddControllers();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                //app.UseHttpsRedirection();

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }

    }
}