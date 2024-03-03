using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Http;
using BattleShip.APP;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();


builder.Services.AddHttpClient("ServerAPI",
        client => client.BaseAddress = new Uri(builder.Configuration["ServerAPI"]))
    .AddHttpMessageHandler(sp =>
    {
        var httpMessageHandler = sp.GetService<AuthorizationMessageHandler>()?
            .ConfigureHandler(authorizedUrls: new[] { builder.Configuration["ServerAPI"] });

        return httpMessageHandler ?? throw new NullReferenceException(nameof(AuthorizationMessageHandler));
    });

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("ServerAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("https://localhost:5023", new GrpcChannelOptions { HttpClient = httpClient });

    return new BattleshipService.BattleshipServiceClient(channel);
});

await builder.Build().RunAsync();
