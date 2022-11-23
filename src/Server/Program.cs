using Squads.Services.Sessions;
using Squads.Services.Users;
using ProtoBuf.Grpc.Server;
using Squads.Persistence;
using Squads.Fakers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });

// Database configuration
builder.Services.AddDbContext<SquadsDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<FakeUserService>();
    endpoints.MapGrpcService<FakeSessionService>();
});


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SquadsDbContext>();
    FakeSeeder seeder = new(dbContext);
    seeder.Seed();
}

app.Run();
