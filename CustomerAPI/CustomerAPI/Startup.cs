using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAPI.Data.DataContext;
using CustomerAPI.Data.Repositories;
using CustomerAPI.Domain;
using CustomerAPI.Domain.Mapping;
using CustomerAPI.Messaging.Send.Configuration;
using CustomerAPI.Messaging.Send.Sender;
using CustomerAPI.Service.Command;
using CustomerAPI.Service.Query;
using MediatR;
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

namespace CustomerAPI
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

            services.AddDbContext<CustomerDataContext>(options => options.UseInMemoryDatabase(databaseName: "Customer1"));

            services.AddAutoMapper(typeof(CustomerMapper));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddMediatR(typeof(CreateCustomerCommandHandler).Assembly, typeof(GetCustomerByIdHandle).Assembly);

            services.Configure<RabbitConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddTransient<ICustomerUpdateSender, CustomerUpdateSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Customer Api",
                    Description = "This API is responsible to create or update data customers",
                    Contact = new OpenApiContact
                    {
                        Name = "Alejandro de Hombre",
                        Email = "adehombre@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
