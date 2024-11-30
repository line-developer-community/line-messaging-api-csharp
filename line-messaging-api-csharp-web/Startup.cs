
using LineDC.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.Get<LineBotSettings>() ?? throw new Exception();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ILineMessagingClient, LineMessagingClient>(httpClient =>
    LineMessagingClient.Create(httpClient, settings.ChannelAccessToken));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapSwagger();
app.MapPost("webhook", async (
    HttpRequest request,
    [FromHeader(Name = "x-line-signature")] string xLineSignature,
    [FromServices] ILineMessagingClient line,
    [FromServices] IOptions<LineBotSettings> settings) =>
{
    var bot = new BotApp(line, settings.Value.ChannelSecret, settings.Value.BotUserId);

    var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();

    try
    {
        app.Logger.LogTrace($"RequestBody: {body}");
        await bot.RunAsync(xLineSignature, body);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e.Message);
    }
});

app.Run();
