using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.IdentityModel.Tokens;

namespace OcelotApiGw
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());
            services.AddCors(opt =>
            {
                opt.AddPolicy("ElearningPolicy", policy =>
                {
                    //policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            var authenticationProviderKey = "IdentityApiKey";

            // NUGET - Microsoft.AspNetCore.Authentication.JwtBearer
            services.AddAuthentication()
             .AddJwtBearer(authenticationProviderKey, x =>
             {
                 x.Authority = "http://localhost:8027"; // IDENTITY SERVER URL
                 x.RequireHttpsMetadata = false;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = false
                 };
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseCors("ElearningPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            await app.UseOcelot();
        }
    }
}
