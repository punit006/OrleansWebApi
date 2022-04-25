using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;

try
{
    var host = await StartSiloAsync();
    Console.WriteLine("\n\n Press Enter to terminate...\n\n");
    Console.ReadLine();

    await host.StopAsync();

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return 1;
}

static async Task<ISiloHost> StartSiloAsync()
{
    var builder = new SiloHostBuilder()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "OrleansBasics";
        })       
        .UseAdoNetClustering(options =>
        {
            options.Invariant = "Npgsql";
            options.ConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456a;Database=game";
        })
        .UseDashboard(options => {
            options.Username = "root";
            options.Password = "123";
        })
        .ConfigureEndpoints(11111,30000)
        .UseSiloUnobservedExceptionsHandler()
        .ConfigureLogging(logging => logging.AddConsole());

    var host = builder.Build();
    await host.StartAsync();

    return host;
}