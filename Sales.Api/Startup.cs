using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOrder.Api.Models.Configuration;
using SalesOrder.Domain.Configuration;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;

namespace Sales.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Sales Api", Version = "v1"
                });
                c.DescribeAllEnumsAsStrings();

//                var dir = new DirectoryInfo(PlatformServices.Default.Application.ApplicationBasePath);
//                foreach (var fi in dir.EnumerateFiles("*.xml"))
//                {
//                    c.IncludeXmlComments(fi.FullName);
//                }
            });

            services.Configure<RavenDbSettings>(Configuration.GetSection("RavenDbSettings"));
            services.AddMvc();

            return ConfigureIoC(services);
        }
        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup));
                    _.WithDefaultConventions();
                });

                config.AddRegistry<DomainRegistry>();
                config.AddRegistry<ModelRegistry>();
                config.For<IMapper>().Use(SetupMapper());

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();

        }

        private IMapper SetupMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<SalesOrderHandlerMapProfile>();
            });

            return config.CreateMapper();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales Api");
            });
            app.UseMvc();
        }
    }
}