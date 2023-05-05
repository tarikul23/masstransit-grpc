using GprcShareEvent;
using GprcWebApi;
using MassTransit;
using MassTransit.Clients;
using MassTransit.Transports;
using static MassTransit.Logging.OperationName;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EmployeeConsumeEvent>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingGrpc((context, cfg) =>
    {
        cfg.Host(h =>
        {
            //h.Host = "127.0.0.1";
            h.Port = 19796;
            h.AddServer(new Uri("http://127.0.0.1:19798"));  
            h.AddServer(new Uri("http://127.0.0.1:19799"));  
            
        });
        cfg.ConfigureEndpoints(context);
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("sent-to-person", async (IPublishEndpoint publishEndPoint) =>
{
    await publishEndPoint.Publish<PersonEvent>(new PersonEvent("Tarikul"));
});

app.MapGet("sent-to-user", async (IPublishEndpoint publishEndPoint) =>
{
    await publishEndPoint.Publish<UserEvent>(new UserEvent("Tarikul","Islam"));
});

app.MapGet("sent-to-employee", async (IPublishEndpoint publishEndPoint, ISendEndpointProvider sendEndpointProvider) =>
{

    var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("exchange:test-exchange?type=topic"));
    await endpoint.Send(new EmployeeEvent("rashed"), x => x.SetRoutingKey("service2.routeKey"));

    //await publishEndPoint.Publish<EmployeeEvent>(new EmployeeEvent("Rashed"));
});

app.MapPost("user-request-response", async (UserRequest userreq, IPublishEndpoint publishEndPoint, IBusControl _bus) =>
{
    IRequestClient<UserRequest> client = _bus.CreateRequestClient<UserRequest>();
    var result = await client.GetResponse<UserResponse>(userreq);


    return Results.Ok(result.Message);
});


app.Run();
