using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.UOF;
using TrueNorthChallenge.Contracts.Managers;
using TrueNorthChallenge.Contracts.Repositories;
using TrueNorthChallenge.DAL;
using TrueNorthChallenge.DAL.Repositories;
using TrueNorthChallenge.Managers;
using TrueNorthChallenge.DAL.UOF;
using TrueNorthChallenge.Middlewares;

namespace TrueNorthChallenge
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
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            
            services.AddDbContext<TrueNorthContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Local")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrueNorthChallenge", Version = "v1" });
            });

            // Managers
            services.AddScoped<IPostManager, PostManager>();
            services.AddScoped<ITagManager, TagManager>();

            // Repositories
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            // Infraestrucutre
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrueNorthChallenge v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<TrueNorthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
