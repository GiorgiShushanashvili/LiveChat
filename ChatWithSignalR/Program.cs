using System.Text;
using ChatWithSignalR.ChatType;
using ChatWithSignalR.DataAccess;
using ChatWithSignalR.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSingleton<LiveChatRegistry>();
//builder.Services.AddDbContext<ChatDbContext>();
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretKey").Value))

    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireClaim("Role", "Admin");
    });
    
});*/
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/auth";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
    //endpoint.MapHub<ChatHub>("/chat");
    endpoint.MapHub<LiveChatHub>("/liveChat");
    //endpoint.MapHub<GroupChat>("/group");
});

app.Run();
