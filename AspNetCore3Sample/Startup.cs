using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xamarin.AspNetCore.Auth;

namespace Sample3
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
			services.AddControllers();

			services.AddXamarinAuth(o =>
			{
				o.CallbackUri = new Uri("myapp://");
			});

			services.AddAuthentication()
				.AddCookie()
				.AddFacebook(fb =>
				{
					fb.AppId = "2344250882335158";
					fb.AppSecret = "7ec567a843ec9681a91f9ccc6694e260";
					fb.SaveTokens = true;
				})
				.AddAppleSignIn(new AppleSignInOptions
				{
					ServerId = "com.altusapps",
					TeamId = "85HMA3YHJX",
					KeyId = "9B2VXC6589",
					P8Key = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQg2nJcwOukSetKCWTaTqmHUZR7KWA+CXd/MlTqrTxx7dGgCgYIKoZIzj0DAQehRANCAAR46XGOQgzUyhhBgcxqLkdW1/EeVCzOH/8msTKaZVUEmnOe9FLa97+EgLPdeBxdAFTyB+oyDG71coDLZ05w/xUc",
				}, c =>
				{
					
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			
			app.UseHttpsRedirection();

			app.UseXamarinAuth();

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
