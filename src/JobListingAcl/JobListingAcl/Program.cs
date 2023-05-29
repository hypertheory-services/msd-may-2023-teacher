using JobListingAcl.Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<HiringRequests>();
var dataConnectionString = builder.Configuration.GetConnectionString("data") ?? throw new ArgumentNullException("Need a connection string");
var kafkaConnectionString = builder.Configuration.GetConnectionString("kafka") ?? throw new ArgumentNullException("Need a kafka broker");


builder.Services.AddCap(options =>
{
    options.UseKafka(kafkaConnectionString); 
    options.UsePostgreSql(dataConnectionString); 
    options.UseDashboard(); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/", () => "Not an API");

app.Run();
