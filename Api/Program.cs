using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;


var orleansClient = InitialiseClient().Result;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGrainFactory>(orleansClient);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDeveloperExceptionPage();
app.Run();

static async Task<IClusterClient> InitialiseClient()
{
    var client = new ClientBuilder()
        .UseAdoNetClustering(options =>
        {
            options.Invariant = "Npgsql";
            options.ConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456a;Database=game";
        })
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "OrleansBasics";
        })
        .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole())
        .Build();

    await client.Connect();
    return client;
}




















/*public class Program
{
    public Program(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var orleansClient = InitialiseClient().Result;
        services.AddSingleton(orleansClient);
        services.AddMvc();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMvc();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerTest");
        });
    }

    private const int InitializeAttemptsBeforeFailing = 5;
    private static int attempt = 0;

    private static async Task<IClusterClient> InitialiseClient()
    {
        var client = new ClientBuilder()
            .UseAdoNetClustering(options =>
            {
                options.Invariant = "Npgsql";
                options.ConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456a;Database=game";
            })
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "OrleansBasics";
            })
            .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole())
            .Build();

        await client.Connect(RetryFilter);

        return client;
    }


    private static async Task<bool> RetryFilter(Exception exception)
    {
        if (exception.GetType() != typeof(SiloUnavailableException))
        {
            Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
            return false;
        }
        attempt++;
        Console.WriteLine($"Cluster client attempt {attempt} of {InitializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
        if (attempt > InitializeAttemptsBeforeFailing)
        {
            return false;
        }
        await Task.Delay(TimeSpan.FromSeconds(3));
        return true;
    }
}*/





