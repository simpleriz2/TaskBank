using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace TaskBank.Services.Auth
{
    public static class AuthServiceExtension
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(googleOptions =>
            {
                var _googleAuthOptions = configuration.GetSection("GoogleAuth").Get<GoogleAuthOptions>();
                if (_googleAuthOptions != null)
                {
                    googleOptions.ClientId = _googleAuthOptions.ClientId;
                    googleOptions.ClientSecret = _googleAuthOptions.ClientSecret;
                }
                else
                {
                    throw new InvalidOperationException("Google OAuth configuration not found. Check UserSecrets or appsettings.json.");
                }

            });

            return services;
        }

        public static WebApplication UseAuthService(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/login", async (context) =>
            {
                bool isLog = context.User.Identity?.IsAuthenticated ?? false;
                if (!isLog)
                {
                    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
                    {
                        RedirectUri = "/"
                    });
                }
                else
                {
                    context.Response.Redirect("/");
                }

            });

            app.MapGet("/logout", async context =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/");
            });

            return app;
        }
    }
}


class GoogleAuthOptions
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

}