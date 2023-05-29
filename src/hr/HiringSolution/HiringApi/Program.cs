var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var kafkaConnectionString = builder.Configuration.GetConnectionString("kafka") ?? throw new ArgumentNullException("Need a kafka broker");
var dataConnectionString = builder.Configuration.GetConnectionString("data") ?? throw new ArgumentNullException("Need a data connection string");
builder.Services.AddCap(options =>
{
    options.UseKafka(kafkaConnectionString); // message broker
    options.UsePostgreSql(dataConnectionString);
    options.UseDashboard(); // just for class, but I think it's cool. 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
