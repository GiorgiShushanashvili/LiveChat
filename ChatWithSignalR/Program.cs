using ChatWithSignalR;
using ChatWithSignalR.ChatType;
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
    endpoint.MapHub<LiveChatHub>("/liveChat");
    endpoint.MapHub<GroupChat>("/group");
});

//app.UseHttpsRedirection();
//app.MapHub<ChatHub>("chat-hub");


app.Run();
