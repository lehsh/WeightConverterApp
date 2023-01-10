using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WeightConverterJsonAPI;
using static WeightConverterApp.Messages;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

СonversionMeasureOfWeight measuresOfWeight = new СonversionMeasureOfWeight();

app.MapGet("/ping", async (context) =>
{
    await context.Response.WriteAsync("pong");
});

app.MapGet("/status", async (context) =>
{
    StatusMessage status = new StatusMessage("https://weightconverterapp-alexey.amvera.io/", 
        new Uri("https://weightconverterapp-alexey.amvera.io").Port);
    await context.Response.WriteAsJsonAsync(status);
});

app.MapGet("/info", async (context) =>
{
    InfoMessage infoStatus = new InfoMessage("GET /status", "Информация о сервере, номер порта");
    InfoMessage infoConversion = new InfoMessage("POST /conversion", "Конвертация мер веса");
    InfoMessages infoMessage = new InfoMessages(new List<InfoMessage> { infoStatus, infoConversion });

    await context.Response.WriteAsJsonAsync(infoMessage);
});

app.MapPost("/conversion", async (context) =>
{
    MeasureOfWeight? measure = null;
    try
    {
        measure = await context.Request.ReadFromJsonAsync<MeasureOfWeight>();
        Console.WriteLine(measuresOfWeight);
    }
    catch { }

    if (measure is null)
    {
        ErrorMessage error = new ErrorMessage("Invalid request paramters");
        Console.WriteLine(error);
        await context.Response.WriteAsJsonAsync(error);
    }
    else
    {
        try
        {
            measuresOfWeight.ChangeValue(measure);
            Console.WriteLine(measuresOfWeight);
            await context.Response.WriteAsJsonAsync(measuresOfWeight);
        }
        catch (Exception ex)
        {
            ErrorMessage error = new ErrorMessage($"Error during request processing: {ex.Message}");
            Console.WriteLine(error);
            await context.Response.WriteAsJsonAsync(error);
        }
    }
});

app.Run();