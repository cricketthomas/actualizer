using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Okta.AspNetCore;
using Microsoft.OpenApi.Models;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using Microsoft.AspNetCore.Authentication;
using actualizer.Security.claims;

namespace actualizer {
    public class Startup {

        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);



            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Actualizer", Version = "v1" });
            });


            var client = new OktaClient(new OktaClientConfiguration {
                OktaDomain = "https://dev-839928.okta.com",
                Token = "00v9TMD0d5oAq7tssPHRFtlmRwQ7wh2V1FLOrU9g2C"
            });

            services.AddSingleton<IOktaClient, OktaClient>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            })
            .AddOktaWebApi(new OktaWebApiOptions() {
                OktaDomain = "https://dev-839928.okta.com"
            });

            services.AddSingleton<IClaimsTransformation, UserTransformer>();

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
