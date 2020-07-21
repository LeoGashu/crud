using CadastroRepository.Interfaces;
using CadastroRepository.Mock;
using CadastroRepository.Mongo;
using CadastroRepository.Sqlite;
using CadastroRepository.TypeHandlers;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CrudBasico
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var dbType = Configuration.GetSection("DBType").Get<string>();
            //Serviços
            switch (dbType)
            {
                case "Mongo":
                    string mongoConnectionString = Configuration["ConnectionStrings:MongoDBConnectionString:ConnectionString"];
                    string mongoDatabaseName = Configuration["ConnectionStrings:MongoDBConnectionString:DatabaseName"];
                    string mongoCollectionName = Configuration["ConnectionStrings:MongoDBConnectionString:CollectionName"];

                    services.AddSingleton<IPessoaRepository>(new PessoasMongoRepository(mongoConnectionString, mongoDatabaseName, mongoCollectionName));
                    break;
                case "Sqlite":
                    var connection = Configuration["ConnectionStrings:SqliteConnectionString"];

                    services.AddSingleton<IPessoaRepository>(new PessoaRepository(connection));
                    SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
                    SqlMapper.RemoveTypeMap(typeof(Guid));
                    SqlMapper.RemoveTypeMap(typeof(Guid?));
                    break;
                case "Mock":
                default:
                    services.AddSingleton<IPessoaRepository>(new PessoaRepositoryMock());
                    break;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
