using ChatWithSignalR;
using ChatWithSignalR.Database;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
    endpoint.MapHub<ChatHub>("/chat");
    endpoint.Map("/get-cookie", ctx =>
    {
        ctx.Response.StatusCode = 200;
        ctx.Response.Cookies.Append("/",Guid.NewGuid().ToString(),new()
        {
            
        });
        return ctx.Response.WriteAsync("");
    });
    endpoint.Map("/token", ctx =>
    {
        ctx.Response.StatusCode = 200;
        return ctx.Response.WriteAsync(ctx.User.Claims.FirstOrDefault(x => x.Type == "ff")?.Value);
        clai = ctx.User.Claims.Select(x => new { x.Type, x.Value });
    }).RequireAuthorization("Token");
});

//app.UseHttpsRedirection();
//app.MapHub<ChatHub>("chat-hub");


app.Run();
