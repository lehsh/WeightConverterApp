using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using WeightConverterApp.Model;
using WeightConverterApp.Model.Entity;
using WeightConverterApp.Service;
using WeightConverterJsonAPI;
using static WeightConverterApp.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeightConverterDbContext>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    WeightConverterDbContext db = new WeightConverterDbContext();
    KnownHostServices knownHost = new KnownHostServices(db);
    RequestServices request = new RequestServices(db);
    KnownHost? curentHost = new KnownHost();
    curentHost = knownHost.AddHost(new KnownHost { Ip = context.Connection.RemoteIpAddress.ToString() });
    request.AddRequest( new Request { Body = context.Request.Body.ToString(), HostId = curentHost.Id, Host = curentHost } );
    await next.Invoke();
});

СonversionMeasureOfWeight measuresOfWeight = new СonversionMeasureOfWeight();

app.MapGet("/ping", async (context) =>
{
    await context.Response.WriteAsync("pong");
});

app.MapGet("/status", async (context) =>
{
    StatusMessage status = new StatusMessage(context.Connection.RemoteIpAddress.ToString(), context.Connection.LocalPort);
    await context.Response.WriteAsJsonAsync(status);
});

app.MapGet("/info", async (context) =>
{
    InfoMessage infoStatus = new InfoMessage("GET /status", "Информация о сервере, номер порта");
    InfoMessage infoConversion = new InfoMessage("POST /conversion", "Конвертация мер веса");
    InfoMessage InfoKnownHosts = new InfoMessage("GET /known-hosts", "Список известных хостов");
    InfoMessage InfoHostRequests = new InfoMessage("GET /host-requests", "Список всех запросов");
    InfoMessages infoMessage = new InfoMessages(new List<InfoMessage> { infoStatus, infoConversion, InfoKnownHosts, InfoHostRequests }, $"API format: {new InfoMeasureOfWeight("Gram", 1000)}" +
      $"  Доступные для конвертации меры веса: Gram - Грамм, Kilogram - Килограмм, Stone - Стоун, Pound - Фунт, Ounce - Унция, Dram - Драм, Grain - Гран");

    await context.Response.WriteAsJsonAsync(infoMessage);
});

app.MapGet("/known-hosts", async (context) =>
{
    WeightConverterDbContext db = new WeightConverterDbContext();
    var hosts = db.KnownHosts.ToList();
    await context.Response.WriteAsJsonAsync(hosts);
});

app.MapGet("/host-requests", async (context) =>
{
    WeightConverterDbContext db = new WeightConverterDbContext();
    var requests = db.Requests.ToList();
    await context.Response.WriteAsJsonAsync(requests);
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