using Google.Protobuf;
using GprcShareEvent;
using GrpcService2;
using GrpcService2.Services;
using MassTransit;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static MassTransit.Logging.OperationName;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserConsumeEvent2>();
    x.AddConsumer<EmployeeConsumeEvent2>();
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingGrpc((context, cfg) =>
    {
        cfg.Host(h =>
        {
            //h.Host = "127.0.0.1";
            h.Port = 19799;
            //h.AddServer(new Uri("http://127.0.0.1:19798"));
            h.AddServer(new Uri("http://127.0.0.1:19796"));
        });
        cfg.ConfigureEndpoints(context, filter => filter.Include<UserConsumeEvent2>());
        //cfg.ConfigureEndpoints(context, filter => filter.Include<EmployeeConsumeEvent2>());


        cfg.ReceiveEndpoint("employee-consume", endpoint =>
        {
            endpoint.ConfigureConsumeTopology = false;
            endpoint.Bind("test-exchange",MassTransit.Transports.Fabric.ExchangeType.Topic, "this-is-route-key");

            endpoint.ConfigureConsumer<EmployeeConsumeEvent2>(context);
        });
    });
});
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
