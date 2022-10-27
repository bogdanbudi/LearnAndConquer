using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tutorial.Infrastructure.Data;
using Tutorial.Infrastructure.Repository;
//using Microsoft.IdentityModel.Tokens;

namespace Tutorial.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tutorial.API", Version = "v1" });
            });

            services.AddScoped<ITutorialContext, TutorialContext>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("ElearningPolicy", policy =>
            //    {
            //        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            //        //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            //    });
            //});

           // services.AddAuthentication("Bearer")
           //.AddJwtBearer("Bearer", options =>
           //{
           //    options.Authority = "https://localhost:5005";
           //    options.TokenValidationParameters = new TokenValidationParameters
           //    {
           //        ValidateAudience = false
           //    };
           //});

           // services.AddAuthorization(options =>
           // {
           //     options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
           // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutorial.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("ElearningPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
