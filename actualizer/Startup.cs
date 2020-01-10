using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Okta.Sdk;
using Okta.AspNetCore;
using Okta.Sdk.Configuration;
using Microsoft.AspNetCore.Authentication;
using actualizer.Security.claims.transformation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using actualizer.Infastructure.Data.Actualizer.db;

namespace actualizer {
    public class Startup {

        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {


            services.AddDbContext<ActualizerContext>(options => options.UseSqlite("Data Source=Data.db"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);



            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Actualizer", Version = "v1" });
            });


            //var client = new OktaClient(new OktaClientConfiguration {
            //    OktaDomain = "https://dev-839928.okta.com/",
            //    Token = "00jwXF0-ir7_Vn7HbUWb7LEeArC3MpJJEDVkRbVQwn"
            //});

            //services.AddSingleton<IOktaClient, OktaClient>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            })
            .AddOktaWebApi(new OktaWebApiOptions() {
                OktaDomain = "https://dev-839928.okta.com/"
            });


            services.AddAuthorization(options => {
                options.AddPolicy("CanMakeAnalyticsRequests", policy => policy.RequireClaim("CanMakeAnalyticsRequests"));
            });

            services.AddCors(options => {
                options.AddPolicy("VueCorsPolicy", builder => {
                    builder
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins("http://localhost:8080");
                });
            });

            services.AddTransient<IClaimsTransformation, UserTransformer>();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env) {
            //Swagger MIddleware
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Actualizer");
                c.RoutePrefix = string.Empty;

            });
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }


            app.UseCors("VueCorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();





        }
    }
}
