using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nagarro.NAGP.EBroker.Business.Services;
using Nagarro.NAGP.EBroker.DAL.Data;
using Nagarro.NAGP.EBroker.DAL.Repo;
using System;

namespace Nagarro.NAGP.EBroker
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
            services.AddDbContext<EBrokerDBContext>(options => options.UseInMemoryDatabase(databaseName: "EBrokerDB"));
            services.AddScoped<EBrokerDBContext>();
            services.AddTransient<IEquityService, EquityService>();
            services.AddTransient<IEquityRepo, EquityRepo>();
            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Load initial data
            serviceProvider.GetService<EBrokerDBContext>().AddTestData();
        }

    }
}
