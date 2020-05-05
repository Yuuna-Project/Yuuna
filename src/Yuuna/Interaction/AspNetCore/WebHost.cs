// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Interaction.AspNetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using System.Threading.Tasks;

    using Yuuna.Contracts.Evaluation;
    using Yuuna.Contracts.Interaction;
    using Yuuna.TextSegmention;

    public class Startup
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var segmenter = new JiebaTextSegmenter();
            var canResponses = new Response[]
            {
                    (Moods.Sad, "我不清楚你想做什麼 OvQ"),
                    (Moods.Sad, "我不懂你想幹嘛 QAQ"),
                    (Moods.Sad, "我不知道你想幹嘛 OHQ"),
            };
            var modules = ModuleManager.Instance.Modules;
            var strategy = default(IStrategy);
            var actor = new Actor(segmenter, canResponses, strategy, modules);
            services.AddSingleton(actor);
            services.AddControllers();
        }
    }
}