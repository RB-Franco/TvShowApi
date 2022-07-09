using Application.Application;
using Application.AutoMapper;
using Application.Interface;
using AutoMapper;
using Domain.Interfaces;
using Entity.Entity;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using TvShowWebApi.Token;

namespace TvShowWebApi
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
            Mapper.Initialize(AutoMapperConfiguration.RegisterMappings);

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<Context>();

            services.AddSingleton<IUser, RepositoryUser>();
            services.AddSingleton<ITvShow, RepositoryTvShow>();

            services.AddSingleton<IApplicationUser, ApplicationUser>();
            services.AddSingleton<IApplicationTvShow, ApplicationTvShow>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(option =>
                   {
                       option.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,

                           ValidIssuer = "Teste.Securiry.Bearer",
                           ValidAudience = "Teste.Securiry.Bearer",
                           IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                       };

                       option.Events = new JwtBearerEvents
                       {
                           OnAuthenticationFailed = context =>
                           {
                               Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                               return Task.CompletedTask;
                           },
                           OnTokenValidated = context =>
                           {
                               Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                               return Task.CompletedTask;
                           }
                       };
                   });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TvShowWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var urlCliente3 = "http://localhost:4200";
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader().WithOrigins(urlCliente3));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvShowWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
