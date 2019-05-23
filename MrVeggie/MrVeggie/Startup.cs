using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;

namespace MrVeggie {

    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            var connection = @"Server=DESKTOP-F88H89P;Database=MrVeggie;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<UtilizadorContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<IngredienteContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ReceitaContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<PassoContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<IngredientesPassoContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UtilizadorIngredientesPrefContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UtilizadorReceitasPrefContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UnidadeContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UtensilioContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UtensiliosReceitaContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/UserView/LoginUtilizador/";

                    });

            services.AddMvc().AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
    
}
