using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using To_Do.Providers;

[assembly: OwinStartup(typeof(To_Do.Startup))]
namespace To_Do
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
               AllowInsecureHttp = true,
               TokenEndpointPath = new PathString("/token"),
               AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
               Provider = new SimpleAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    OnRequestToken = context =>
                    {
                        var cookie = context.OwinContext.Request.Cookies["access_token"];

                        if (!string.IsNullOrEmpty(cookie))
                            context.Token = cookie;

                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}