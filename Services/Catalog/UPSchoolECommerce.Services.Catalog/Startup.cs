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
using System.Linq;
using System.Threading.Tasks;
using UPSchoolECommerce.Services.Catalog.Services;
using UPSchoolECommerce.Services.Catalog.Settings;

namespace UPSchoolECommerce.Services.Catalog
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
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
			{
				opt.Authority = Configuration["IdentityServerURL"];
				opt.Audience = "Resources_Catalog";
				opt.RequireHttpsMetadata = false;
			});

			services.AddScoped<ICategorySerrvice, CategoryService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddAutoMapper(typeof(Startup));

			//çift tırnak içers,ndeki ifade appsettings.json doyasından alındı 
			services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

			services.AddSingleton<IDatabaseSettings>(sp =>
			{
				return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
			});

			services.AddControllers(opt=> 
			{
				opt.Filters.Add(new AuthorizeFilter());
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "UPSchoolECommerce.Services.Catalog", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UPSchoolECommerce.Services.Catalog v1"));
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
