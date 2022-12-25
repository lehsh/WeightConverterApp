var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/ping", async (context) =>
{
    await context.Response.WriteAsync("pong");
});



app.Run();
