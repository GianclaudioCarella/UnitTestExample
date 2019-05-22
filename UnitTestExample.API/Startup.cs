using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using UnitTestExample.Business.Data;
using UnitTestExample.Business.Models;
using UnitTestExample.Business.Services;

namespace UnitTestExample.API
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));
            services.AddScoped<BillService>();
            services.AddScoped<UserService>();
            services.AddScoped<TripService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SplitTripBills", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BillService billService)
        {
            //User user = new User() { UserId = Guid.NewGuid(), Name = "Gian", Email = "gian@gmail.com", Total = 0 };
            //User user2 = new User() { UserId = Guid.NewGuid(), Name = "Rocio", Email = "rocio@gmail.com", Total = 0 };
            //User user3 = new User() { UserId = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com", Total = 0 };

            //Trip trip = new Trip() { TripId = Guid.NewGuid(), Location = "Paris" };

            //billService.Add((
            //    new Bill()
            //    {
            //        BillId = Guid.NewGuid(),
            //        Debit = 655.90,
            //        Users = new List<User>() { user, user2 }
            //    }
            //));

            //billService.Add((
            //    new Bill()
            //    {
            //        BillId = Guid.NewGuid(),
            //        Debit = 98.40,
            //        Users = new List<User>() { user, user3 }
            //    }
            //));

            //billService.Add((
            //    new Bill()
            //    {
            //        BillId = Guid.NewGuid(),
            //        Debit = 1955.66,
            //        Users = new List<User>() { user, user2, user3 }
            //    }
            //));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SplitTripBills V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
