using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Append.Blazor.Sidepanel;
using Squads.Shared.Sessions;
using Squads.Client.Shared;
using ProtoBuf.Grpc.Client;
using Grpc.Net.Client.Web;
using Squads.Shared.Users;
using MudBlazor.Services;
using Grpc.Net.Client;
using Squads.Client;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSidepanel();


builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton(services =>
{
    string backendUrl;

    // Get the service address from appsettings.json
    var config = services.GetRequiredService<IConfiguration>();

    backendUrl = builder.HostEnvironment.IsDevelopment() ? config.GetConnectionString("DevelopmentUrl") : config.GetConnectionString("AcceptanceUrl");

    // If no address is set then fallback to the current webpage URL
    if (string.IsNullOrEmpty(backendUrl))
    {
        var navigationManager = services.GetRequiredService<NavigationManager>();
        backendUrl = navigationManager.BaseUri;
    }

    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    return GrpcChannel.ForAddress(
        backendUrl,
        new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });
});

//Todo - implement services

/// Implements the ISessionService gRPC
builder.Services.AddTransient(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<ISessionService>();
});
builder.Services.AddTransient(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<IUserService>();
});

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<FakeAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());



await builder.Build().RunAsync();

