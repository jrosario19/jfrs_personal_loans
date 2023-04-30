﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.CompanyConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jfrs_personal_loans
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<JFRSPersonalLoansDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("JFRSPersoalLoansConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<JFRSPersonalLoansDBContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters { 
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "jfrspersonalloans.com",
                ValidAudience = "jfrspersonalloans.com",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Super_secret_key"])),
                ClockSkew = TimeSpan.Zero
            });

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().AddJsonOptions(ConfigureJson);

            services.AddHttpContextAccessor();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<ICompanyConfigurationService, CompanyConfigurationService>();
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICompanyConfigurationService companyConfigurationService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();

            //if (!companyConfigurationService.GetCompanyConfigurations().ToList().Any())
            //{
            //    companyConfigurationService.(new CompanyConfiguration() { Name="JFRS Personal Loans", 
            //                                                              Address="Calle Juan Aguado No. 30, El Almirante, Santo Domingo Este, Republica Dominicana", Currency="RD$", PhoneNumber="+1 (829) 637-1908", IsActive=true, CreatedByUser=})
            //}
        }
    }
}
