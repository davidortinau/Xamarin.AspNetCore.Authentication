using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.AspNetCore.Auth;

namespace Sample
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
			services.AddXamarinAuth(o =>
			{
				o.CallbackUri = new Uri("myapp://");
			});

			services.AddMvc();
			
			services.AddAuthentication()
				.AddCookie()
				.AddFacebook(fb =>
				{
					fb.AppId = "2344250882335158";
					fb.AppSecret = "7ec567a843ec9681a91f9ccc6694e260";
					// Must save tokens so the middleware can validate
					fb.SaveTokens = true;
					fb.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddAppleSignIn(new AppleSignInOptions
				{
					ServerId = "com.troublefreepool.signin",
					TeamId = "958B4K3AD3",
					KeyId = "XVSK594239",
					P8Key = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgrxpSPvl6s0b5dfjRlyr7vAt5wgZGdRLivN/TiTSbhmagCgYIKoZIzj0DAQehRANCAATOaHN3iLy32w4jbWi2iEF1Cfjyww7TD70kEpYEyeZR8p3knlzvTIwQYSRsFBvVnRpY6wbGVDSH2mCeOiJY3oyq",
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseXamarinAuth();

			app.UseMvc();

			app.UseAuthentication();
		}
	}
}
