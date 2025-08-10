using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    var _googleAuthOptions = builder.Configuration.GetSection("GoogleAuth").Get<GoogleAuthOptions>();
    if(_googleAuthOptions != null)
    {
        googleOptions.ClientId = _googleAuthOptions.ClientId;
        googleOptions.ClientSecret = _googleAuthOptions.ClientSecret;
    }
    else
    {
        throw new InvalidOperationException("Google OAuth конфигурация не найдена. Проверьте UserSecrets или appsettings.json.");
    }
    
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/login", async (context) =>
{
    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/"
    });
});

app.Run();


class GoogleAuthOptions
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

}
