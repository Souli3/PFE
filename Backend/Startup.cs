using Backend.Data;
using Backend.Logic;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Authentification;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ConnectionStrings") != null)
            {
                services.AddDbContext<DataContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings"), options => options.SetPostgresVersion(9, 6)));

            }
            else
            {
                services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), options => options.SetPostgresVersion(9, 6)));

            }
            //services.AddCors();
            //services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithMethods("GET")));
            
            services.AddDbContext<IDataContext, DataContext>();
            //services.AddSingleton<IDataContext>(porvider => porvider.GetService<DataContext>());
            services.AddScoped<IMembresLogic, MemberLogic>();
            services.AddScoped<IRegisterLogic, RegisterLogic>();
            services.AddScoped<IRegisterServices, RegisterServices>();
            services.AddScoped<IMembreServices, MembreServices>();
            services.AddScoped<IAnnonceLogic, AnnonceLogic>();
            services.AddScoped<IAnnonceServices, AnnonceService>();
            services.AddScoped<ICategorieLogic, CategorieLogic>();
            services.AddScoped<ICategorieService, CategorieService>();
            services.AddScoped<IAdresseLogic, AdresseLogic>();
            services.AddScoped<IAdresseService, AdresseService>();
            services.AddScoped<IMediaLogic, MediaLogic>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IAnnonceAdresseService, AnnonceAdresseService>();
            services.AddScoped<IDiscussionLogic, DiscussionLogic>();
            services.AddScoped<IDiscussionService, DiscussionService>();
            services.AddScoped<IMailLogic, MailLogic>();
            services.AddControllers();
            services.AddAuthentication(authOption =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                var key = Configuration.GetValue<String>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ValidateAudience=false,
                    ValidateIssuer=false

                };
            });
            services.AddScoped(typeof(IJwtTokenManager), typeof(JwtTokenManager));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ngrok http localhost:26934 -host-header="localhost:5000"
            app.UseCors(
                options =>
                {
                    options.WithOrigins(Configuration["Domains:FrontEnd"]).AllowAnyMethod().AllowAnyHeader();
                    options.WithOrigins("https://frontend-dev-pfe.herokuapp.com").AllowAnyMethod().AllowAnyHeader();
                    options.WithOrigins("https://frontend-prod-pfe.herokuapp.com").AllowAnyMethod().AllowAnyHeader();
                }
            ); 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Medias")),
                RequestPath = new PathString("/Medias")
            }) ;

            //app.UseCors("AllowAll");
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
