using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Serrvices;
using UPSchool_Microservices.Services.Basket.Services;
using UPSchool_Microservices.Services.Basket.Settings;

namespace UPSchool_Microservices.Services.Basket
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
			//yarın burayı hoca anlatacak (authos ekranından brekpoint ile bakacağız)
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
			{
				opt.Authority = Configuration["IdentityServerURL"];
				opt.Audience = "Resources_Basket";
				opt.RequireHttpsMetadata = false;

			});
			services.AddHttpContextAccessor();
			services.AddScoped<IsharedIdentityService, SharedIdentityService>();
			services.AddScoped<IBasketService, BasketService>();
			services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
			services.AddSingleton<RedisService>(sp =>
			{
				var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
				var redis = new RedisService(redisSettings.Host, redisSettings.Port);
				redis.Connect();
				return redis;
			});

			services.AddControllers(opt=> {

				opt.Filters.Add(new AuthorizeFilter());
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "UPSchool_Microservices.Services.Basket", Version = "v1" });
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UPSchool_Microservices.Services.Basket v1"));
			}

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
