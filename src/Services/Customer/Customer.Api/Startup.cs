using System.Linq;
using Customer.Api.Infrastructure;
using Customer.Api.Validators;
using Customer.Data;
using Customer.Service.Contact;
using Customer.Service.Customer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace Customer.Api
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
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ResponseWrapper));
                })
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssemblyContaining(typeof(AddNewCustomerRequestModelValidator)))
                .ConfigureApiBehaviorOptions(x =>
                {
                    x.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                        return new BadRequestObjectResult(errors.FirstOrDefault());
                    };
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Customer.Api", Version = "v1"});
            });
            
            var dbHost = Configuration.GetValue<string>("DBHOST");
            if (string.IsNullOrEmpty(dbHost))
                dbHost = "database";
            var connString = string.Format(Configuration.GetConnectionString("DbConnection"), dbHost);
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseMySql(new MySqlConnection(connString), ServerVersion.AutoDetect(connString), b => b.MigrationsAssembly("Customer.Api")));
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddHttpClient();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IContactService, ContactService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.Api v1"));
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}