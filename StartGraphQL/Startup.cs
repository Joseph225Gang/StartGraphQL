using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StartGraphQL.Contracts;
using StartGraphQL.Entities.Context;
using StartGraphQL.GraphQL.GraphQLSchema;
using StartGraphQL.Repository;

namespace StartGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")));
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            // ���UIDependencyResolver����
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            // ���UAppSchema����
            services.AddScoped<AppSchema>();
            // ���UGraphQL�H��GraphTypes����A�䤤AddGraphTypes()�|�۰����ڭ̵��U�Ҧ�GraphQL Types�A�٤U�ڭ̭ӧO���U�C�@��GraphQL Type���ɶ��C
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // �bConfigure���[�JGraphQL Middleware�A�H��GraphQL Client�ݴ��ե�Middleware GraphQLPlayground
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseMvc();
        }
    }
}
