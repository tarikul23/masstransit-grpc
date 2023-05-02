using GrpcService;
using GrpcService.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PersonConsumeEvent>();
    x.AddConsumer<UserConsumeEvent>();
    x.AddConsumer<UserRequestResponseEvent>();
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingGrpc((context, cfg) =>
    {
        
        cfg.Host(h =>
        {
            //h.Host = "127.0.0.1";
            h.Port = 19798;
            h.AddServer(new Uri("http://127.0.0.1:19796"));
            h.AddServer(new Uri("http://127.0.0.1:19799"));

        });
        //cfg.Host(new Uri("http://127.0.0.1:19798"));
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
